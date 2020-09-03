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

    public bool dontAddStatus = false;

    public delegate void DelFinishStatus(Status status);
    public DelFinishStatus FinishStatus;

    public virtual void SetUpStatus(Ability sourceAbility, GameObject deliveryObject)
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

    public virtual bool OnStatusAdded(BaseBattleActor bbA, Status statusAdded)
    {
        return true;
    }

    public virtual void OnAbilityUsed(BaseBattleActor bbA, Ability abilityUsed)
    {

    }

    public void CleanUpStatus(BaseBattleActor bbA)
    {
        bbA.battleActorView.StopShowStatus(this);//otherwise stop showing the status. Consider doing all this just after status removeal?
    }

    public bool IsOnlyStatusOfType(BaseBattleActor bbA)
    {
        foreach (Status status in bbA.curStatuses) //if there is another status of this type and if it is not actually this status and if the status is not going to end on the same frame as this then don't do anything because one of the statuses is still ongoing
        {
            if (status.GetType() == this.GetType())
            {
                if (status != this)
                {
                    if (status.endTime > Time.time)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}
