using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayWithXPositionOfObject : FireableBehavior
{
    private GameObject objectToStayWith;
    bool follow = false;
    public override void OnFire()
    {
        objectToStayWith = fireable.sourceAbility.Owner;
        follow = true;
    }
    public void Update()
    {
        if (follow)
        {
            Vector3 tempXPos = new Vector3(objectToStayWith.transform.position.x, transform.position.y, transform.position.z);
            transform.position = tempXPos;
        }
    }
    public void OnDisable()
    {
        follow = false;
    }
}
