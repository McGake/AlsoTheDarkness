using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    // Start is called before the first frame update

    private float gravity = 9.806f;

    private float initialSpeed = 3f;

    private float curSpeed;

    private float yMovementThisTick;

    private float bounceHeight;

    private float floorDistance = .4f;
    void OnEnable()
    {
        curSpeed = initialSpeed;
        bounceHeight = transform.position.y - floorDistance;
    }

    // Update is called once per frame
    void Update()
    {
        yMovementThisTick = curSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y + yMovementThisTick, transform.position.z);
        curSpeed = curSpeed - gravity * Time.deltaTime;

        if(bounceHeight > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, bounceHeight, transform.position.z);
            curSpeed = -curSpeed - 1.75f;
        }
    }
}
