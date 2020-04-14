using System.Collections;
using UnityEditor.IMGUI.Controls;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestMagicAim))]
public class MagicSetup : Editor
{
    TestMagicAim tMA;

    ArcHandle arcHandle = new ArcHandle();

    void OnEnable()
    {
        tMA = (TestMagicAim)target;
        arcHandle.SetColorWithRadiusHandle(Color.white, 0.1f);
    }

    //public override void OnInspectorGUI()
    //{
    //    DrawDefaultInspector();
    //}

    void OnSceneGUI()
    {
        tMA = (TestMagicAim)target;
        arcHandle.angle = tMA.angle;
        //Handles.Label(tMA.transform.position, "angle of projectiels : " + tMA.name);

        Vector3 handleDirection = tMA.facingDirection;
        Debug.Log(handleDirection);
        Vector3 handleNormal = Vector3.Cross(handleDirection, Vector3.up);
        Matrix4x4 handleMatrix = Matrix4x4.TRS(
            tMA.transform.position,
            Quaternion.LookRotation(handleDirection, handleNormal),
            Vector3.one);

        using (new Handles.DrawingScope(handleMatrix))
        {
            // draw the handle
            EditorGUI.BeginChangeCheck();
            arcHandle.DrawHandle();
            if (EditorGUI.EndChangeCheck())
            {
                // record the target object before setting new values so changes can be undone/redone
                Undo.RecordObject(tMA, "Change Projectile Properties");

                // copy the handle's updated data back to the target object
                tMA.angle = arcHandle.angle;
                //tMA.impulse = arcHandle.radius;
            }
        }
    }


}
