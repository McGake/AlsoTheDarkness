using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MultiplyMagicalPower", menuName = "Effects/MultiplyMagicalPower", order = 1)]
public class MultiplyMagicalPower : Status
{
    public float magicMultiplier;

    private int totalModifier;

    public override void SetReferences(Ability sourceAbility, GameObject de)
    {

    }

    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
        totalModifier = Mathf.FloorToInt(bbA.stats.magicalPower * magicMultiplier);
        bbA.modifiedStats.magicalPower += totalModifier; 
    }
    public override void DoStatus(BaseBattleActor bbA)
    {

    }
    public override void DoStatusEnd(BaseBattleActor bbA)
    {
        bbA.modifiedStats.magicalPower -= totalModifier;
    }
}
