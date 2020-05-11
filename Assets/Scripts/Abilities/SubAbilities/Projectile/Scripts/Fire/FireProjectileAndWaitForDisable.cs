using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireProjectileAndWaitForDisable", menuName = "SubProjectileAbility/Fire/FireProjectileAndWaitForDisable", order = 1)]
public class FireProjectileAndWaitForDisable : SubProjectileAbility
{
    public GameObject projectilePrefab;

    private List<GameObject> projectiles = new List<GameObject>();

    private GameObject curProjectile;

    public override void DoInitialProjectileSubAbility(ProjectileAbility pa)
    {
       
        if (projectiles.Count == 0)
        {
            GameObject tempProjectile;
            for (int i = 0; i < pa.numberOfProjectiles; i++)
            {
                tempProjectile = GameObject.Instantiate(projectilePrefab);
                tempProjectile.GetComponent<EffectOnContact>().SetSourceAbility(pa.ability); //TODO: make factory for projectile creation
                tempProjectile.SetActive(false);
                projectiles.Add(tempProjectile);
            }
        }
        curProjectile = projectiles[pa.projectilesFired];
        curProjectile.transform.position = pa.sources[0].position;
        curProjectile.transform.rotation = pa.quatProjectileFireAngle;
        curProjectile.SetActive(true);
        pa.projectilesFired++;
    }

    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {

        if(!curProjectile.activeInHierarchy)
        {
            EndProjectileSubAbility();
        }
        //GameObject.Instantiate(projectilePrefab, pa.sources[0].position, pa.quatProjectileFireAngle);

        


        
    }
}
