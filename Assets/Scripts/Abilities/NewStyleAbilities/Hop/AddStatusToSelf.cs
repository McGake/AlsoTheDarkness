using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AddStatusToSelf", menuName = "SubAbilities/AddStatus/AddStatusToSelf", order = 1)]
public class AddStatusToSelf : SubAbility
{
#pragma warning disable 649
    [SerializeField]
    private Status statusToAdd;
#pragma warning restore 649

    private Status statusToAddInstance;
    public override void DoInitialSubAbility(Ability ab)
    {
        statusToAddInstance = Instantiate(statusToAdd);
        ab.owner.GetComponent<BaseBattleActor>().AddStatus(statusToAddInstance);
        EndSubAbility();
    }

    public override void DoSubAbility(Ability ab)
    {

    }
}


