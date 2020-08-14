using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SubAbility : ScriptableObject
{
    public delegate void DelEndProjectileSubAbility();
    public DelEndProjectileSubAbility EndSubAbility; //This is set by the owning ability. call this to make the owning ability go to the next subability

    public virtual void DoInitialSubAbility(Ability ab)
    {

    }

    public abstract void DoSubAbility(Ability ab);

    protected void SetNewAnimation(string newAnim, Ability ab)
    {
        if (ab.actorType == typeof(BattlePC))//TODO: this is temp code that must be modified if we want enemies to be able to use same sub abilities as pcs
        {
            ab.battleActorView.PlayCharacterAnimation(newAnim);

        }
    }

    protected void SetNewAnimationAtSpeed(string newAnim, Ability ab, float speed)
    {

        if (ab.actorType == typeof(BattlePC))//TODO: this is temp code that must be modified if we want enemies to be able to use same sub abilities as pcs
        {
            ab.battleActorView.PlayCharacterAnimationAtSpeed(newAnim,speed);

        }
    }

    protected void EndLastAnimation(Ability ab)
    {
        ab.pcAnimator.SetBool(ab.lastAnimSet, false); ;
    }

    protected void SetNewSpecialAnimation(AnimationClip anim, Ability ab)
    {
        AnimatorOverrideController aOC;
        aOC = ab.pcAnimator.runtimeAnimatorController as AnimatorOverrideController;
        Debug.Log("aoc is " + aOC.name);
        ab.pcAnimator.runtimeAnimatorController = aOC;
        Debug.Log(aOC["special"].name);
        Debug.Log(anim.name);
        aOC["special"] = anim;
        SetNewAnimation("special", ab);
    }   

    public virtual void DoFinishSubAbility(Ability ab)
    {

    }

    public virtual void OnSelectionFinished(List <GameObject> selectedObjects)
    {

    }

}

public abstract class SubProjectileAbility:ScriptableObject
{

    public delegate void DelEndProjectileSubAbility();
    public DelEndProjectileSubAbility EndProjectileSubAbility;

    public virtual void DoInitialProjectileSubAbility(ProjectileAbility pa)
    {

    }

    public virtual void DoFinishProjectileSubAbility(ProjectileAbility pa)
    {

    }

    public abstract void DoProjectileSubAbility(ProjectileAbility projectileAbility);
}

public abstract class SubMiniGameAbility:ScriptableObject
{
    public delegate void DelEndProjectileSubAbility();
    public DelEndProjectileSubAbility EndProjectileSubAbility;

    public virtual void DoInitialProjectileSubAbility(SubMiniGameAbility mga)
    {

    }

    public virtual void DoFinishProjectileSubAbility(SubMiniGameAbility mga)
    {

    }

    public abstract void DoProjectileSubAbility(SubMiniGameAbility mga);
}
