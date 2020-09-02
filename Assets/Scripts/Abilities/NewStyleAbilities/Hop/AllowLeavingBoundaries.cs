using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AllowLeavingBoundaries", menuName = "SubAbility/Special/AllowLeavingBoundaries", order = 1)]
public class AllowLeavingBoundaries : SubAbility
{
    private StayInBounds stayInBounds;
    public override void DoInitialSubAbility(Ability ab)
    {
       stayInBounds = ab.owner.GetComponent<StayInBounds>();
       stayInBounds?.SetStayInBounds(false);
        EndSubAbility();
    }
    public override void DoSubAbility(Ability ab)
    {
        //throw new System.NotImplementedException();
    }
}
