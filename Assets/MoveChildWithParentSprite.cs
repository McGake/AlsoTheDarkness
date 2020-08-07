using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChildWithParentSprite : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 staticRelativePos;

    private Transform parentPosition;

    private BoxCollider2D parentCollision;
    void Awake()
    {
        parentCollision =transform.parent.GetComponent<BoxCollider2D>();
        staticRelativePos = parentCollision.bounds.size - transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = parentCollision.bounds.size - staticRelativePos;
    }
}
