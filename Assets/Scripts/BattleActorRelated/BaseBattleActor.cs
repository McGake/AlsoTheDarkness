using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BaseBattleActor :MonoBehaviour
{
    public string displayName;
    public BattleActorView battleActorView;
    public BattleStats stats;
    public List<Status> curStatuses = new List<Status>();
    public List<Ability> inspectorAbilities = new List<Ability>();
    [System.NonSerialized]public List<Ability> abilities = new List<Ability>();

    public delegate void DelOnDeathCallback(GameObject gO);
    public DelOnDeathCallback OnDeathCallback;

    public Status deathStatus;
    
    public virtual void Awake()
    {
        SetUpAbilities();
        SetUpStats();
    }
    #region Setup
    protected virtual void SetUpAbilities()
    {
        foreach (Ability aB in inspectorAbilities)
        {
            abilities.Add(Instantiate(aB));
        }

        foreach (Ability aB in abilities)
        {
            aB.SetupAbility(gameObject);
        }
    }

    public void SetUpStats()
    {
        stats.modified = stats.basic.Copy();
    }
    #endregion Setup

    public void OnEnable()
    {
        battleActorView.UpdateHealthBar(stats.modified.hP, stats.modified.maxHP);
    }

    public virtual void Update()
    {
        RunStatuses();
    }
    #region StatusManagement
    public virtual void AddStatus(Status status)
    {
        if (status.imediate == true)
        {
            status.nextInterval = 0f;
        }
        else
        {
            status.nextInterval = Time.time + status.intervalLength;
        }

        if (status.HasDurationType(typeof(TimeElapsed)))
        {
            TimeElapsed tE = status.GetDurationOfType<TimeElapsed>();
            tE.endTime = Time.time + tE.timeDuration;
        }

        status.FinishStatus = FinishStatus;
        for (int i = 0; i < curStatuses.Count; i++)//TODO: This might not be needed
        {
           if( curStatuses[i].OnStatusAdded(this, status) == false)
            {
                return;
            }
        }
        curStatuses.Add(status);

        status.DoStatusInitialEffect(this); 
    }

    public virtual void RunStatuses()
    {
        for(int i = 0; i<curStatuses.Count; i++)
        {
            if(IsStatusFinished(curStatuses[i])) //TODO: Statuses should probably finish themselves. Not all statuses will finish due to time
            {
                EndStatus(curStatuses[i]);
                RemoveStatus(curStatuses[i]);
                
            }            
            else if(IsTimeToRunStatus(curStatuses[i]))//TODO: this also should probably be handled on the status except for an update like check perhaps
            {
                RunStatus(curStatuses[i]);
            }
        }
    }

    public void FinishStatus(Status status)
    {
        EndStatus(status);
        RemoveStatus(status);
    }

    private bool IsStatusFinished(Status status) //TODO: wheather a status is finished should be determined and checked on the status itself.
    {
        if (status.HasDurationType(typeof(TimeElapsed)))
        {
            TimeElapsed tE = status.GetDurationOfType<TimeElapsed>();
            if(tE.endTime <= Time.time)
            {
                return true;
            }
            return false;
        }
        else return false;
    }

    private void EndStatus(Status status)
    {
        status.DoStatusEnd(this);
    }

    private void RemoveStatus(Status status)
    {
        curStatuses.Remove(status);
    }

    public void FinishAllStatusesOfType(Status status)
    {
        for(int i = curStatuses.Count-1; i >= 0; i--)
        {
            if(curStatuses[i].GetType() == status.GetType())
            {
                FinishStatus(curStatuses[i]);
            }
        }
    }

    private bool IsTimeToRunStatus(Status status)
    {
        if(status.nextInterval <= Time.time)
        {
            return true;
        }
        return false;
    }

    private void RunStatus(Status status)
    {
        status.nextInterval = Time.time + status.intervalLength;
        status.DoStatus(this);
    }

    public bool HasStatus(Status status)//TODO: this seems like a bad setup. if we need to use this then it should probably be something the status itself does.
    {
        for (int i = 0; i<curStatuses.Count; i++)
        {
            System.Type type1 = status.GetType();
            System.Type type2 = curStatuses[i].GetType();
            if (type1 == type2)
            {
                return true;
            }
        }
        return false;
    }

    #endregion StatusManagement

    //This regoin should be temporary till i move functionality to abilities themselves
    #region AbilityManagement 
    public void DoAbility(Ability aB)
    {
         for (int i = 0; i < curStatuses.Count; i++)
         {
             curStatuses[i].OnAbilityUsed(this, aB);//TODO: change this to an event system
         }
         AbilityManager.abManager.TurnOnAbility(aB);
    }
    #endregion AbilityManagement

    #region ExternalMethods
    public void TakePhysicalDamage(float amount)
    {
        TakePhysicalDamage(amount, 0f);
    }

    public void TakePhysicalDamage(float amount, float penAmount)
    {
        const float armorMultiplyer = .5f;
        amount -= (amount * (stats.modified.armor * .01f * armorMultiplyer));
        amount += penAmount;
        ChangeHp(-amount);
    }

    public void ChangeHp(float amount)
    {
        stats.modified.hP += amount;

        if (stats.modified.hP > stats.modified.maxHP)
        {
            stats.modified.hP = stats.modified.maxHP;
        }
        if (stats.modified.hP <= 0)
        {
            Die();
        }
        if(amount <0)
        {
            battleActorView.ShowDamage(amount);
            battleActorView.StartFlash();
        }
        else if(amount >=0)
        {
            battleActorView.ShowHeal(amount);
        }
        battleActorView.UpdateHealthBar(stats.modified.hP, stats.modified.maxHP);
    }

    public void EndBattle()
    {
        for (int i = curStatuses.Count -1; i >= 0; i--)
        {
            if (curStatuses[i].HasDurationType(typeof(BattleEnd)))
            {
                curStatuses[i].FinishStatus(curStatuses[i]);
            }
        }
    }

    public void LevelUp()
    {
        stats.modified.exp = stats.modified.exp - stats.modified.nextLevel;
        stats.modified.nextLevel = stats.modified.nextLevel * stats.modified.expLevelModifier;
        stats.basic.AddOnLevelUp(stats.increaseRate);
        stats.modified.AddOnLevelUp(stats.increaseRate);
        stats.modified.level++;
    }

    public virtual void Die()
    {
        Debug.Log("death " + gameObject.name);
        OnDeathCallback(gameObject);
        AddStatus(deathStatus);
        //gameObject.SetActive(false); //TODO: flesh this out with arbitrary animation and make it part of a pooling system. PC's and monsters will of course have their own thing but should implement the base class if possible.
    }
    #endregion ExternalMethods
}












