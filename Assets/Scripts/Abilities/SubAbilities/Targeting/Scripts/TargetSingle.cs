using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TargetSingle", menuName = "SubAbilities/Targeting/TargetSingle", order = 1)]
public class TargetSingle : SubAbility
{
    public SelectionCategories sC;

    private Ability ability;//TODO: possibly set this when I initialize/instantiate the sub ability and then stop passing it to each method

    public override void DoInitialSubAbility(Ability ab)
    {
        ab.StartSelectFromPCs(this);
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
 
