﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;


[System.Serializable]
public class Stats
{
    public float maxHP;
    public float hP;

    public float maxMana;
    public float mana;

    public float speed;
    public float quickness;

    public float magicalPower;
    public float magicalSkill;

    public float physicalSkill;
    public float physicalPower;

    public float armor;

    public Stats Copy()
    {
        Stats tempStats = new Stats();

        tempStats.maxHP = maxHP;
        tempStats.hP = hP;

        tempStats.maxMana = maxMana;
        tempStats.mana = mana;

        tempStats.speed = speed;
        tempStats.quickness = quickness;

        tempStats.magicalPower = magicalPower;
        tempStats.magicalSkill = magicalSkill;

        tempStats.physicalSkill = physicalSkill;
        tempStats.physicalPower = physicalPower;

        tempStats.armor = armor;

        return tempStats;
    }
}

[System.Serializable]
public class BattleStats
{
    public Stats basic;

    public Stats modified;

    public BattleStats Copy()
    {
        BattleStats tempStats = new BattleStats();

        tempStats.basic = basic;
        tempStats.modified = modified;

        return tempStats;
    }

    public float GetGoverningStat(GoverningStat gS)
    {
        switch (gS)
        {
            case GoverningStat.magicalPower:
                return modified.magicalPower;
                break;
            case GoverningStat.magicalSkill:
                return modified.magicalSkill;
                break;
            case GoverningStat.physicalPower:
                return modified.physicalPower;
                break;
            case GoverningStat.physicalSkill:
                return modified.physicalSkill;
                break;
            case GoverningStat.none:
                return 1f;
                break;
        }

        Debug.LogError("we somehow searched for a governing stat that is not accounted for");
        return 0f;
    }

}

[System.Serializable]
public class BaseBattleActor :MonoBehaviour
{
    public string displayName;

    public BattleActorView battleActorView;

    public BattleStats stats;

    public List<Status> curStatuses = new List<Status>();

    public List<Ability> inspectorAbilities = new List<Ability>();

    [System.NonSerialized]
    public List<Ability> abilities = new List<Ability>();

    public delegate void DelShowDamage();
    public DelShowDamage ShowDamage;

    public delegate void DelShowBuff();
    public DelShowBuff ShowBuff;

    public delegate void DelOnDeathCallback(GameObject gO);
    public DelOnDeathCallback OnDeathCallback;

    public Status deathStatus;

    public virtual void Awake()
    {
        SetUpAbilities();
        stats.modified = stats.basic.Copy(); //TODO: set up status system to re add all stat effecting statuses on start

    }

    public void OnEnable()
    {
        battleActorView.UpdateHealthBar(stats.modified.hP, stats.modified.maxHP);
    }

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
        status.endTime = Time.time + status.duration;
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
            else if(IsTimeToRunStatus(curStatuses[i]))
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

    private bool IsStatusFinished(Status status)
    {
        if (status.endTime <= Time.time)
        {
            return true;
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

    public bool HasStatus(Status status)
    {
        for (int i = 0; i<curStatuses.Count; i++)
        {
            Debug.Log(status);

            Debug.Log(curStatuses);

            Debug.Log("status count " +curStatuses.Count);

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
        if (!IsStuned())//TODO this is no longer the right way to do this. add this functionality to the Stuned OnAbilityStarted override
        {
            for (int i = 0; i < curStatuses.Count; i++)
            {
                curStatuses[i].OnAbilityUsed(this, aB);//TODO: change this to an event system
            }
            AbilityManager.abManager.TurnOnAbility(aB);
        }
    }

    public bool IsStuned() //TODO: when we have a better grasp of what kind of status effects are going to be in our game, generalize this. We could , for example, have a function that runs a snipet of code from every status called ModifyAbility()
    {
        foreach(Status status in curStatuses)
        {
            if(status is Stun)
            {
                return true;
            }
        }
        return false;
    }

    #endregion AbilityManagement


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


    public virtual void Die()
    {
        OnDeathCallback(gameObject);
        AddStatus(deathStatus);
        //gameObject.SetActive(false); //TODO: flesh this out with arbitrary animation and make it part of a pooling system. PC's and monsters will of course have their own thing but should implement the base class if possible.
    }
}












