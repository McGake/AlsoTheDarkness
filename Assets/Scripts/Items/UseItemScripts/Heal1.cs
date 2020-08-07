using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "Items/Heal", order = 1)]
public class Heal1 : UseItemScript
{
    public int healthToAdd;
    public override void DoSelectionBehavior()
    {
        
    }


    public override void UseItem(PC pcToUse)
    {
        pcToUse.battler.GetComponent<BaseBattleActor>().stats.hP += healthToAdd;

        if(pcToUse.battler.GetComponent<BaseBattleActor>().stats.hP > pcToUse.battler.GetComponent<BaseBattleActor>().stats.maxHP)
        {
            pcToUse.battler.GetComponent<BaseBattleActor>().stats.hP = pcToUse.battler.GetComponent<BaseBattleActor>().stats.maxHP;
        }
    }
}
