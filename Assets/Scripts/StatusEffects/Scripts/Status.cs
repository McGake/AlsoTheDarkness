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

    protected BattleStats stats;

    public List<StatusValue> statusValues;

    private const float percentCoversion = .01f;

    public Status CreateStatusInstance(BattleStats curStats)
    {
        Status statusToCreate = Instantiate(this);
        statusToCreate.stats = curStats;
        statusToCreate.SetModifiers();
        return statusToCreate;
    }

    public virtual void SetReferences(Ability sourceAbility, GameObject deliveryObject)
    {
        stats = sourceAbility.stats.Copy();
    }
    /// <summary>
    /// multiplies a base value of a status effect (say damage for example) 
    /// by the designated governing stat (say physical power for example) converted to a percentage. (so a stat value of 18 is multipled by .01 making it .18)
    /// One is added to the percentage so that the stats add to the base vaule not divide it. (So .18 becomes 1.18 which when multiplied works as 18 percent increase)
    /// Then the that is multiplied by an overall modifier to determin how strong of an impact the stats have on the base value. So a multiplier of 1 means that a stat of 18 will add 18 percent to the
    /// </summary>
    public virtual void SetModifiers()
    {
        foreach(StatusValue sVal in statusValues)
        {
            float governingStatValue = stats.GetGoverningStat(sVal.governingStat);


            sVal.val = 
                sVal.baseValue * 
                (((governingStatValue * sVal.multiplyer) * 
                percentCoversion)+1)
                ;
        }
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

public enum GoverningStat
{
    none,
    magicalPower,
    magicalSkill,
    physicalPower,
    physicalSkill,
}

[System.Serializable]
public class StatusValue
{
    public string label;
    public GoverningStat governingStat;

    public float baseValue;
    public float multiplyer = 1f;
    public float val;
}