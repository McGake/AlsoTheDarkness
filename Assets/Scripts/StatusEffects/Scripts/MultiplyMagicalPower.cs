using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MultiplyMagicalPower", menuName = "StatusEffects/MultiplyMagicalPower", order = 1)]
public class MultiplyMagicalPower : Status
{
    public StatusValue magicMultiplier;

    private float totalModifier;

     public override void SetModifiers()
    {
        statusValues.Add(magicMultiplier);
        base.SetModifiers();
    }

    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {

        Debug.Log("magic mult val " + magicMultiplier.val);
        Debug.Log("magic power " + bbA.stats.basic.magicalPower);

        Debug.Log("power x magic " + bbA.stats.basic.magicalPower * magicMultiplier.val);

        Debug.Log("magic mult val again " + magicMultiplier.val);
        Debug.Log("magic power again" + bbA.stats.basic.magicalPower);

        Debug.Log("whole thing " + ((bbA.stats.basic.magicalPower * magicMultiplier.val) - bbA.stats.basic.magicalPower));
        totalModifier = ((bbA.stats.basic.magicalPower * magicMultiplier.val) - bbA.stats.basic.magicalPower) ;
        Debug.Log("total modifier " + totalModifier);
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
