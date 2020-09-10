using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

[CreateAssetMenu(fileName = "Attacking", menuName = "SubAbilities/Prep/Attacking", order = 1)]
public class Attacking : SubAbility
{
    //TODO: make this able to be played faster or slower by setting the current animation speed;
    private bool skipFrame = true;
    public override void DoInitialSubAbility(Ability ab)
    {
        SetNewAnimation("attack",ab);
        skipFrame = true;
    }

    public override void DoSubAbility(Ability ab)
    {
        if (skipFrame == false)
        {
            if (ab.PCAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .8f && ab.PCAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack")) //TODO: make this an event on the animation or just find some better way to do this
            {
                EndLastAnimation(ab);
                EndSubAbility();
            }
        }
        else
        {
            skipFrame = false;
        }
    }
}
