using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TargetAllEnemies", menuName = "SubAbilities/Targeting/TargetAllEnemies", order = 1)]
public class TargetAllEnemies : SubAbility
{
    private Ability ability;

    public override void DoInitialSubAbility(Ability ab)
    {
        ability = ab;
        ab.StartSelectAllEnemeies(this, ab.actorType);
    }

    public override void DoSubAbility(Ability ab)
    {
        //if(skipFirst == true)
        //{
        //    skipFirst = false;
        //    return;//This skips one frame incase the player has already pressed a this frame to select the overall ability. TODO: do this better somehow and check if this is really even needed
        //}

    }

    public override void OnSelectionFinished(List<GameObject> selectedObjects)
    {
        ability.objectTargets.AddRange(selectedObjects);
        EndSubAbility();
    }
}
 
