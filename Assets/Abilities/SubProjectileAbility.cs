using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubProjectileAbility : ScriptableObject
{

    public delegate void DelEndProjectileSubAbility();
    public DelEndProjectileSubAbility EndProjectileSubAbility;

    public virtual void DoInitialProjectileSubAbility(ProjectileAbility pa)
    {

    }

    public virtual void DoFinishProjectileSubAbility(ProjectileAbility pa)
    {

    }

    public virtual void DoProjectileSubAbility(ProjectileAbility projectileAbility)
    {
    }
}
