using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireReturningTargetsProjectile", menuName = "SubProjectileAbility/Fire/FireReturningTargetsProjectile", order = 1)]
public class FireReturningTargetsProjectile : SubProjectileAbility
{
    public EffectOnContact projectilePrefab;

    private List<GameObject> projectiles = new List<GameObject>(); 

    private Ability ab;

    private int reported;

    private EffectOnContact subbedProjectile;

    public override void DoInitialProjectileSubAbility(ProjectileAbility pa) 
    {
        reported = 0;
        

        if (projectiles.Count == 0)
        {
            GameObject tempProjectile;
            for (int i = 0; i < pa.numberOfProjectiles; i++)
            {
                tempProjectile = ProjectileFactory.ProduceProjectile(projectilePrefab.gameObject, pa.ability);
                subbedProjectile = tempProjectile.GetComponent<EffectOnContact>();
                subbedProjectile.SubscribeToObjectsHit(ReceiveObjectsHit);
                
                tempProjectile.transform.position = pa.sources[0].position;
                projectiles.Add(tempProjectile);
                pa.projectilesFired++;
            }
        }
        ab = pa.ability;
    }

    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {
        //GameObject.Instantiate(projectilePrefab, pa.sources[0].position, pa.quatProjectileFireAngle);



        
    }

    private void ReceiveObjectsHit(List<GameObject> oH)
    {
        Debug.Log("recived objects&&&&&&&&&");
        subbedProjectile.UnsubscribeToObjectsHit(ReceiveObjectsHit);
        if (oH.Count > 0)
        {
            ab.objectTargets.AddRange(oH);
        }

        reported++;
        if (reported >= projectiles.Count)
        {
            projectiles.Clear();
            EndProjectileSubAbility();
        }
    }
}
