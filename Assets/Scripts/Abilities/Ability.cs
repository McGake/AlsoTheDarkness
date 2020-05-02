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


public enum AbilityState
{
    none = 0,
    Setup = 1,
    Target = 2,
    PrepAnimation = 3,
    Execute = 4,
    EndAnimation = 5,
    Cancel = 1000,
}


[System.Serializable]
[CreateAssetMenu(fileName = "NameThisAbility", menuName = "ScriptableObjects/Ability", order = 1)]
public class Ability:ScriptableObject
{
    [SerializeField]
    private string _displayName;
    public string DisplayName { get { return _displayName; } set { _displayName = value; } }

    public Animator pcAnimator { get; private set; }

    public bool abilityOver { get; private set; } = true;

    public bool useable { get; set; } = true; //change name of this variable to better indicate that it is checking wheather anything is preventing this ability from being used including cooldown

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

    [SerializeField]
    public List<SubAbility> inspectorSubAbilities;

    [System.NonSerialized]
    private List<SubAbility> subAbilities = new List<SubAbility>();

    [HideInInspector]
    public int curSubAbilityIndx = 0;

    public delegate void DelStartSelectFromPCs(SubAbility subAb);
    public DelStartSelectFromPCs StartSelectFromPCs; //TODO: this is temporary untill I can put in a proper event system

    public delegate void DelStartSelectFromEnemies(SubAbility subAb);
    public DelStartSelectFromEnemies StartSelectFromEnemies;

    public delegate void DelStartSelectAllPCs(SubAbility subAb);
    public DelStartSelectAllPCs StartSelectAllPCs;

    public delegate void DelStartSelectAllEnemies(SubAbility subAb);
    public DelStartSelectAllEnemies StartSelectAllEnemeis;

    public delegate void DelStartSelectAllPCsButCurrent(SubAbility subAb);
    public DelStartSelectAllPCsButCurrent StartSelectAllPCsButCurrent;

    public delegate void DelKickOffMinigame(MiniGame mG);
    public DelKickOffMinigame KickOffMiniGame;


    public delegate GameObject DelInstantiateInWorldSpaceCanvas(GameObject go, Vector3 position);
    public DelInstantiateInWorldSpaceCanvas InstantiateInWorldSpaceCanvas;


    public void SetupAbility(GameObject inOwner)
    {
        Debug.Log("set up ability");
        owner = inOwner;
        pcAnimator = owner.GetComponent<Animator>();
        subAbilities.Clear();
        abilityOver = true;
        useable = true;
        curCooldownEndTime = 0f;
        lastAnimSet = "stand";
        curSubAbilityIndx = 0;
        for(int i = 0; i < inspectorSubAbilities.Count; i++)
        {
            subAbilities.Add(Instantiate(inspectorSubAbilities[i]));
        }
    }


    public GameObject owner { get; protected set; }

    #region CooldownVariables
    public float cooldownTime;

    #endregion CooldownVariables

    public virtual void StartAbility()
    {
        //useable = false;
        abilityOver = false;
        lastAnimSet = "stand";
        curSubAbilityIndx = 0;
        objectTargets.Clear();
        positionTargets.Clear();
        SetUpNextSubAb();

    }

    public bool IsAbilityOver()
    {
        return (abilityOver);
    }

    public void AbilityStateMachine()
    {
        //Debug.Log( " ownder: " + owner.name+ " indx: " + curSubAbilityIndx + " ability: " + DisplayName + " sub abilities: " + subAbilities.Count + " isfinsihed: " + abilityOver);
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
        Debug.Log("end ability called");
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
                    break;
                }
            }
        }
        return abilitiesOfContext;
    }
    #endregion Utilities


    #region TargetingMethods
    public void TM_None()
    {

        //abilityState = NewAbilityState.Delay;
    }
    public void TM_NoFurtherInput()
    {
        //abilityState = NewAbilityState.Delay;
    }


    public void TM_DirectSelectionEnemy()
    {      
       //----
        //curTarget = BattleMenuManager.battleMenuManager.LinearSwitchSelection(SelectionCategories.Enemies);
        if (Input.GetButtonDown("A"))
        {
           // BattleMenuManager.battleMenuManager.EndLinearSwitchSelection();
//
            //abilityState = NewAbilityState.Delay;
        }
        if (Input.GetButtonDown("B"))
        {
            //BattleMenuManager.battleMenuManager.EndLinearSwitchSelection();
            //abilityState = NewAbilityState.EndAbility;
        }
    }
    #endregion TargetingMethods

    #region SelectSource
    public  void E_SS_PCFrontCenter()
    {
        //sources.Add(owner.transform);
    }
    #endregion SelectSource

    #region AnimateExecute

    public void E_A_CastSpell()
    {
        pcAnimator.SetBool("castSpell", true);
    }

    #endregion AnimateExecute
}

#region OldAbilities
[System.Serializable]
public class AbilityVars
{
    public string displayName;
    public List<UsableContexts> usableContexts = new List<UsableContexts>();

    public float damage;
    public float coolDown;


    public virtual void GetVars(AbilityVars def)
    {
        displayName = def.displayName;
        usableContexts = def.usableContexts;
        damage = def.damage;
        coolDown = def.coolDown;
    }
}

[System.Serializable]
public class OldAbility:AbilityVars//maybe ability should just be an interface since Im contemplating a sub class that just does totally different things based off Targeting Type
{


    [HideInInspector]
    public bool abilityOver = true;
    [HideInInspector]
    public bool castable = true; //change name of this variable to better indicate that it is checking wheather anything is preventing this ability from being used including cooldown
    [HideInInspector]
    public GameObject source;
    [HideInInspector]
    public float curCooldownEndTime = 0;

    protected AbilityState abilityState;



    public virtual void StartAbility(GameObject sourceOfAbility)
    {
        source = sourceOfAbility;
        abilityOver = false;
        abilityState = AbilityState.Setup;
    }

    public bool IsAbilityOver()
    {
        return (abilityOver);
    }

    public void AbilityStateMachine()
    {
        switch (abilityState)
        {
            case (AbilityState.Setup):
                Setup();
                break;
            case (AbilityState.Target):
                Target();
                break;
            case (AbilityState.PrepAnimation):
                PrepAnimation();
                break;
            case (AbilityState.Execute):
                Execute();
                break;
            case (AbilityState.EndAnimation):
                EndAnimation();
                break;
        }
        //if (AreStepsComplete() == false)
        //{
        //    foreach (AbilityStep step in steps)
        //    {
        //        if (step.ready)
        //        {
        //            step.DoStep();
        //        }
        //    }
        //}
    }

    //protected bool AreStepsComplete()
    //{
    //    foreach (AbilityStep step in steps)
    //    {
    //        if (step.concluded == false)
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    public virtual void Setup()
    {

    }

    public virtual void Target()
    {

    }


    public virtual void PrepAnimation()
    {

    }

    public virtual void Execute()
    {

    }

    public virtual void EndAnimation()
    {
        curCooldownEndTime = Time.time + coolDown;
    }

    #region Utilities
    public static List<OldAbility> RetrunOnlyAbilitiesOfContext(UsableContexts cont, List<OldAbility> abs)
    {
        List<OldAbility> abilitiesOfContext = new List<OldAbility>();
        foreach (OldAbility a in abs)
        {
            foreach (UsableContexts c in a.usableContexts)
            {
                if (c == cont)
                {
                    abilitiesOfContext.Add(a);
                    break;
                }
            }
        }
        return abilitiesOfContext;
    }
    #endregion Utilities
}

[System.Serializable]
public class SingleDirectVars : OldAbility
{
    public void GetVariables(SingleDirectVars def)
    {
        animSpeed = def.animSpeed;
    }
    public float animSpeed = 2f;
}
public class SingleDirect : SingleDirectVars
{
    private Animator pcAnimator;
    protected BaseBattleActor curTarget;
    protected Vector3 targetPosOffset = new Vector3(1, 0, 0);

    private List<GameObject> enemiesInBattle;
    protected Vector3 startPosition;
    public override void Setup()
    {
        enemiesInBattle = GameObject.FindObjectOfType<ObjectsInBattle>().enemiesInBattle;
        abilityState = AbilityState.Target;
        startPosition = source.transform.position;//this should be moved to a post selection targeting method called execute setup or something
        BattleMenuManager.battleMenuManager.preventCharacterSwitch = true;
        BattleMenuManager.battleMenuManager.TurnOnSecondaryPointer();
        pcAnimator = source.GetComponent<Animator>();
    }

    public override void Target()
    {
        //curTarget = BattleMenuManager.battleMenuManager.LinearSwitchSelection(enemiesInBattle).GetComponent<BaseBattleActor>();
        if (Input.GetButtonDown("A"))
        {
            BattleMenuManager.battleMenuManager.TurnOffSecondaryPointer();
            pcAnimator.SetBool("walk", true);
            pcAnimator.SetBool("stand", false);
            abilityState = AbilityState.PrepAnimation;
            BattleMenuManager.battleMenuManager.preventCharacterSwitch = false;
        }
        if (Input.GetButtonDown("B"))
        {
            BattleMenuManager.battleMenuManager.TurnOffSecondaryPointer();
            abilityState = AbilityState.Cancel;
        }
    }

    public override void PrepAnimation()
    {
        MoveToTarget(curTarget.transform.position + targetPosOffset);

        if (ArrivedAtTarget(curTarget.transform.position + targetPosOffset))
        {
            pcAnimator.SetBool("walk", false);
            pcAnimator.SetBool("attack", true);
            abilityState = AbilityState.Execute;
        }
    }




    public override void Execute()
    {
        DoAttack();

        if (pcAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && pcAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            pcAnimator.SetBool("walk", true);
            pcAnimator.SetBool("attack", false);
            abilityState = AbilityState.EndAnimation;
        }
    }
    public override void EndAnimation()
    {
        MoveToTarget(startPosition);

        if (ArrivedAtTarget(startPosition))
        {
            pcAnimator.SetBool("walk", false);
            pcAnimator.SetBool("stand", true);
            curCooldownEndTime = Time.time + coolDown;
            abilityOver = true;
        }
    }

    protected void MoveToTarget(Vector2 target)
    {
        source.transform.position = Vector3.MoveTowards(source.transform.position, target, animSpeed * Time.deltaTime);
    }

    protected bool ArrivedAtTarget(Vector2 target)
    {
        return (Vector3)target == source.transform.position;
    }

    protected void DoAttack()
    {
        //do atack animation/slash animation here and wait for animation to end
        curTarget.GetComponent<BaseBattleActor>().stats.hP -= (int)damage;
    }
}
public class SingleDirectEnemyVer:SingleDirect
{

    public override void Setup()
    {
        targetPosOffset = new Vector3(-1, 0, 0);
        abilityState = AbilityState.Target;
        startPosition = source.transform.position;//this should be moved to a post selection targeting method called execute setup or something
    }

    public override void Target()
    {
        int randomPCIndx =UnityEngine.Random.Range(0, ObjectsInBattle.objectsInBattle.pcsInBattle.Count);
        curTarget = ObjectsInBattle.objectsInBattle.pcsInBattle[randomPCIndx].GetComponent<BaseBattleActor>();
        abilityState = AbilityState.PrepAnimation;
    }

    public override void PrepAnimation()
    {
        MoveToTarget(curTarget.transform.position + targetPosOffset);

        if (ArrivedAtTarget(curTarget.transform.position + targetPosOffset))
        {
            abilityState = AbilityState.Execute;
        }
    }

    public override void Execute()
    {
        DoAttack();
        //Debug.Log(pcAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        //if (pcAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && pcAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        //{

            abilityState = AbilityState.EndAnimation;
        //}
    }
    public override void EndAnimation()
    {
        MoveToTarget(startPosition);

        if (ArrivedAtTarget(startPosition))
        {
            curCooldownEndTime = Time.time + coolDown;
            abilityOver = true;
        }
    }
}

[System.Serializable]
public class IndirectMagicVars : OldAbility
{
    public void GetVariables(IndirectMagicVars def)
    {
        walkSpeed = def.walkSpeed;
        buttonDownTime = def.buttonDownTime;
        repeateSpeed = def.repeateSpeed;
        numberOfMagicObjects = def.numberOfMagicObjects;
        magicObject = def.magicObject;
        distanceToStepForward = def.distanceToStepForward;
    }
        public float walkSpeed = 2f;
        public float buttonDownTime = 1f;
        public float repeateSpeed = .7f;
        public int numberOfMagicObjects = 3;
        public GameObject magicObject;
        public Vector3 distanceToStepForward;    
}
public class IndirectMagic : IndirectMagicVars
{
    //outside Vars

    //program influnced vars

    //inside vars

    private float triggerTime = 0;
    private Vector3 stepForwardTarget;
    private Vector3 stepBackTarget;
    private Animator pcAnimator;

    public override void Setup()
    {
        curCooldownEndTime = Time.time + coolDown;
        stepForwardTarget = source.transform.position + distanceToStepForward;
        stepBackTarget = source.transform.position;
        abilityState = AbilityState.Target;
        BattleMenuManager.battleMenuManager.preventCharacterSwitch = false /*true*/;
        createNextObjectTime = 0f;
        numOfObjectsCreated = 0;
        triggerTime = Time.time + buttonDownTime;
        pcAnimator = source.GetComponent<Animator>();
    }

    public override void Target()
    {
        if (Input.GetButtonDown("A"))
        {
            triggerTime = Time.time + buttonDownTime;
            Debug.Log("time. time "+ Time.time);
            Debug.Log("trigger time " + triggerTime);
            Debug.Log(Time.time + " " + triggerTime);
            return;
        }
        else
        {
            Debug.Log("trigger time in hold " + triggerTime);
            pcAnimator.SetBool("stand", false);
            pcAnimator.SetBool("chantSpell", true);
            if (Time.time > triggerTime)
            {

                abilityState = AbilityState.PrepAnimation;
                BattleMenuManager.battleMenuManager.preventCharacterSwitch = false;
                pcAnimator.SetBool("walk", true);
                pcAnimator.SetBool("chantSpell", false);

            }
        }
    }


    public override void PrepAnimation()
    {

        MoveToTarget(stepForwardTarget);
        if (ArrivedAtTarget(stepForwardTarget))
        {
            Debug.Log("2");
            pcAnimator.SetBool("walk", false);
            pcAnimator.SetBool("castSpell", true);
            abilityState = AbilityState.Execute;
        }
    }




    public override void Execute()
    {
        CreateMajicObjects();

    }
    public override void EndAnimation()
    {
        MoveToTarget(stepBackTarget);
        if (ArrivedAtTarget(stepBackTarget))
        {
            pcAnimator.SetBool("stand", true);
            pcAnimator.SetBool("walk", false);
            curCooldownEndTime = Time.time + coolDown;
            abilityOver = true;
        }
    }

    private void MoveToTarget(Vector3 target)
    {

        source.transform.position = Vector3.MoveTowards(source.transform.position, target, walkSpeed * Time.deltaTime);
    }

    private bool ArrivedAtTarget(Vector3 target)
    {
        Debug.Log(target + " target");
        Debug.Log(source.transform.position + " source pos");
        return (Vector3)target == source.transform.position;
    }


    public float createNextObjectTime = 0f;
    public int numOfObjectsCreated = 0;
    private void CreateMajicObjects()
    {
        if (createNextObjectTime < Time.time && numOfObjectsCreated < numberOfMagicObjects)
        {
            GameObject.Instantiate(magicObject, source.transform.position, Quaternion.identity);
            createNextObjectTime = Time.time + repeateSpeed;
            numOfObjectsCreated++;
        }
        if (numOfObjectsCreated >= numberOfMagicObjects)
        {
            pcAnimator.SetBool("walk", true);
            pcAnimator.SetBool("castSpell", false);
            abilityState = AbilityState.EndAnimation;
        }
    }
}

[System.Serializable]
public class ProjectileVars : OldAbility
{
    public float xSpeed = -5;
    public float ySpeed = 0;
    public float lifeDuration = 3.5f;

    public void GetVariables(ProjectileVars def)
    {
        xSpeed = def.xSpeed;
        ySpeed = def.ySpeed;
        lifeDuration = def.lifeDuration;
    }

}
[System.Serializable]
public class Projectile : ProjectileVars
{
    private Vector3 velocity;
    Rigidbody2D rb2d;
    private float lifeEnd;

    public void OnBattleCollision(BaseBattleActor bbA)
    {
        //Explode animation
        bbA.stats.hP -= (int)damage;
        abilityOver = true;
        //need to destroy the thing somehow
    }

    public override void Setup()
    {
        velocity = new Vector3(xSpeed, ySpeed, 0f);
        abilityState = AbilityState.Target;
        lifeEnd = Time.time + lifeDuration;
        abilityState = AbilityState.Target;
    }

    public override void Target()
    {
        abilityState = AbilityState.PrepAnimation;
    }

    public override void PrepAnimation()
    {
        source.transform.position += velocity * Time.deltaTime;
        if (Time.time > lifeEnd)
        {
            abilityState = AbilityState.Execute;
        }
    }

    public override void Execute()
    {
        //destroy self
        source.SetActive(false);
        abilityState = AbilityState.EndAnimation;
    }

    public override void EndAnimation()
    {
        abilityOver = true;
        curCooldownEndTime = Time.time + coolDown;
    }
}

[System.Serializable]
public class AttackAllVars:OldAbility
{
    public void GetVariables(AttackAllVars def)
    {
        animSpeed = def.animSpeed;
    }
    public float animSpeed = 2f;
}
public class AttackAll:AttackAllVars
{

    public List<GameObject> enemiesInBattle;

    public override void Setup()
    {
        enemiesInBattle = GameObject.FindObjectOfType<ObjectsInBattle>().enemiesInBattle;
        abilityState = AbilityState.Target;        
        BattleMenuManager.battleMenuManager.preventCharacterSwitch = true;
    }

    public override void Target()
    {
       //here i would have some grafics point at the heads of all enememies
        if(Input.GetButtonDown("A"))
        {
            Debug.Log("a button pressed in attack all");
            abilityState = AbilityState.PrepAnimation;
            BattleMenuManager.battleMenuManager.preventCharacterSwitch = false;
        }
    }

    public override void PrepAnimation()
    {
        abilityState = AbilityState.Execute;
    }

    public override void Execute()
    {
        Attack();
        abilityState = AbilityState.EndAnimation;

    }

    public void Attack()
    {
        foreach(GameObject enemy in enemiesInBattle)
        {
            enemy.GetComponent<BaseBattleActor>().stats.hP -= (int)damage;
        }
    }

    public override void EndAnimation()
    {
        abilityOver = true;
        curCooldownEndTime = Time.time + coolDown;
    }




    public class AbilityDescriber
    {

    }

}
#endregion OldAbilities


public enum InitialSelectType
{
    None = 0,
    SingleButtonPress = 1,
    SingleButtonInitialHold = 2,
    SingleButtonPostSelectHold = 3,
    ButtonCombo = 4,
    StartOnAwake = 5,
}

public enum TargetingType
{
    None = 0,
    NoFurtherInput = 1,
    //DirectSelectionEnemy = 2,
    //DirectSelectionEnemyOrFriend = 3,
    //DirectSelectionFriend = 4,
    //SelectEnemyMultiple = 5,
    //SelectEnemyAll = 6,
    //SelectFriendlyMultiple = 7,
    //SelectFriendlyAll = 8,
    //autoTargetSingleRandomEnemy = 9,
    //AutoTargetSingleSpecificEnemy = 10,
    //autoTargetAllEnemy = 11,
   // autoTargetAllFriendly = 12,
}

public enum AbilityMovement
{
    None = 0,
    WalkToTransform = 1,
    WalkToRelative = 2,
    //WalkToEnemy = 2,
    //WalkToEnemyPerfect = 3,

    //ContinuousLocalForward = 10,


}

public enum AbilityTypeNew
{
    None = 0,
    //Direct = 1,
    ManifestProjectile = 2,
    //Melee = 3,    

    //DamageOnTouch = 4,
}

public enum Preparation
{
    None = 0,
    Chanting = 1,
    //PrepareStrike = 2,
    //WaitForCondition = 3,
}

public enum ProjectileSpreadType
{
    None = 0,
    //FixedSingleVector = 1,
    //SingleVectorAtTarget = 2,
    //FixedCone = 3,
    //TargetedCone = 4,

    EvenSpreadCounterClockwise = 10,
    EvenSpreadClockwise = 11,
    EvenSpreadFromMiddle = 12,
}

public enum SpreadDistribution
{
    None = 0,
    //EvenDivideStartBottom = 1,
    //EvenDivideStartTop = 2,
    //SimpleRandom = 3,
    //RandomSingleVector
    //BySetNumberOfDegrees
    //BothExtreems
}


public enum ProjectileSource
{
    None = 0,
    PCFrontCenter = 1,
    //MultipleTransforms = 2,
}

public enum ProjectileSourceOrder
{
    None = 0,
    FirstSource = 1,
    //RandomSource = 2,
    SequentialSource = 3,
}


public enum CooldownType
{
    None = 0,
    StandardTime = 1,
}

