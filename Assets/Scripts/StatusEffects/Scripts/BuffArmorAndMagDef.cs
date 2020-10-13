using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BuffArmorAndMagDef", menuName = "Effects/BuffArmorAndMagDef", order = 1)]
public class BuffArmorAndMagDef : Status
{
    public float armorBuff;

    public float magDefBuff;//Does not work yet

    private int totalArmorBuff;

    private float totalMagDefBuff;

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
