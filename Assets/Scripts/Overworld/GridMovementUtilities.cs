using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class GridMovementUtilities
{
    [HideInInspector]
    public Transform ownerTransform;

    public Tilemap terrainMap;
    public float speed;
    public float pointArrivalThreshold;
    public Vector2? nextCellCenter = null;
    public Vector2 raycastOffset;
    [HideInInspector]
    public float tileSize = .5f;

    public void Setup(Transform transform)
    {
        ownerTransform = transform;
        tileSize = terrainMap.cellSize.x;
    }

    public Vector2 CalculateDirectionWithTarget(Vector2 targetPoint)
    {
        return (targetPoint - (Vector2)((Vector2)ownerTransform.position + raycastOffset)).normalized;
    }

    public Vector2 CalculateDirectionWithInput(Vector2 input)
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            return new Vector2(input.x, 0f).normalized;
        }

        return new Vector2(0f, input.y).normalized;
    }



    public void CalculateNextSquare(Vector2 direction)
    {

        Vector2 tempCellCenter = (Vector2)((Vector2)ownerTransform.position + raycastOffset) + (direction * tileSize);
        Vector3Int nextCellCoords = terrainMap.WorldToCell(tempCellCenter);
        //This makes sure that the next cell position is exact
        nextCellCenter = terrainMap.GetCellCenterWorld(nextCellCoords);
        //Debug.Log("next cell : " +nextCellCenter.ToString("F4") + " position " + ownerTransform.position.ToString("F4") + " raycast offset = : " + raycastOffset.ToString("F4") );
        //Debug.Log("calcultate next square " + transform.position + " " + nextCellCenter + " " + tempCellCenter + " " + (direction * tileSize) + " " + direction + " " + tileSize);
    }

    public bool ArivedAtNextSquare()
    {
        if (nextCellCenter == null)
        {
            CalculateNextSquare(Vector2.zero);
        }
        float distance = Vector2.Distance((Vector2)((Vector2)ownerTransform.position + raycastOffset), (Vector2)nextCellCenter);

        //Debug.Log(((Vector2)ownerTransform.position + raycastOffset).ToString("F4") + " " + distance.ToString("F4") + " " + nextCellCenter.ToString("F4"));
        if (Mathf.Abs(distance) <= pointArrivalThreshold)
        {
            return true;
        }
        return false;
    }

    public void MoveInDirection(Vector2 dir)
    {

        Vector2 translation = dir * speed * Time.deltaTime;
        ownerTransform.Translate(translation);

    }

    public LayerMask mask;
    public bool IsObstructionIn(Vector2 direction)
    {
        RaycastHit2D rh2d = Physics2D.Raycast(((Vector2)ownerTransform.position + raycastOffset), direction, tileSize * 1.45f, mask);
        if (rh2d.transform != null)
        {
            //Debug.Log("Obstruction name is: " + rh2d.transform.name);
            if (rh2d.transform.gameObject.layer == LayerMask.NameToLayer("BaseOverworldTerrain"))
            {
                return true;
            }
        }

        return false;
    }
}
