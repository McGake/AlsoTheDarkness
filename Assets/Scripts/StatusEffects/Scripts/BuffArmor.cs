using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BuffArmor", menuName = "Effects/BuffArmor", order = 1)]
public class BuffArmor : Status
{
    public float armorBuff;

    private int totalArmorBuff;

    public override void SetReferences(Ability sourceAbility, GameObject de)
    {

    }

    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
        totalArmorBuff = Mathf.FloorToInt(armorBuff);
        //bbA.modifiedStats.armor += totalArmorBuff; 
    }

    public override void DoStatus(BaseBattleActor bbA)
    {

    }

    public override void DoStatusEnd(BaseBattleActor bbA)
    {
        //bbA.modifiedStats.armor -= totalArmorBuff;
    }
}
