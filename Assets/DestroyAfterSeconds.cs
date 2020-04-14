using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float destroyAfterSeconds;

    private float destroyTime;

    public void Start()
    {
        destroyTime = Time.time + destroyAfterSeconds;
    }

    public void Update()
    {
        if(Time.time >= destroyTime)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
