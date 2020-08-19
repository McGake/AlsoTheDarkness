using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ClearDirTargets", menuName = "SubAbility/Targeting/ClearDirTargets", order = 1)]
public class ClearDirTargets : SubAbility
{
    //private bool skip = true;

    public override void DoInitialSubAbility(Ability ab)
    {
        ab.positionTargets.Clear();
        EndSubAbility();
        
    }


    public override void DoSubAbility(Ability ab)
    {
    }

}

