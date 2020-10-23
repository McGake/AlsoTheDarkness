using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CastingForTime", menuName = "SubAbilities/Prep/CastingForTime", order = 1)]
public class CastingForTime : SubAbility
{
    public float time;
    private float endTime;
    public override void DoInitialSubAbility(Ability ab)
    {
        SetNewAnimation("castSpell", ab);
        EndSubAbility();
        endTime = Time.time + time;
    }

    public override void DoSubAbility(Ability aB)
    {
        if(Time.time > endTime)
        {
            EndSubAbility();
        }
    }
}
