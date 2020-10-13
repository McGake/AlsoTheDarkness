using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MultiplyMagicalPower", menuName = "Effects/MultiplyMagicalPower", order = 1)]
public class MultiplyMagicalPower : Status
{
    public StatusValue magicMultiplier;

    private float totalModifier;

    public override void SetReferences(Ability sourceAbility, GameObject de)
    {

    }

    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
        totalModifier = ((bbA.stats.basic.magicalPower * magicMultiplier.val) - bbA.stats.basic.magicalPower) ;
        bbA.stats.modified.magicalPower += totalModifier; 
    }
    public override void DoStatus(BaseBattleActor bbA)
    {

    }
    public override void DoStatusEnd(BaseBattleActor bbA)
    {
        bbA.stats.modified.magicalPower -= totalModifier;
    }
}
