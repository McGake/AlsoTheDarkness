﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireCallbackOnDisable", menuName = "SubProjectileAbility/Fire/FireCallbackOnDisable", order = 1)]
public class FireCallbackOnDisable : SubProjectileAbility
{
    public GameObject projectilePrefab;

    public EffectOnContact eOC;

    private List<GameObject> projectiles = new List<GameObject>(); 

    private Ability ab;

    private int reported;

    public override void DoInitialProjectileSubAbility(ProjectileAbility pa) 
    {       
        if (projectiles.Count == 0)
        {
            GameObject tempProjectile;
            for (int i = 0; i < pa.numberOfProjectiles; i++)
            {
                tempProjectile = Instantiate(eOC.gameObject);
                Debug.Log(tempProjectile.name + " created");
                eOC = tempProjectile.GetComponent<EffectOnContact>();
                eOC.SendObjectsHit = ReceiveObjectsHit;

                tempProjectile.SetActive(false);
                projectiles.Add(tempProjectile);
            }
        }

        ab = pa.ability;
        Debug.Log(ab);

        projectiles[pa.projectilesFired].transform.position = pa.sources[0].position;
        projectiles[pa.projectilesFired].SetActive(true);
        pa.projectilesFired++;
    }

    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {
        //GameObject.Instantiate(projectilePrefab, pa.sources[0].position, pa.quatProjectileFireAngle);



        
    }

    private void ReceiveObjectsHit(List<GameObject> oH)
    {

        ab.objectTargets.AddRange(oH);
        Debug.Log(oH[0].name);
        reported++;
        if (reported >= projectiles.Count)
        {
            EndProjectileSubAbility();
        }
    }
}
