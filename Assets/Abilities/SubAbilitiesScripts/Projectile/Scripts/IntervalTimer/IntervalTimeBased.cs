using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "IntervalTimed", menuName = "SubProjectileAbility/Interval/Timed", order = 1)]
public class IntervalTimeBased : SubProjectileAbility
{
    private float nextFireTime = 0f;
    public float fireInterval;

    public void Awake()
    {
        nextFireTime = 0f;
    }

    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireInterval;
            EndProjectileSubAbility();
        }
    }

}
