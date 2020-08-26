using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: consider putting this as a behavior on the battle actors themselves
public class Barrier : MonoBehaviour
{

    private BoxCollider2D bC2D;

    private Vector3 curSettingPosition;

    private List<StopInfo> stopInfos;

    private CanPassBarriers canPassStatus;
    public enum DirectionToRestrict
    {
        none= 0,
        right = 1,
        left =2,
        up = 3,
        down = 4,
    }
    public void Awake()
    {
        bC2D = GetComponent<BoxCollider2D>();
        
    }
    public class StopInfo
    {
        public Transform objectToStop;
        public Vector3 stopPosition;
        public DirectionToRestrict dirToRestrict;
        
        public StopInfo(Transform oTS, Vector3 sP, DirectionToRestrict dTR)
        {
            objectToStop = oTS;
            stopPosition = sP;
            dirToRestrict = dTR;
        }
    }

    public void Start()
    {
        stopInfos = new List<StopInfo>();
    }

    private Vector3 closestBounds;

    private DirectionToRestrict dirToRestrict;
    public void OnTriggerEnter2D(Collider2D col)
    {

        if (col.GetComponent<BaseBattleActor>() != null)
        {
            if (col.GetComponent<BaseBattleActor>().HasStatus(canPassStatus) == false)
            {
                GetClosestBound(col);
                curSettingPosition = closestBounds;
                col.transform.position = curSettingPosition;
                stopInfos.Add(new StopInfo(col.transform, curSettingPosition, dirToRestrict));
            }
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        GetClosestBound(col);
        curSettingPosition = closestBounds;
        col.transform.position = curSettingPosition;
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        for (int i = 0; i < stopInfos.Count; i++)
        {
            if(stopInfos[i].objectToStop == col.transform)
            {
                stopInfos.RemoveAt(i);
                break;
            }
        }
    }

    public void LateUpdate()
    {
        for (int i = 0; i < stopInfos.Count; i++)
        {
            if(stopInfos[i].dirToRestrict == DirectionToRestrict.right)
            {
                if(stopInfos[i].objectToStop.position.x > stopInfos[i].stopPosition.x)
                {
                    stopInfos[i].objectToStop.position = new Vector3(stopInfos[i].stopPosition.x, stopInfos[i].objectToStop.position.y, stopInfos[i].objectToStop.position.z);
                }
            }
            if (stopInfos[i].dirToRestrict == DirectionToRestrict.left)
            {
                if (stopInfos[i].objectToStop.position.x < stopInfos[i].stopPosition.x)
                {
                    stopInfos[i].objectToStop.position = new Vector3(stopInfos[i].stopPosition.x, stopInfos[i].objectToStop.position.y, stopInfos[i].objectToStop.position.z);
                }
            }
            if (stopInfos[i].dirToRestrict == DirectionToRestrict.up)
            {
                if (stopInfos[i].objectToStop.position.y > stopInfos[i].stopPosition.y)
                {
                    stopInfos[i].objectToStop.position = new Vector3(stopInfos[i].objectToStop.position.x, stopInfos[i].stopPosition.y, stopInfos[i].objectToStop.position.z);
                }
            }
            if (stopInfos[i].dirToRestrict == DirectionToRestrict.down)
            {
                if (stopInfos[i].objectToStop.position.y < stopInfos[i].stopPosition.y)
                {
                    stopInfos[i].objectToStop.position = new Vector3(stopInfos[i].objectToStop.position.x, stopInfos[i].stopPosition.y, stopInfos[i].objectToStop.position.z);
                }
            }
            
        }
    }

    private void GetClosestBound(Collider2D col)
    {

        Vector3 pos = col.transform.position;
        Vector3 max = bC2D.bounds.max;
        Vector3 min = bC2D.bounds.min;

        float smallestDist = 100000f;

        closestBounds = Vector3.zero;

        if(Mathf.Abs(pos.x - max.x) < smallestDist)
        {
            smallestDist = Mathf.Abs(pos.x - max.x);
            closestBounds = new Vector3(max.x + col.bounds.extents.x, pos.y, pos.z);
            dirToRestrict = DirectionToRestrict.left;

        }
        if (Mathf.Abs(pos.x - min.x) < smallestDist)
        {
            smallestDist = Mathf.Abs(pos.x - min.x);
            closestBounds = new Vector3(min.x -col.bounds.extents.x, pos.y, pos.z);
            dirToRestrict = DirectionToRestrict.right;
 

        }
        if (Mathf.Abs(pos.y - max.y) < smallestDist)
        {
            smallestDist = Mathf.Abs(pos.y - max.y);
            closestBounds = new Vector3(pos.x, max.y + col.bounds.extents.y, pos.z);
            dirToRestrict = DirectionToRestrict.down;
  

        }
        if (Mathf.Abs(pos.y - min.y) < smallestDist)
        {
            smallestDist = Mathf.Abs(pos.y - min.y);
            closestBounds = new Vector3(pos.x, min.y - col.bounds.extents.y, pos.z);
            dirToRestrict = DirectionToRestrict.up;
       

        }

    }
}
