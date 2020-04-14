using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Chanting", menuName = "SubAbilities/Prep/Chanting", order = 1)]
public class P_Chanting : SubAbility
{

    [SerializeField]
    private float prepTime = 0f;

    private float endPrepTime=0f;
    public override void DoInitialSubAbility(Ability ab)
    {
        endPrepTime = prepTime + Time.time;
        SetNewAnimation("chantSpell", ab);
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
