using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Victory", menuName = "StatusEffects/Victory", order = 1)]
public class Victory : Status //TODO: perhaps to turn this and things like this to an event that the battle actor listens for and self applies 
{

    public override void SetReferences(Ability sourceAbility, GameObject deliveryObject)
    {
    }

    //TODO: this may cause problems if abilities are not properly reset
    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
        //bbA.battleActorView.ShowStatus(this);
        bbA.battleActorView.PlayInteruptingCharacterAnimation("victory");
        AbilityManager.abManager.StopAllAbilitiesFromCharacter(bbA.gameObject);
        SetAbilityUseablility(false, bbA);
        bbA.stats.modified.exp += stats.modified.exp;

        if(bbA.stats.modified.exp >= bbA.stats.modified.nextLevel)
        {
            bbA.LevelUp();
        }

    }

    public override void DoStatus(BaseBattleActor bbA)
    {
        
    }

    public override void DoStatusEnd(BaseBattleActor bbA)
    {
        if (IsOnlyStatusOfType(bbA))
        {
            SetAbilityUseablility(true, bbA);
            CleanUpStatus(bbA);
        }
    }

    private void SetAbilityUseablility(bool useableValue, BaseBattleActor bbA)
    {
        for (int i = 0; i < bbA.abilities.Count; i++)
        {
            if (useableValue == true)
            {
                bbA.abilities[i].RemoveUsePreventor(this);
            }
            else if (useableValue == false)
            {
                bbA.abilities[i].AddUsePreventor(this);
            }
        }
    }
}
