using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BuffMagicSkill", menuName = "StatusEffects/BuffMagicSkill", order = 1)]
public class BuffMagicSkill : Status
{
    public float magicSkillBuff;

    private float finalMagicSkillBuff;
    public override void SetReferences(Ability sourceAbility, GameObject de)
    {

    }

    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
        finalMagicSkillBuff = Mathf.FloorToInt(magicSkillBuff);
        bbA.stats.modified.magicalSkill += finalMagicSkillBuff;
    }

    public override void DoStatus(BaseBattleActor bbA)
    {

    }

    public override void DoStatusEnd(BaseBattleActor bbA)
    {
        bbA.stats.modified.magicalSkill -= finalMagicSkillBuff;
    }
}