using System;
using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("WE INSTANTIATED");
            curProjectile = GameObject.Instantiate(projectilePrefab);
        }
            
       
        Debug.Log("projectiels fired " + pa.projectilesFired);
        Debug.Log("pa source name" + pa.sources[0].name);
        Debug.Log(" pa source position " + pa.sources[0].position);
        curProjectile.transform.position = pa.sources[0].position;
        curProjectile.transform.rotation = pa.quatProjectileFireAngle;
        curProjectile.SetActive(true);
        pa.projectilesFired++;
    }

    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {

        EndProjectileSubAbility();
        if (!curProjectile.activeInHierarchy)
        {
            
        }
    }
}
