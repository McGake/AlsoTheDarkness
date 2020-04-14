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
            //AbilityManager.abManager.TurnOnAbility(newAbilities[randAbilIndx], gameObject); 
        }
    }

    //private void UpdateAbilitiesUseability()
    //{
    //    foreach (Ability ab in abilities)
    //    {
    //        ab.castable = true;
    //        if (Time.time <= ab.curCooldownEndTime)
    //        {
    //            ab.castable = false;
    //        }
    //    }
    //}
}
