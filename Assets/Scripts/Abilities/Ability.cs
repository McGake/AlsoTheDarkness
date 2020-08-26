using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public enum UsableContexts
{
    none = 0,
    battleAbilityMenu = 1,
    partyMenu = 2,
    passive = 3,
    projectile = 4,
}



[System.Serializable]
[CreateAssetMenu(fileName = "NameThisAbility", menuName = "ScriptableObjects/Ability", order = 1)]
public class Ability:ScriptableObject
{
    [SerializeField]
    private string _displayName;
    public string DisplayName { get { return _displayName; } set { _displayName = value; } }

    public Animator pcAnimator { get; private set; }

    public BattleActorView battleActorView { get; private set; }

    public bool abilityOver { get; private set; } = true;

    public bool useable { get; set; } = true; //change name of this variable to better indicate that it is checking wheather anything is preventing this ability from being used including cooldown

    public int maxUses;

    public int uses = 0;
    public GameObject owner { get; protected set; }

    public float cooldownTime;

    public float curCooldownEndTime { get; private set; } = 0f;

    public string lastAnimSet { get; set; } = "stand";

    public GameObject singleObjectTarget //this avoids using a majic number in every subability that only has one target while also avoiding having a seperate variable for single and multiple targets. In other words, single targeting abilities always use the first object slot in objectTargets
    {
        get
        {
            return objectTargets[0];
        }
        set
        {
            objectTargets[0] = value;
        }
    }

    [HideInInspector]
    public List<GameObject> objectTargets = new List<GameObject>();
    [HideInInspector]
    public List<Vector3> positionTargets = new List<Vector3>();

    public Action projectileCallbacks { get; private set; }

    [SerializeField]
    public List<SubAbility> inspectorSubAbilities;

    [System.NonSerialized]
    private List<SubAbility> subAbilities = new List<SubAbility>();

    [HideInInspector]
    public int curSubAbilityIndx = 0;

    public delegate void DelStartSelectFromPCs(SubAbility subAb,Type requesterType);
    public DelStartSelectFromPCs StartSelectFromPCs; //TODO: this is temporary untill I can put in a proper event system

    public delegate void DelStartSelectFromEnemies(SubAbility subAb, Type requesterType);
    public DelStartSelectFromEnemies StartSelectFromEnemies;

    public delegate void DelStartSelectAllPCs(SubAbility subAb, Type requesterType);
    public DelStartSelectAllPCs StartSelectAllPCs;

    public delegate void DelStartSelectAllEnemies(SubAbility subAb, Type requesterType);
    public DelStartSelectAllEnemies StartSelectAllEnemeies;

    public delegate void DelStartSelectAllPCsButCurrent(SubAbility subAb, Type requesterType);
    public DelStartSelectAllPCsButCurrent StartSelectAllPCsButCurrent;

    public delegate void DelKickOffMinigame(MiniGame mG);
    public DelKickOffMinigame KickOffMiniGame;

    public Type actorType;

    public delegate GameObject DelInstantiateInWorldSpaceCanvas(GameObject go, Vector3 position);
    public DelInstantiateInWorldSpaceCanvas InstantiateInWorldSpaceCanvas;

    
    public void SubscribeToProjectileCallback(Action callbackToSubscribe)
    {
        projectileCallbacks += callbackToSubscribe;
    }

    public void UnsubscribeToProjectileCallback(Action callbackToUnsubscribe)
    {
        projectileCallbacks -= callbackToUnsubscribe;
    }

    public void SetupAbility(GameObject inOwner)
    {
        owner = inOwner;
        pcAnimator = owner.GetComponent<Animator>();
        battleActorView = owner.GetComponent<BattleActorView>();
        if(owner.GetComponent<BaseEnemy>() != null)
        {
            actorType = typeof(BaseEnemy);
        }
        else if(owner.GetComponent<BattlePC>() != null)
        {
            actorType = typeof(BattlePC);
        }
        subAbilities.Clear();
        
        abilityOver = true;
        useable = true;
        curCooldownEndTime = 0f;
        lastAnimSet = "stand";
        curSubAbilityIndx = 0;
        Debug.Log("ability: " + DisplayName);
        for(int i = 0; i < inspectorSubAbilities.Count; i++)
        {
            subAbilities.Add(Instantiate(inspectorSubAbilities[i]));
        }

        
    }

    public virtual void ReadyAbility()//TODO: Rename this
    {
        abilityOver = false;
        lastAnimSet = "stand";
        curSubAbilityIndx = 0;
        objectTargets.Clear();
        positionTargets.Clear();
        SetUpNextSubAb();
        
        uses++;
        useable = false;
        
    }

    public void UpdateCooldown()
    {
        if (curCooldownEndTime < Time.time)
        {
            if (uses < maxUses)
            {
                if (abilityOver == true)
                {
                    useable = true;
                }
            }
            AbilityManager.abManager.UnregisterAbilityForCooldown(this);
        }
    }

    public bool IsAbilityOver()
    {
        return (abilityOver);
    }

    public void AbilityStateMachine()//This is called on update by the ability manager
    {
        subAbilities[curSubAbilityIndx].DoSubAbility(this);
        
    }

    
    public void OnSubAbilityOver()//Called by sub ability delegate
    {
        FinishLastSubAb();
        IncrementCurSubAb();

        if (SubAbilitesAreFinished())
        {
            EndAbility();
        }
        else
        {
            SetUpNextSubAb();
        }
    }

    private void IncrementCurSubAb()
    {
        curSubAbilityIndx++;
    }

    private bool SubAbilitesAreFinished()
    {
        if (curSubAbilityIndx >= subAbilities.Count)
        {
            
            return true;
        }
        return false;
    }

    private void EndAbility()
    {
        //Debug.Log("end ability called");
        AbilityManager.abManager.RegisterAbilityForCooldown(this);
        curCooldownEndTime = cooldownTime + Time.time;
        abilityOver = true;
    }

    private void SetUpNextSubAb()
    {
        subAbilities[curSubAbilityIndx].EndSubAbility = OnSubAbilityOver;
        subAbilities[curSubAbilityIndx].DoInitialSubAbility(this);
    }

    private void FinishLastSubAb()
    {
        subAbilities[curSubAbilityIndx].DoFinishSubAbility(this);
    }

    public void OnSelectionFinished(List<GameObject> selectedObjects)
    {

    }

    #region Utilities
    public static List<Ability> NewRetrunOnlyAbilitiesOfContext(UsableContexts cont, List<Ability> abs)
    {
        List<Ability> abilitiesOfContext = new List<Ability>();
        foreach (Ability a in abs)
        {
           // foreach (UsableContexts c in a.usableContexts)
            {
                //if (c == cont)
                {
                    abilitiesOfContext.Add(a);
                    //break;
                }
            }
        }
        return abilitiesOfContext;
    }
    #endregion Utilities
}


