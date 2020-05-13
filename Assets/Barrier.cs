using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{

    private BoxCollider2D bC2D;

    public void Awake()
    {
        bC2D = GetComponent<BoxCollider2D>();
    }
public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("entered " + col.gameObject.name);
        
        col.transform.position = GetClosestBound(col);
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        col.transform.position = GetClosestBound(col);
    }

    private Vector3 GetClosestBound(Collider2D col)
    {

        Vector3 pos = col.transform.position;
        Vector3 max = bC2D.bounds.max;
        Vector3 min = bC2D.bounds.min;

        float smallestDist = 100000f;

        float setX = 0f;
        float setY = 0f;

        Vector3 valToSet = Vector3.zero;

        if(Mathf.Abs(pos.x - max.x) < smallestDist)
        {
            smallestDist = Mathf.Abs(pos.x - max.x);
            valToSet = new Vector3(max.x + col.bounds.extents.x, pos.y, pos.z);

        }
        if (Mathf.Abs(pos.x - min.x) < smallestDist)
        {
            smallestDist = Mathf.Abs(pos.x - min.x);
            valToSet = new Vector3(min.x -col.bounds.extents.x, pos.y, pos.z);
        }
        if (Mathf.Abs(pos.y - max.y) < smallestDist)
        {
            smallestDist = Mathf.Abs(pos.y - max.y);
            valToSet = new Vector3(pos.x, max.y + col.bounds.extents.y, pos.z);
        }
        if (Mathf.Abs(pos.y - min.y) < smallestDist)
        {
            smallestDist = Mathf.Abs(pos.y - min.y);
            valToSet = new Vector3(pos.x, min.y - col.bounds.extents.y, pos.z);
        }
        

        return valToSet;


    }
}
