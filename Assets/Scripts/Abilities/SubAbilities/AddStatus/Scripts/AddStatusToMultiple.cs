using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AddStatusToMultiple", menuName = "SubAbilities/AddStatus/AddStatusToMultiple", order = 1)]
public class AddStatusToMultiple : SubAbility
{

    [SerializeField]
#pragma warning disable 649
    private Status statusToAdd;
#pragma warning restore 649

    private Status statusToAddInstance;

    private CastSequence castSequence;

    public override void DoInitialSubAbility(Ability ab)
    {
        foreach (GameObject gO in ab.objectTargets)
        {
            statusToAddInstance = Instantiate(statusToAdd);
            gO.GetComponent<BaseBattleActor>().AddStatus(statusToAddInstance);
        }

        EndSubAbility();
    }

    public override void DoSubAbility(Ability ab)
    {

    }
}


