using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AddStatusToSingle", menuName = "SubAbilities/AddStatus/AddStatusToSingle", order = 1)]
public class AddStatusTosingle : SubAbility
{


#pragma warning disable 649
    [SerializeField]
    private Status statusToAdd;
#pragma warning restore 649

    private Status statusToAddInstance;

    private CastSequence castSequence;

    public override void DoInitialSubAbility(Ability ab)
    {
        statusToAddInstance = statusToAdd.CreateStatusInstance(ab.stats);
        ab.objectTargets[0].GetComponent<BaseBattleActor>().AddStatus(statusToAddInstance);        
        EndSubAbility();
    }

    public override void DoSubAbility(Ability ab)
    {

    }
}


