using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnSecondaryOnTime : MonoBehaviour
{
    public float lifeTime;

    public GameObject objectToTurnOn;

    private float deathTime;

    public bool turnOffSelfOnTime = true;

    private Fireable fireable;

    public void Awake()
    {
        fireable = GetComponent<Fireable>();
    }

    public void OnEnable()
    {
        deathTime = lifeTime + Time.time;
    }

    public void Update()
    {
        if (deathTime <= Time.time)
        {
            Debug.Log(fireable);
            Debug.Log(fireable.sourceAbility);
            ProjectileFactory.ProduceProjectile(objectToTurnOn, fireable.sourceAbility, transform.position);
            if (turnOffSelfOnTime)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
