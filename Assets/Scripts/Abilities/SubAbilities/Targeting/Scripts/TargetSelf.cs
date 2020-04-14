using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TargetSelf", menuName = "SubAbilities/Targeting/TargetSelf", order = 1)]
public class TargetSelf : SubAbility
{
    public override void DoInitialSubAbility(Ability ab)
    {
        ab.objectTargets.Add(ab.owner);
        EndSubAbility();
    }

    public override void DoSubAbility(Ability ab)
    {
            
        
    }

    public override void OnSelectionFinished(List<GameObject> selectedObjects)
    {

    }


}
 
