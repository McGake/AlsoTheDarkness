using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DefensivePosture", menuName = "StatusEffects/DefensivePosture", order = 1)]
public class DefensivePosture : Status //TODO: perhaps to turn this and things like this to an event that the battle actor listens for and self applies 
{

    public override void SetReferences(Ability sourceAbility, GameObject deliveryObject)
    {
    }

    private float armorChange = 0;
    //TODO: this may cause problems if abilities are not properly reset
    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
       // armorChange = bbA.stats.armor;
       // bbA.stats.armor += armorChange;
        //Send to view for armor up! message once that system is in place
    }

    //Right now the actuall prevention of action is handled by checking if a stun is in the characters statuses on BaseBattleActor. 
    public override void DoStatus(BaseBattleActor bbA)
    {
        
    }

    public override void DoStatusEnd(BaseBattleActor bbA)
    {
        //bbA.stats.armor -= armorChange;
    }

    public override void OnAbilityUsed(BaseBattleActor bbA, Ability abilityUsed)
    {
        FinishStatus(this);
    }
}
