using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CanPassBarriers", menuName = "StatusEffects/Can Pass Barriers", order = 1)]
public class CanPassBarriers : Status //TODO: perhaps to turn this and things like this to an event that the battle actor listens for and self applies 
{

    public override void SetReferences(Ability sourceAbility, GameObject deliveryObject)
    {
    }

    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
    }

    public override void DoStatus(BaseBattleActor bbA)
    {
    }

    public override void DoStatusEnd(BaseBattleActor bbA)
    {
        //make sure character is inside boundaries at this point and move him in if not
    }
}

