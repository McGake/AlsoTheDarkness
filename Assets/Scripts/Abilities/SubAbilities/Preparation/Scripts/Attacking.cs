using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Attacking", menuName = "SubAbilities/Prep/Attacking", order = 1)]
public class Attacking : SubAbility
{
    //TODO: make this able to be played faster or slower by setting the current animation speed;
    public override void DoInitialSubAbility(Ability ab)
    {
        SetNewAnimation("attack", ab);
    }

    public override void DoSubAbility(Ability ab)
    {
        if (ab.pcAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && ab.pcAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack")) //TODO: turn this into a utility that can call back on end of animation or just check on update
        {
            SetNewAnimation("stand", ab);
            Debug.Log("attack notices over");
            EndSubAbility();           
        }
    }
}
