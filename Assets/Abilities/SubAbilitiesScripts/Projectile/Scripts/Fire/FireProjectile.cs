using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireProjectile", menuName = "SubProjectileAbility/Fire/FireProjectile", order = 1)]
public class FireProjectile : SubProjectileAbility
{
    public GameObject projectilePrefab;

    public override void DoInitialProjectileSubAbility(ProjectileAbility pa)
    {
        GameObject tempProjectile;
        tempProjectile = ProjectileFactory.ProduceProjectile(projectilePrefab, pa.ability, pa.sources[0].position, pa.quatProjectileFireAngle);
        pa.projectilesFired++;
        EndProjectileSubAbility();
    }
}
