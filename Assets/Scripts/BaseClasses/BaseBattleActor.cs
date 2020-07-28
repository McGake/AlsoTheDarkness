using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public BattleStats modifiedStats;

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


    public void ChangeHp(int amount)
    {
        stats.hP += amount;

        if (stats.hP > stats.maxHP)
        {
            stats.hP = stats.maxHP;
        }



        if (stats.hP <= 0)
        {
            Die();
        }

        if(amount <0)
        {
            battleActorView.ShowDamage(amount);
            battleActorView.StartBlink();
        }
    }


    public virtual void Die()
    {
        OnDeathCallback(gameObject);
        AddStatus(deathStatus);
        gameObject.SetActive(false); //TODO: flesh this out with arbitrary animation and make it part of a pooling system. PC's and monsters will of course have their own thing but should implement the base class if possible.
    }
}












