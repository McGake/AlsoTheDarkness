using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayWithXPositionOfObject : FireableBehavior
{
    GameObject objectToStayWith;

    public override void OnFire()
    {
        objectToStayWith = fireable.sourceAbility.Owner;
    }

    public void Update()
    {
        Vector3 tempXPos = new Vector3(objectToStayWith.transform.position.x, transform.position.y, transform.position.z);
        transform.position = tempXPos;
    }
}
