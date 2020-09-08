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
        tempProjectile = BattlePooler.ProduceObject(projectilePrefab);
        tempProjectile.GetComponent<Fireable>().SetSourceAbility(pa.ability); //TODO: make factory for projectile creation
        tempProjectile.transform.position = pa.sources[0].position;
        tempProjectile.transform.rotation = pa.quatProjectileFireAngle;
        pa.projectilesFired++;
        EndProjectileSubAbility();
    }

    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {
    }
}
