using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InstantDamage", menuName = "StatusEffects/InstandDamage", order = 1)]
public class InstantDamage : Status
{
    #pragma warning disable 649
    [SerializeField] 
    //private float damage;
#pragma warning restore 649

    public StatusValue damage;

    public override void SetModifiers()
    {
        statusValues.Add(damage);
        base.SetModifiers();
    }

    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
        bbA.ChangeHp((int)-damage.val);
    }

    public override void DoStatusEnd(BaseBattleActor bbA)
    {
        
    }
}
