using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationZone : MonoBehaviour
{
    public float rotation;

    public void OnTriggerEnter2D(Collider2D col)
    {
        Quaternion rot = Quaternion.Euler(0, rotation, 0);
        col.transform.SetPositionAndRotation(col.transform.position, rot);
    }
}
