using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
[CreateAssetMenu(fileName = "NameThisAbility", menuName = "ScriptableObjects/Ability", order = 1)]
public class Ability:ScriptableObject
{

#pragma warning disable 649
    [SerializeField] private string _displayName;
    public string DisplayName { get { return _displayName; }}
    public int maxUses;
    [HideInInspector] public int uses = 0;
    public float cooldownTime;
    [SerializeField] public List<SubAbility> inspectorSubAbilities;
#pragma warning restore 649

    public Type ActorType { get; private set; }
    public Animator PCAnimator { get; private set; }
    public BattleActorView BattleActorView { get; private set; }
    public bool AbilityOver { get; private set; } = true;
    public bool Useable { get; set; } = true;
    public GameObject Owner { get; private set; }
    public float CurCooldownEndTime { get; private set; } = 0f;
    public string LastAnimSet { get; set; } = "stand";

    /// <summary>
    /// A property so that sub abilitie that only ever target one object can make that fact explicit
    /// </summary>
    public GameObject singleObjectTarget  {get { return objectTargets[0]; } set{ objectTargets[0] = value; } }

    [HideInInspector] public BattleStats stats;
    [HideInInspector] public List<GameObject> objectTargets = new List<GameObject>();
    [HideInInspector] public List<Vector3> positionTargets = new List<Vector3>();
    [HideInInspector] public List<Action> projectileCallbacks { get; private set; } = new List<Action>();
    [System.NonSerialized] private List<SubAbility> subAbilities = new List<SubAbility>();
    [HideInInspector] public int curSubAbilityIndx = 0;

    private List<object> scriptsPreventingUse = new List<object>();

    #region TrackIfAbilityIsUseable
    public void AddUsePreventor(object script)
    {
        scriptsPreventingUse.Add(script);
    }

    public void RemoveUsePreventor(object script)
    {
        if (scriptsPreventingUse.Contains(script))
        {
            scriptsPreventingUse.Remove(script);
        }
    }

    public bool IsUseable()
    {
        if(scriptsPreventingUse.Count > 0)
        {
            return false;
        }
        return true;
    }
    #endregion TrackIfAbilityIsUseable

    #region ExternalTargetingMethods 
    //This allows different targeting methods for sub abilities using the exact same code. So monsters can target pcs using an ai script and pcs can target using a controll stick and all sub abilities and abilities work with no changes.
    public delegate void DelStartSelectFromPCs(SubAbility subAb,Type requesterType);
    public DelStartSelectFromPCs StartSelectFromPCs; 

    public delegate void DelStartSelectFromEnemies(SubAbility subAb, Type requesterType);
    public DelStartSelectFromEnemies StartSelectFromEnemies;

    public delegate void DelStartSelectAllPCs(SubAbility subAb, Type requesterType);
    public DelStartSelectAllPCs StartSelectAllPCs;

    public delegate void DelStartSelectAllEnemies(SubAbility subAb, Type requesterType);
    public DelStartSelectAllEnemies StartSelectAllEnemeies;

    public delegate void DelStartSelectAllPCsButCurrent(SubAbility subAb, Type requesterType);
    public DelStartSelectAllPCsButCurrent StartSelectAllPCsButCurrent;

    public Func<GameObject, bool> IsCurrentSelectedHero;

    public delegate void DelKickOffMinigame(MiniGame mG);
    public DelKickOffMinigame KickOffMiniGame;

    public delegate GameObject DelInstantiateInWorldSpaceCanvas(GameObject go, Vector3 position);
    public DelInstantiateInWorldSpaceCanvas InstantiateInWorldSpaceCanvas;
    #endregion ExternalTargetingMethods

    public void SetupAbility(GameObject owner)
    {
        CacheOwnerReferences(owner);
        SetStartingValues();
        SetupActorType();
        InstantiateScriptableObjects();
        RemoveUsePreventor(this);
    }
    #region Setup
    private void CacheOwnerReferences(GameObject owner)
    {
        this.Owner = owner;
        PCAnimator = Owner.GetComponent<Animator>();
        BattleActorView = Owner.GetComponent<BattleActorView>();
        stats = Owner.GetComponent<BaseBattleActor>().stats;
    }

    private void SetStartingValues()
    {
        AbilityOver = true;
        CurCooldownEndTime = 0f;
        LastAnimSet = "stand";
        curSubAbilityIndx = 0;
    }

    private void SetupActorType()
    {
        if (Owner.GetComponent<BaseEnemy>() != null)
        {
            ActorType = typeof(BaseEnemy);
        }
        else if (Owner.GetComponent<BattlePC>() != null)
        {
            ActorType = typeof(BattlePC);
        }
    }
    private void InstantiateScriptableObjects()
    {
        for (int i = 0; i < inspectorSubAbilities.Count; i++)
        {
            subAbilities.Add(Instantiate(inspectorSubAbilities[i]));
        }
    }
    #endregion Setup

    #region KickOff
    public virtual void KickOffAbility()
    {
        SetStartingValues();
        ClearPreviousUseData();
        SetUpNextSubAb();
        RunSubAbilityInitial();
        uses++;
        AddUsePreventor(this);
    }
    
    private void ClearPreviousUseData()
    {
        objectTargets.Clear();
        positionTargets.Clear();
    }
    #endregion KickOff

    #region MainStateMachine //Runs all sub abilities and Calls EndAbility when done
    public void OnSubAbilityOver()
    {
        RunSubAbilityFinish();

        IncrementCurSubAb();

        if (SubAbilitesAreFinished())
        {
            EndAbility();
        }
        else
        {
            SetUpNextSubAb();
            RunSubAbilityInitial();
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
        AbilityManager.abManager.RegisterAbilityForCooldown(this);
        CurCooldownEndTime = cooldownTime + Time.time;
        AbilityOver = true;
    }
    private void SetUpNextSubAb()
    {
        subAbilities[curSubAbilityIndx].EndSubAbility = OnSubAbilityOver;
    }

    private void RunSubAbilityInitial()
    {
        subAbilities[curSubAbilityIndx].DoInitialSubAbility(this);
    }

    //This is called on update by the AbilityManager
    public void RunSubAbility()
    {
        subAbilities[curSubAbilityIndx].DoSubAbility(this);
    }
    private void RunSubAbilityFinish()
    {
        subAbilities[curSubAbilityIndx].DoFinishSubAbility(this);
    }

    #endregion MainStateMachine

    public void UpdateCooldown()
    {
        if (CurCooldownEndTime < Time.time)
        {
            if (uses < maxUses)
            {
                if (AbilityOver == true)
                {
                    RemoveUsePreventor(this);
                }
            }
            AbilityManager.abManager.UnregisterAbilityForCooldown(this);
        }
    }

    #region Utilities
    public static List<Ability> NewRetrunOnlyAbilitiesOfContext(UsableContexts cont, List<Ability> abs) //TODO: this system is on hold for now untill we create use contexts other than battle that actually need this. For now it returns all abilities sent.
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

    public void SubscribeToProjectileCallback(Action callbackToSubscribe)
    {
        projectileCallbacks.Add(callbackToSubscribe);
    }

    public void UnsubscribeToProjectileCallback(Action callbackToUnsubscribe)
    {
        projectileCallbacks.Remove(callbackToUnsubscribe);
    }

    public void RunProjectileCallbacks()
    {
        for (int i = 0; i < projectileCallbacks.Count; i++)
        {
            projectileCallbacks[i]();
        }
    }
    public bool IsAbilityOver()
    {
        return (AbilityOver);
    }
    #endregion Utilities
}


public enum UsableContexts
{
    none = 0,
    battleAbilityMenu = 1,
    partyMenu = 2,
    passive = 3,
    projectile = 4,
}