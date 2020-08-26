using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireProjectile", menuName = "SubProjectileAbility/Fire/FireProjectile", order = 1)]
public class FireProjectile : SubProjectileAbility
{
    public GameObject projectilePrefab;

    private List<GameObject> projectiles = new List<GameObject>();

    public override void DoInitialProjectileSubAbility(ProjectileAbility pa)
    {
        projectiles.Clear();
        if (projectiles.Count == 0)
        {
            GameObject tempProjectile;
            for (int i = 0; i < pa.numberOfProjectiles; i++)
            {
                tempProjectile = GameObject.Instantiate(projectilePrefab);
                tempProjectile.GetComponent<Fireable>().SetSourceAbility(pa.ability); //TODO: make factory for projectile creation
                tempProjectile.SetActive(false);
                projectiles.Add(tempProjectile);
            }
        }
    }

    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {
        //GameObject.Instantiate(projectilePrefab, pa.sources[0].position, pa.quatProjectileFireAngle);
        projectiles[pa.projectilesFired].transform.position = pa.sources[0].position;
        projectiles[pa.projectilesFired].transform.rotation = pa.quatProjectileFireAngle;
        projectiles[pa.projectilesFired].SetActive(true);

        pa.projectilesFired++;

        EndProjectileSubAbility();
    }
}
