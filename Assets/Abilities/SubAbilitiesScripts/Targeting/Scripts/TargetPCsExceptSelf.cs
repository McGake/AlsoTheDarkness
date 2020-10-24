using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TargetPCsExceptSelf", menuName = "SubAbilities/Targeting/TargetPCsExecptSelf", order = 1)]
public class TargetPCsExceptSelf : SubAbility
{
    private Ability ability;

    public override void DoInitialSubAbility(Ability ab)
    {
        ability = ab;
        ab.StartSelectAllPCsButCurrent(this, ab.ActorType);
    }

    public override void OnSelectionFinished(List<GameObject> selectedObjects)
    {
        ability.objectTargets.AddRange(selectedObjects);
        EndSubAbility();
    }
}
 
