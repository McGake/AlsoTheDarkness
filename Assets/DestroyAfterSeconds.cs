using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float destroyAfterSeconds;

    private float destroyTime;

    public GameObject turnOnOnDestroy = null;

    public void Start()
    {
        destroyTime = Time.time + destroyAfterSeconds;
    }

    public void Update()
    {
        if(Time.time >= destroyTime)
        {
            if(turnOnOnDestroy != null)
            {
                turnOnOnDestroy.transform.SetParent(null);
                turnOnOnDestroy.SetActive(true);
            }
            GameObject.Destroy(gameObject);
        }
    }
}
