using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TargetSingleEnemy", menuName = "SubAbilities/Targeting/TargetSingleEnemy", order = 1)]
public class TargetSingleEnemy : SubAbility//TODO: THIs is no longer target single. rename to target single pc or something
{

    private Ability ability;

    public override void DoInitialSubAbility(Ability ab)
    {
        ab.StartSelectFromEnemies(this);
        ability = ab;
    }

    public override void DoSubAbility(Ability ab)
    {
            
        
    }

    public override void OnSelectionFinished(List<GameObject> selectedObjects)
    {
        ability.objectTargets.AddRange(selectedObjects);
        EndSubAbility();
    }


}
 
