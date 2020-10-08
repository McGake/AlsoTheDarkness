using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireProjectileWithForce", menuName = "SubProjectileAbility/Fire/FireProjectileWithforce", order = 1)]
public class FireProjectileWithForce : SubProjectileAbility
{
    public Fireable projectilePrefab;
    private GameObject projectileInst;

    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {
        Debug.Log(pa.ability);
        Debug.Log(projectilePrefab);


        projectileInst = ProjectileFactory.ProduceProjectile(projectilePrefab.gameObject, pa.ability, pa.sources[0].position, pa.quatProjectileFireAngle);
        projectileInst.GetComponent<Rigidbody2D>().AddForce(projectileInst.transform.right*pa.power, ForceMode2D.Impulse);
        pa.projectilesFired++;
        EndProjectileSubAbility();
    }
}
