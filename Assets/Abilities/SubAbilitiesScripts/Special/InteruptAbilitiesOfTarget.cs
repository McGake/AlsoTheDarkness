using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InteruptAbilitiesOfTarget", menuName = "SubAbilities/ChangeAbilitiesOnOther/InteruptAbilitiesOfTarget", order = 1)]
public class InteruptAbilitiesOfTarget : SubAbility
{
    public override void DoInitialSubAbility(Ability ab)
    {
        AbilityManager.abManager.StopAllAbilitiesFromCharacter(ab.singleObjectTarget);
        EndSubAbility();
    }

    public override void DoSubAbility(Ability ab)
    {

    }
}


