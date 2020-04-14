using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AngleAttribute))]
public class AngleDrawer : PropertyDrawer
{
    private static Vector2 mousePosition;
    private static Texture2D KnobBack = Resources.Load("KnobBack") as Texture2D;
    private static Texture2D Knob = Resources.Load("Knob") as Texture2D;
    private static GUIStyle s_TempStyle = new GUIStyle();
    private static float height = 48;

    private AngleAttribute angleAttribute
    {
        get { return (AngleAttribute)attribute; }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.floatValue = FloatAngle(position, property.floatValue, angleAttribute.Snap, angleAttribute.Min, angleAttribute.Max);
        // name label
        EditorGUI.LabelField(new Rect(position.x + height, position.y, position.width, position.height), property.displayName);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return height;
    }

    public static float FloatAngle(Rect rect, float value, float snap, float min, float max)
    {
        Rect knobRect = new Rect(rect.x, rect.y, rect.height, rect.height);

        // Handle click & drag control
        int id = GUIUtility.GetControlID(FocusType.Passive, rect);
        if (Event.current != null)
        {
            if ((Event.current.type == EventType.MouseDown && knobRect.Contains(Event.current.mousePosition)) ||
                (Event.current.type == EventType.MouseDrag && GUIUtility.hotControl == id))
            {
                GUIUtility.hotControl = id;
                mousePosition = Event.current.mousePosition;

                // Clockface
                Vector2 dir = mousePosition - knobRect.center;
                var targetAngle = Mathf.Atan2(-dir.y, dir.x) * 180f / Mathf.PI;
                while (targetAngle < 0) targetAngle += 360;
                while (targetAngle > 360) targetAngle -= 360;
                value = targetAngle;

                if (snap > 0)
                {
                    float deltaSnap = value % snap;
                    value -= deltaSnap;
                }

                mousePosition = Event.current.mousePosition;
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && GUIUtility.hotControl == id)
                GUIUtility.hotControl = 0;
        }

        GUI.DrawTexture(knobRect, KnobBack);
        Matrix4x4 matrix = GUI.matrix;

        if (min != max)
            GUIUtility.RotateAroundPivot(-value * (360 / (max - min)), knobRect.center);
        else
            GUIUtility.RotateAroundPivot(-value, knobRect.center);

        GUI.DrawTexture(knobRect, Knob);
        GUI.matrix = matrix;


        // value label
        Rect label = new Rect(rect.x + rect.height, rect.y + ((rect.height / 2) +8) , rect.height * 2, 18);
        value = EditorGUI.FloatField(label, value);

        if (min != max)
            value = Mathf.Clamp(value, min, max);

        return value;
    }

    private static void DrawTexture(Rect position, Texture2D texture)
    {
        if (Event.current.type != EventType.Repaint)
            return;
        s_TempStyle.normal.background = texture;
        s_TempStyle.Draw(position, GUIContent.none, false, false, false, false);
    }
}