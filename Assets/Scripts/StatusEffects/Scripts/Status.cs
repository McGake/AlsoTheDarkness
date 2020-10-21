using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private List<DurationType> inspectorDurationTypes = new List<DurationType>();
    
    private Dictionary<Type,DurationType> durationTypes = new Dictionary<Type, DurationType>();

    public delegate void DelFinishStatus(Status status);
    public DelFinishStatus FinishStatus; //TODO: there is no reason to do it this way. I can simply have a finish status function on the basebattleactor.

    protected BattleStats stats;

    public List<StatusValue> statusValues = new List<StatusValue>();

    private const float percentCoversion = .01f;

    private void Awake()
    {
        for(int i = 0; i < inspectorDurationTypes.Count; i++)
        {
            durationTypes.Add(inspectorDurationTypes[i].GetType(), inspectorDurationTypes[i]);
        }
    }

    public Status CreateStatusInstance(BattleStats curModifiedStats)
    {
        Status statusToCreate = Instantiate(this);//TODO: this may be creating scriptable objects that never get destroyed

        statusToCreate.stats = curModifiedStats;
        statusToCreate.SetModifiers();
        return statusToCreate;
    }
    public Status CreateStatusInstance()
    {
        Status statusToCreate = Instantiate(this);//TODO: this may be creating scriptable objects that never get destroyed
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

    public bool HasDurationType(Type type)
    {
        if(durationTypes.ContainsKey(type))
        {
            return true;
        }
        return false;
    }

    public T GetDurationOfType<T>() where T : DurationType
    {
        DurationType returnDT;
        durationTypes.TryGetValue(typeof(T), out returnDT);
        if(returnDT is T)
        {
            return returnDT as T;
        }
        return null;
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

//public enum DurationType
//{
//    time,
//    instant,
//    unequip,
//    battleEnd,
//    externalSource,
//    selfDefined,
//}





