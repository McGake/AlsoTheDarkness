using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnSecondaryOnTime : MonoBehaviour
{
    public float lifeTime;

    public GameObject objectToTurnOn;

    private float deathTime;

    public bool turnOffSelfOnTime = true;

    public void OnEnable()
    {
        deathTime = lifeTime + Time.time;
    }

    public void Update()
    {
        if (deathTime <= Time.time)
        {
            BattlePooler.pool.ProduceObject(objectToTurnOn, transform.position);
            if (turnOffSelfOnTime)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
