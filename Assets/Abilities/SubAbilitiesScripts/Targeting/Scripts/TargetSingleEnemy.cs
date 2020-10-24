using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TargetSingleEnemy", menuName = "SubAbilities/Targeting/TargetSingleEnemy", order = 1)]
public class TargetSingleEnemy : SubAbility
{
    private Ability ability;

    public override void DoInitialSubAbility(Ability ab)
    {
        ab.StartSelectFromEnemies(this, ab.ActorType);
        ability = ab;
    }

    public override void OnSelectionFinished(List<GameObject> selectedObjects)
    {
        ability.objectTargets.AddRange(selectedObjects);
        EndSubAbility();
    }
}
 
