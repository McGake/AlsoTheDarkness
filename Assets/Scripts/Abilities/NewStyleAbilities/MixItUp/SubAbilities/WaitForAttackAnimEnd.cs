﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WaitForAttackAnimEnd", menuName = "SubAbilities/Prep/WaitForAttackAnimEnd", order = 1)]
public class WaitForAttackAnimEnd : SubAbility
{
    public override void DoSubAbility(Ability ab)
    {
            if (ab.pcAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack") && ab.pcAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .99f) 
            {
                Debug.Log("ended attack animat");
                EndSubAbility();
            }
    }
}