using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireProjectileWithForce", menuName = "SubProjectileAbility/Fire/FireProjectileWithforce", order = 1)]
public class FireProjectileWithForce : SubProjectileAbility
{
    public GameObject projectilePrefab;
    private GameObject projectileInst;

    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {
        projectileInst = pa.ability.battlePooler.ProduceObject(projectilePrefab, pa.sources[0].position, pa.quatProjectileFireAngle);
  
        projectileInst.GetComponent<Rigidbody2D>().AddForce(projectileInst.transform.right*pa.power, ForceMode2D.Impulse);
        pa.projectilesFired++;

        Debug.Log("power used " + pa.power);
        Debug.Log("projectiels fired " + pa.projectilesFired);

        EndProjectileSubAbility();
    }
}
