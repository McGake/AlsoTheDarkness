﻿using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FireProjectileAndWaitForDisable", menuName = "SubProjectileAbility/Fire/FireProjectileAndWaitForDisable", order = 1)]
public class FireProjectileAndWaitForDisable : SubProjectileAbility
{
    public GameObject projectilePrefab;
    private GameObject curProjectile;

    public override void DoInitialProjectileSubAbility(ProjectileAbility pa)
    {
        if (curProjectile == null)
        {
            curProjectile = ProjectileFactory.ProduceProjectile(projectilePrefab, pa.ability);
        }
        curProjectile.transform.position = pa.sources[0].position;
        curProjectile.transform.rotation = pa.quatProjectileFireAngle;
        curProjectile.SetActive(true);
        pa.projectilesFired++;
    }

    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {      
        if (!curProjectile.activeInHierarchy)
        {
            EndProjectileSubAbility();
        }
    }
}
