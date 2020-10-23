using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ObjectTargetToDirection", menuName = "SubAbility/Movement/ObjectTargetToDirection", order = 1)]
public class ObjectTargetToDirection : SubAbility
{
    public override void DoInitialSubAbility(Ability ab)
    {
        ab.positionTargets.Add((ab.objectTargets[0].transform.position - ab.Owner.transform.position).normalized);     
    }

    public override void DoSubAbility(Ability ab)
    {
    }
}
