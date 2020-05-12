using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BuffPhysicalPowerAndHeal", menuName = "Effects/BuffPhysicalPowerAndHeal", order = 1)]
public class BuffPhysicalPowerAndHeal : Status
{

    public int healthBuff;

    public int physicalPowerBuff;

    public override void SetUpStatus(Ability sourceAbility, GameObject de)
    {

    }

    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
        bbA.ChangeHp(healthBuff);



        bbA.stats.physicalPower += physicalPowerBuff;
    }
    public override void DoStatus(BaseBattleActor bbA)
    {

    }
    public override void DoStatusEnd(BaseBattleActor bbA)
    {
        bbA.stats.physicalPower -= physicalPowerBuff;
    }
}
