using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy : BaseBattleActor
{
    public override void Update()
    {
        base.Update();
        SimpleAbilitySelector();
    }

    void SimpleAbilitySelector()
    {
        if(AbilityManager.abManager.IsCharacterCurrentlyDoingAbility(gameObject))
        {
            return;
        }
        else
        {
            int randAbilIndx = Random.Range(0, abilities.Count);
            //AbilityManager.abManager.TurnOnAbility(abilities[randAbilIndx]); 
        }
    }

}
