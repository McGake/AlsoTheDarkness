using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SourceSelfCenter", menuName = "SubProjectileAbility/Source/SelfCenter", order = 1)]
public class SetProjSourceSelfCenter : SubProjectileAbility
{
    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {
        if (pa.sources.Count == 0)
        {
            pa.sources.Add(pa.ability.owner.transform);
        }
        else
        {
            pa.sources[0] = pa.ability.owner.transform;
        }
        EndProjectileSubAbility();
    }
}
