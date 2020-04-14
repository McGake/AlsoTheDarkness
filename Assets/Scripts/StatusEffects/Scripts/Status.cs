using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status : ScriptableObject
{
    public bool imediate = false;//TODO: clarify that this is for repeating effects that you want the first repetition to happen imediatly rather than after the interval length. has no effect on code in InitialEffect
    public float intervalLength;

    [HideInInspector]
    public float nextInterval;

    [HideInInspector]
    public float endTime; //TODO: Make a method to set this or maybe even an interface. consider doing same for all hideininspector tags
    /// <summary>
    /// For Instant effects just set this to zero and use the DoStatusInitialEffect method
    /// </summary>
    public float duration;

    public virtual void SetUpStatus(BaseBattleActor bbA)
    {

    }

    public virtual void DoStatusInitialEffect(BaseBattleActor bbA)
    {

    }
    public virtual void DoStatus(BaseBattleActor bbA)
    {

    }
    public virtual void DoStatusEnd(BaseBattleActor bbA)
    {

    }
}
