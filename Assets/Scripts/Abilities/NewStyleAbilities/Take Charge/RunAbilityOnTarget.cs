using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RunAbilityOnTarget", menuName = "SubAbilities/ChangeAbilitiesOnOther/RunAbilityOnTarget", order = 1)]
public class RunAbilityOnTarget : SubAbility
{
    public Ability abilityToAdd;

    private Ability abilityInstance;

    public override void DoInitialSubAbility(Ability ab)
    {
        abilityInstance = Instantiate(abilityToAdd);
        abilityInstance.SetupAbility(ab.singleObjectTarget);
        
        AbilityManager.abManager.TurnOnAbility(abilityInstance);
        EndSubAbility();
    }

    public override void DoSubAbility(Ability ab)
    {

    }
}


