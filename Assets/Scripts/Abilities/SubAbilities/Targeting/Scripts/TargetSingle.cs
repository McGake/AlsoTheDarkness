using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TargetSingle", menuName = "SubAbilities/Targeting/TargetSingle", order = 1)]
public class TargetSingle : SubAbility//TODO: THIs is no longer target single. rename to target single pc or something
{

    private Ability ability;

    public override void DoInitialSubAbility(Ability ab)
    {
        ab.StartSelectFromPCs(this, ab.ActorType);
        ability = ab;
    }

    public override void DoSubAbility(Ability ab)
    {
            
        
    }

    public override void OnSelectionFinished(List<GameObject> selectedObjects)
    {
        Debug.Log("on selection finished called");
       ability.objectTargets.AddRange(selectedObjects);
        EndSubAbility();
    }


}
 
