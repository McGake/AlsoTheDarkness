using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BuffArmor", menuName = "StatusEffects/BuffArmor", order = 1)]
public class BuffArmor : Status
{
    public float armorBuff;

    private float totalArmorBuff;
    public override void SetReferences(Ability sourceAbility, GameObject de)
    {

    }

    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
        totalArmorBuff = Mathf.FloorToInt(armorBuff);
        bbA.stats.modified.armor += totalArmorBuff; 
    }

    public override void DoStatus(BaseBattleActor bbA)
    {

    }

    public override void DoStatusEnd(BaseBattleActor bbA)
    {
        bbA.stats.modified.armor -= totalArmorBuff;
    }
}
