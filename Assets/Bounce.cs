using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    // Start is called before the first frame update

    private float gravity = 9.806f;

    private float initialSpeed = 15f;

    private float curSpeed;

    private float yMovementThisTick;

    private float bounceHeight;
    void OnEnable()
    {
        curSpeed = initialSpeed;
        //bounceHeight = transform.position.y - 
    }

    // Update is called once per frame
    void Update()
    {
        yMovementThisTick = curSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y + yMovementThisTick, transform.position.z);
        curSpeed = curSpeed - gravity * Time.deltaTime;
    }
}
