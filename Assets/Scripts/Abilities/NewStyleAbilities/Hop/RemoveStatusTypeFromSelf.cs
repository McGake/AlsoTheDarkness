using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RemoveStatusTypeFromSelf", menuName = "SubAbilities/AddStatus/RemoveStatusTypeFromSelf", order = 1)]
public class RemoveStatusTypeFromSelf : SubAbility
{
#pragma warning disable 649
    [SerializeField]
    private Status statusOfTypeToRemove;
#pragma warning restore 649

    public override void DoInitialSubAbility(Ability ab)
    {
        ab.owner.GetComponent<BaseBattleActor>().FinishAllStatusesOfType(statusOfTypeToRemove);
        EndSubAbility();
    }

    public override void DoSubAbility(Ability ab)
    {

    }
}



