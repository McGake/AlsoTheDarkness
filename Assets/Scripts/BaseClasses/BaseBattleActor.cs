﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleStats
{
    public int maxHP;
    public int hP;

    public int maxMana;
    public int mana;

    public int speed;
    public int quckness;

    public int magicalPower;
    public int magicalSkill;

    public int physicalSkill;
    public int physicalPower;

    public int armor;

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

   


    void Awake()
    {
        stats.hP = stats.maxHP;
        SetUpAbilities();

    }

    void Start()
    {
        


    }

    private void SetUpAbilities()
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

        SetAbilityUsable();//TODO:temp
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
        curStatuses.Add(status);
        status.SetUpStatus(this);
        status.DoStatusInitialEffect(this); 
    }

    public virtual void RunStatuses()
    {
        for(int i = 0; i<curStatuses.Count; i++)
        {
            if(IsStatusFinished(curStatuses[i]))
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

    #endregion StatusManagement

    //This regoin should be temporary till i move functionality to abilities themselves
    #region AbilityManagement 
    void SetAbilityUsable()
    {
        foreach (Ability ab in abilities) //TODO: make this a part of the ability itself. Why should it be handled here? 
        {
            ab.useable = true;
            if (ab.curCooldownEndTime > Time.time)
            {
                ab.useable = false;
            }
        }
    }
    #endregion AbilityManagement


    public virtual void Die()
    {
        GameObject.Destroy(gameObject); //TODO: flesh this out with arbitrary animation and make it part of a pooling system. PC's and monsters will of course have their own thing but should implement the base class if possible.
    }
}









