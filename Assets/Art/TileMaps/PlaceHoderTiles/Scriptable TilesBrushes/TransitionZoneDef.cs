using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
[CustomGridBrush(false, true, false, "Prefab Brush/TransitionZone")]
public class TransitionZoneDef : GridBrushBase
{
    public GameObject triggerZone;
    public string sceneToLoad;

    private Vector3 offset = new Vector3(0, 0, 0);

    const int zPos = 0;

    public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        //Vector3Int cellPosition = new Vector3Int(position.x, position.y, zPos);

        Vector3 cellPosition = new Vector3(position.x +.5f, position.y+.5f, zPos);
        if(GetObjectInCell(gridLayout, brushTarget.transform, new Vector3Int(position.x, position.y, zPos)) != null)
        {
            return;
        }

        GameObject go;

        go = Instantiate(triggerZone);

        go.GetComponent<ChangeLevelOnTouch>().levelToTransitionTo = sceneToLoad;

        go.transform.SetParent(brushTarget.transform);
        go.transform.position = gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(cellPosition) );
       // base.Paint(gridLayout, brushTarget, position);
    }

    private static Transform GetObjectInCell(GridLayout grid, Transform parent, Vector3Int position )
    {
        int childCount = parent.childCount;
        Vector3 min = grid.LocalToWorld(grid.CellToLocalInterpolated(position));

        Vector3 max = grid.LocalToWorld(grid.CellToLocalInterpolated(position + new Vector3(1f, 1f, 1f)));

        Bounds bounds = new Bounds((max + min) * .5f, max - min);

        Debug.Log("min " + min + " max " + max);

        for (int i = 0; i < childCount; i++)
        {
            Transform child = parent.GetChild(i);
            string tag = child.gameObject.tag;

            if(bounds.Contains(child.position) && (tag == "triggerObject"))
            {
                return child;
            }
        }

        return null;

    }
}
