using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FireProjectileCallbacks", menuName = "SubAbility/ProjectileManagement/FireProjectileCallbacks", order = 1)]
public class FireProjectileCallbacks : SubAbility
{
    public override void DoInitialSubAbility(Ability ab)
    {
        ab.RunProjectileCallbacks();
        EndSubAbility();
    }
    public override void DoSubAbility(Ability ab)
    {
    }
}
