using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PCDown", menuName = "StatusEffects/Down", order = 1)]
public class PCDown : Status //TODO: perhaps to turn this and things like this to an event that the battle actor listens for and self applies 
{

    public override void SetReferences(Ability sourceAbility, GameObject deliveryObject)
    {
    }

    //TODO: this may cause problems if abilities are not properly reset
    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {

        bbA.battleActorView.PlayInteruptingCharacterAnimation("down");
        bbA.gameObject.GetComponent<Collider2D>().enabled = false;
        AbilityManager.abManager.StopAllAbilitiesFromCharacter(bbA.gameObject);
    }

    //Right now the actuall prevention of action is handled by checking if a stun is in the characters statuses on BaseBattleActor. 
    public override void DoStatus(BaseBattleActor bbA)
    {

    }

    public override void DoStatusEnd(BaseBattleActor bbA)
    {

        bbA.OnDeathCallback(bbA.gameObject);
        bbA.gameObject.GetComponent<Collider2D>().enabled = true;

        bbA.battleActorView.StopShowStatus(this);
        if (IsOnlyStatusOfType(bbA))
        {
            CleanUpStatus(bbA);
        }
    }
}
