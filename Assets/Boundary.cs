using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private StayInBounds stayInBounds;

    public BoxCollider2D boundaryCollider;
    public void OnTriggerEnter2D(Collider2D col)
    {
        stayInBounds = col.GetComponent<StayInBounds>();
            stayInBounds?.SetUpBounds(boundaryCollider);
    }
}
