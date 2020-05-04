using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterSeconds : MonoBehaviour//This is temporary untill we add our object pooling system
{
    public float lifeTime;

    private float deathTime;

    public void OnEnable()
    {
        deathTime = lifeTime + Time.time;
    }

    public void Update()
    {
        if(deathTime<= Time.time)
        {
            gameObject.SetActive(false);
        }
    }
}
