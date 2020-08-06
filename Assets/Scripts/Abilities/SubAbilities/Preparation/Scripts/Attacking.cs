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
        ab.pcAnimator.Play("attack",0,0f);
        skipFrame = true;
    }

    public override void DoSubAbility(Ability ab)
    {
        if (skipFrame == false)
        {
            if (ab.pcAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .7f && ab.pcAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack")) //TODO: make this an event on the animation or just find some better way to do this
            {
                Debug.Log("ended");
                EndSubAbility();
            }
        }
        else
        {
            skipFrame = false;
        }
        Debug.Log("doing attack ability " + ab.pcAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime + " " + ab.pcAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack"));
    }
}
