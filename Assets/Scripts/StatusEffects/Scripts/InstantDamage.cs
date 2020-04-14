using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InstantDamage", menuName = "StatusEffects/InstandDamage", order = 1)]
public class InstantDamage : Status
{
    #pragma warning disable 649
    [SerializeField] 
    private float damage;
    #pragma warning restore 649

    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
        bbA.stats.hP -= (int)damage;
        //bbA.battleActorView.ShowDamage((int)damage);
    }    
}
