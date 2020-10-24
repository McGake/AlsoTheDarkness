using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireProjectileWithForce", menuName = "SubProjectileAbility/Fire/FireProjectileWithforce", order = 1)]
public class FireProjectileWithForce : SubProjectileAbility
{
    public Fireable projectilePrefab;
    private GameObject projectileInstance;

    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {
        //Fireable and my projectile factory gauruntees that projectile will have a rigidbody. ProjectileFactory is hooked up to my pooler which duplicates the functionality of instantiate, 
        //but insures that we wont be endlessly instantiating objects that already exist in deactivated states.
        projectileInstance = ProjectileFactory.ProduceProjectile(projectilePrefab.gameObject, pa.ability, pa.sources[0].position, pa.quatProjectileFireAngle); 
        projectileInstance.GetComponent<Rigidbody2D>().AddForce(projectileInstance.transform.right*pa.power, ForceMode2D.Impulse);
        pa.projectilesFired++;
        EndProjectileSubAbility();
    }
}
