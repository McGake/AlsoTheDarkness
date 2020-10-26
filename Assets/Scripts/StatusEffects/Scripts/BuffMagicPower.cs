using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffMagicPower", menuName = "StatusEffects/BuffMagicPower", order = 1)]
public class BuffMagicPower : Status
{
    public float magicPowerBuff;

    private float finalMagicPowerBuff;
    public override void SetReferences(Ability sourceAbility, GameObject de)
    {

    }

    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
        finalMagicPowerBuff = Mathf.FloorToInt(magicPowerBuff);
        bbA.stats.modified.magicalPower += finalMagicPowerBuff;
    }

    public override void DoStatus(BaseBattleActor bbA)
    {

    }

    public override void DoStatusEnd(BaseBattleActor bbA)
    {
        bbA.stats.modified.magicalPower -= finalMagicPowerBuff;
    }
}
