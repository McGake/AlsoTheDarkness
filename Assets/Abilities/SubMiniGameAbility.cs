using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubMiniGameAbility : ScriptableObject
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
