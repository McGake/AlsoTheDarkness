using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Stun", menuName = "StatusEffects/Stun", order = 1)]
public class Stun : Status //TODO: perhaps to turn this and things like this to an event that the battle actor listens for and self applies 
{

    public override void SetUpStatus(Ability sourceAbility, GameObject deliveryObject)
    {
    }

    //TODO: this may cause problems if abilities are not properly reset
    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
        Debug.Log("stun added to  " + bbA.gameObject.name);
        bbA.battleActorView.ShowStatus(this);
        AbilityManager.abManager.StopAllAbilitiesFromCharacter(bbA.gameObject);
    }

    //Right now the actuall prevention of action is handled by checking if a stun is in the characters statuses on BaseBattleActor. 
    public override void DoStatus(BaseBattleActor bbA)
    {
        
    }

    public override void DoStatusEnd(BaseBattleActor bbA)
    {
        Debug.Log("stun status end");
        CleanUpStatusIfLastOne(bbA);
    }
}
