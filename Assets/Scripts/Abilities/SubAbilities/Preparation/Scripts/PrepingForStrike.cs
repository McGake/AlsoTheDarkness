using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PrepingForStrike", menuName = "SubAbilities/Prep/PrepingForStrike", order = 1)]
public class PrepingForStrike : SubAbility
{
    [SerializeField]
    private float prepTime = 0f;

    private float endPrepTime = 0f;
    public override void DoInitialSubAbility(Ability ab)
    {
        endPrepTime = prepTime + Time.time;
        SetNewAnimation("readyAttack", ab);
    }

    public override void DoSubAbility(Ability ab)
    {

        if (endPrepTime <= Time.time)
        {
            EndSubAbility();
            ab.pcAnimator.SetBool("chantSpell", false);
        }
    }
}
