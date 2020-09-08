using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackingAtSpeed", menuName = "SubAbilities/Prep/AttackingAtSpeed", order = 1)]
public class AttackAtSpeed : SubAbility
{
    //TODO: make this able to be played faster or slower by setting the current animation speed;
    public float playSpeed = 2.2f;
    private bool skipFrame = true;
    public override void DoInitialSubAbility(Ability ab)
    {
        SetNewAnimationAtSpeed("attack", ab, playSpeed);
        skipFrame = true;
    }

    public override void DoSubAbility(Ability ab)
    {
        if (skipFrame == false)
        {
            if (ab.PCAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .8f && ab.PCAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack")) //TODO: make this an event on the animation or just find some better way to do this
            {
                
                Debug.Log("ended");
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
