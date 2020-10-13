using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BuffPhysicalPowerAndHeal", menuName = "Effects/BuffPhysicalPowerAndHeal", order = 1)]
public class BuffPhysicalPowerAndHeal : Status
{

    public StatusValue healthBuff;

    public StatusValue physicalPowerBuff;

    public override void SetModifiers()
    {
        statusValues.Add(healthBuff);
        statusValues.Add(physicalPowerBuff);
        base.SetModifiers();
    }

    public override void SetReferences(Ability sourceAbility, GameObject de)
    {

    }

    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
        bbA.ChangeHp(healthBuff.val);
        //bbA.stats.physicalPower += physicalPowerBuff.val;
        bbA.battleActorView.ShowBuff(physicalPowerBuff.val, "PWR", BuffStyle.Positive);
    }
    public override void DoStatus(BaseBattleActor bbA)
    {

    }
    public override void DoStatusEnd(BaseBattleActor bbA)
    {
        //bbA.stats.physicalPower -= physicalPowerBuff.val;
    }
}
