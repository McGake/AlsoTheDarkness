using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Status
{
    public int healthBuff;

    public override void SetUpStatus(Ability sourceAbility, GameObject de)
    {

    }

    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
        bbA.ChangeHp(healthBuff);
    }
    public override void DoStatus(BaseBattleActor bbA)
    {

    }
    public override void DoStatusEnd(BaseBattleActor bbA)
    {
    }
}

