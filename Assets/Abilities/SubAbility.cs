﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SubAbility : ScriptableObject
{
    public virtual void DoInitialSubAbility(Ability ab)
    {
    }
    public virtual void DoSubAbility(Ability ab)
    {
    }
    public virtual void DoFinishSubAbility(Ability ab)
    {
    }
    public virtual void OnSelectionFinished(List<GameObject> selectedObjects)
    {
    }

    public delegate void DelEndProjectileSubAbility();
    public DelEndProjectileSubAbility EndSubAbility; //This is set by the owning ability. call this to make the owning ability go to the next subability

    #region Utilities
    protected void SetNewAnimation(string newAnim, Ability ab)
    {
        if (ab.ActorType == typeof(BattlePC))//TODO: this is temp code that must be modified if we want enemies to be able to use same sub abilities as pcs
        {
            ab.BattleActorView.PlayCharacterAnimation(newAnim);
        }
    }

    protected void SetNewAnimationAtSpeed(string newAnim, Ability ab, float speed)
    {
        if (ab.ActorType == typeof(BattlePC))//TODO: this is temp code that must be modified if we want enemies to be able to use same sub abilities as pcs
        {
            ab.BattleActorView.PlayCharacterAnimationAtSpeed(newAnim,speed);
        }
    }

    protected void EndLastAnimation(Ability ab)
    {
        ab.PCAnimator.SetBool(ab.LastAnimSet, false); ;
    }

    protected void SetNewSpecialAnimation(AnimationClip anim, Ability ab)
    {
        AnimatorOverrideController aOC;
        aOC = ab.PCAnimator.runtimeAnimatorController as AnimatorOverrideController;
        ab.PCAnimator.runtimeAnimatorController = aOC;
        aOC["special"] = anim;
        SetNewAnimation("special", ab);
    }

    protected int GetOwnerFacing(Ability ab)
    {
        if (ab.Owner.transform.rotation.eulerAngles.y > 179f)
        {
            return -1;
        }
        else if (ab.Owner.transform.rotation.eulerAngles.y < 1f)
        {
            return 1;
        }
        return 0;
    }
    #endregion Utilities
}




