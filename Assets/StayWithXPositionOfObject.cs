using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayWithXPositionOfObject : MonoBehaviour
{
    Fireable fireable;

    GameObject objectToStayWith;

    public void OnEnable()
    {
        fireable = GetComponent<Fireable>();
        objectToStayWith = fireable.sourceAbility.owner;
    }

    public void Update()
    {
        Vector3 tempXPos = new Vector3(objectToStayWith.transform.position.x, transform.position.y, transform.position.z);
        transform.position = tempXPos;
    }
}
