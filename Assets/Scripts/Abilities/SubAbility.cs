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
        ab.pcAnimator.SetBool(ab.lastAnimSet, false);
        ab.pcAnimator.SetBool(newAnim, true);
        ab.lastAnimSet = newAnim;
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
