using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "StatusEffects/Heal", order = 1)]
public class Heal : Status
{
    public int healthBuff;

    public override void SetReferences(Ability sourceAbility, GameObject de)
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

