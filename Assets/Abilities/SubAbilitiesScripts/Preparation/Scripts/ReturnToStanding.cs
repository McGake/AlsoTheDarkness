using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ReturnToStanding", menuName = "SubAbilities/Prep/ReturnToStanding", order = 1)]
public class ReturnToStanding : SubAbility
{
    public override void DoInitialSubAbility(Ability ab)
    {
        SetNewAnimation("stand", ab);
        EndSubAbility();
    }
    public override void DoSubAbility(Ability aB)
    {
    }
}
