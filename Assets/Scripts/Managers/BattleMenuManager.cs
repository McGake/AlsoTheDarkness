using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class BattleMenuManager : GameSegment
{
    public static BattleMenuManager battleMenuManager;

    public GameObject cursor;

    public GameObject secondaryCursor;

    ObjectsInBattle objectsInBattle;

    public GameObject curHero;

    public GameObject abilityButtonDisplay; //figure some way to get this without using drag and drop or finding by string perhaps tag or singleton

    //public Dictionary<AbilityClusterButton, AbilityCluster> abilityClusters = new Dictionary<AbilityClusterButton, AbilityCluster>();

    public List<AbilityCluster> abilityClusters = new List<AbilityCluster>();

    public List<AbilityButton> allAbilityButtons = new List<AbilityButton>();

    public int curHeroIndx = 0;

    public AbilityCluster curCluster;

    public List<GameObject> selectorPool;

    public bool preventCharacterSwitch;

    public GameObject popoutTextBoxPrefab;

    public Transform worldSpaceCanvas;

    public void SetUpBattleMenuManager()
    {
        battleMenuManager = this;
        objectsInBattle = FindObjectOfType<ObjectsInBattle>();//cant I just set this in the inspcetor? 

        AddAbilityClustersToDictionary();
        AddAllAbilitybuttonsToListInProperOrder();
        AddCallbacksToAbilities();

        curHero = objectsInBattle.pcsInBattle[curHeroIndx];
        cursor.transform.position = curHero.transform.position + new Vector3(-.33f, .3f, 0f);
        modifiedSelectionConeAngle = selectionConeAngle;
        PopulateHeroMenu();

        gameStateMachine.SetCurrentGameSegment(this); //this is what actually kicks off the battle being updated. I wonder if I should somehow put this in the battle start script
        gameStateMachine.SetDefaultGameSegment(this);
        CurSelectionBehavior = SwitchHeroOnInput;

    }

    #region OneShotFunctions
    public void ExplodeDamageText(float damage, GameObject damagedObject)
    {
        GameObject explodingText = GameObject.Instantiate(popoutTextBoxPrefab, damagedObject.transform.position, Quaternion.identity, worldSpaceCanvas.transform);
        int damageAsInt = Mathf.RoundToInt(damage);
        explodingText.GetComponent<TextMeshProUGUI>().text = damageAsInt.ToString();
        Collider2D tempCollider = damagedObject.transform.GetChild(0).GetComponent<Collider2D>();
        
        explodingText.GetComponent<OnlyCollideWithOneThing>().thingToCollideWith = tempCollider;
        Vector2 force = new Vector2(UnityEngine.Random.Range(-35f, 35f), 75f);
        force.Normalize();
        explodingText.GetComponent<Rigidbody2D>().AddForce(force * 250);
    }
    #endregion OneShotFunctions

    void AddAbilityClustersToDictionary()
    {
        //this will eventually add all the ability button clusters open, righ trigger down, left trigger down, and both down
        abilityClusters.Add(abilityButtonDisplay.GetComponentInChildren<AbilityCluster>()); //needs work  
        curCluster = abilityClusters[0]; //temp code          
    }

    void AddAllAbilitybuttonsToListInProperOrder()
    {
        //this will eventually go through all the clusters to get all of the abilities
        AbilityCluster tempCluster = abilityClusters[0];
        foreach (AbilityButton aB in tempCluster.abilityButtons)
        {
            allAbilityButtons.Add(aB);
        }
    }

    void AddCallbacksToAbilities() //TODO: this is temporary untill we totaly fix this jumble of a UI battle system and set up a proper event system to communicate with abilities
    {
        List<Ability> tempListToSet = new List<Ability>();
        foreach (GameObject pc in objectsInBattle.pcsInBattle)
        {
           tempListToSet = pc.GetComponent<BattlePC>().abilities; //TODO: Decouple this
            foreach (Ability aB in tempListToSet)
            {
                aB.StartSelectFromPCs = StartSelectFromFriendlies;
                aB.StartSelectFromEnemies = StartSelectFromOpponents;
                aB.StartSelectAllPCs = StartSelectAllFriendlies;
                aB.StartSelectAllEnemeies = StartSelectAllOpponents;
                aB.StartSelectAllPCsButCurrent = StartSelectAllFriendliesButCurrent;               
                aB.InstantiateInWorldSpaceCanvas = InstantiateInWorldSpaceCanvas;
            }
        }  
        foreach(GameObject en in objectsInBattle.enemiesInBattle)
        {
            tempListToSet = en.GetComponent<BaseEnemy>().abilities; //TODO: Decouple this
            foreach (Ability aB in tempListToSet)
            {
                aB.StartSelectFromPCs = StartSelectFromFriendlies_AI;
                aB.StartSelectFromEnemies = StartSelectFromOpponents_AI;
                aB.StartSelectAllPCs = StartSelectAllFriendlies_AI;
                aB.StartSelectAllEnemeies = StartSelectAllOpponents_AI;
                aB.StartSelectAllPCsButCurrent = StartSelectAllFriendliesButCurrent_AI;
                aB.InstantiateInWorldSpaceCanvas = InstantiateInWorldSpaceCanvas;
            }
        }
    }

    public override void UpdateGameSegment()
    {
        CurSelectionBehavior();
    }

    private delegate void DelCurSelectionBehavior();
    private DelCurSelectionBehavior CurSelectionBehavior;

    #region SwitchingHero


    List<Ability> curCombatAbilities = new List<Ability>();
    void PopulateHeroMenu()
    {
        GetBattleAbilities();
        SetDisplayToNewAbilities();        
    }

    void GetBattleAbilities()
    {
        List<Ability> allAbilitiesOnPC = curHero.GetComponent<BattlePC>().abilities;

        curCombatAbilities = Ability.NewRetrunOnlyAbilitiesOfContext( UsableContexts.battleAbilityMenu, allAbilitiesOnPC);
    }

    void SetDisplayToNewAbilities()
    {

        for(int i = 0; i < curCombatAbilities.Count; i++)
        {
            allAbilityButtons[i].uIButton.SetActive(true);
            allAbilityButtons[i].abilityView = allAbilityButtons[i].uIButton.GetComponentInChildren<AbilityView>();
            allAbilityButtons[i].abilityView.SetButtonLabel(curCombatAbilities[i].DisplayName);
            allAbilityButtons[i].abilityView.SetUsesLeft((curCombatAbilities[i].maxUses - curCombatAbilities[i].uses).ToString());
            allAbilityButtons[i].ability = curCombatAbilities[i];
            //put other image changes here when you have them
        }

        for (int i = curCombatAbilities.Count; i < allAbilityButtons.Count; i++)
        {
            allAbilityButtons[i].uIButton.SetActive(false);
        }
    }
    #endregion SwitchingHero

    #region HeroSelected

    const int aIndx = 0;
    const int xIndx = 1;
    const int yIndx = 2;
    const int bIndx = 3;

    void TriggerAbilityOnInput()
    {

        if(Input.GetButtonDown("A"))
        {
            TriggerAbilityByIndex(aIndx);
        }
        if (Input.GetButtonDown("X"))
        {
          //  Debug.Log("x");
            TriggerAbilityByIndex(xIndx);
        }
        if (Input.GetButtonDown("Y"))
        {
          //  Debug.Log("y");
            TriggerAbilityByIndex(yIndx);
        }
        if (Input.GetButtonDown("B"))
        {
           // Debug.Log("b");
            TriggerAbilityByIndex(bIndx);
        }
    }

    void TriggerAbilityByIndex(int indx)
    {
        Ability abilityToPass = null;
        foreach (Ability ab in curCombatAbilities)
        {
            if (ab == curCluster.abilityButtons[indx].ability)
            {
                abilityToPass = ab;
                break;
            }
        }
        if (abilityToPass == null)
        {
            Debug.LogError("We just called a null ability from the menu. did the hero get switched or the ability list change?");
        }
        if (AbilityManager.abManager.IsCharacterCurrentlyDoingAbility(curHero) == false && objectsInBattle.IsPCUpAndInBattle(curHero)) //another way to do this would be just to check current heros abilities to see if he has any that are currently set to not finished.
        {
            CurSelectionBehavior = SwitchHeroOnInput;//WaitForAbilityAndUpdateDisplay;
            curHero.GetComponent<BaseBattleActor>().DoAbility(abilityToPass);
           
            
            //curState = BattleMenuStates.WaitingOnAbilitySystem;
        }
        //else just do nothing and continue
    }


    void UpdateAbilityMenu()//ToDo: move setting the actual circle and such to the AbilityView. Time till cooldown end should probably still be calculated here or perhaps on the ability itself
    {
        //AbilityCluster tempCluster;
        AbilityButton curButton;
        float coolDownEndTime;
        float coolDown;
        float fillPercentage;

        //this will grey out abilities that are curently not useable and update a recharge bar
        for(int i = 0; i < abilityClusters.Count; i++)
        {
            for(int j = 0; j < abilityClusters[i].abilityButtons.Count; j++)
            {
                curButton = abilityClusters[i].abilityButtons[j];
                if (curButton.uIButton.activeSelf)
                {
                    coolDownEndTime = curButton.ability.curCooldownEndTime;
                    coolDown = curButton.ability.cooldownTime;
                    fillPercentage = (coolDownEndTime - Time.time) / coolDown;
                    if (fillPercentage > 1)
                    {
                        fillPercentage = 1;
                    }
                    curButton.radialProgress.fillAmount = fillPercentage;
                    if (curButton.ability.useable == false)
                    {
                        curButton.greyMask.SetActive(true);
                    }
                    else
                    {
                        curButton.greyMask.SetActive(false);
                    }
                }

                curButton.abilityView.SetUsesLeft((curButton.ability.maxUses - curButton.ability.uses).ToString());//TODO: change this to trigger when the ability acctually gets used to avoid this calculation all the time
            }
        }
    }
    #endregion HeroSelected




    private const float delaySwitchTime = .5f;

    private float nextSwitchAllowedTime = 0f;


    private bool CheckIfInsideCone(GameObject target, Vector3 inputDir, float coneAngleSize)
    {
        Vector3 normalizedDir = FindDirectionOfTarget(target);
   
        inputDir.Normalize();

        Debug.DrawRay(curHero.transform.position, (inputDir*3) , Color.green, .5f);
        

        Vector3 outsideVec = Quaternion.AngleAxis(35, Vector3.forward) * inputDir;

        Vector3 outsideVec2 = Quaternion.AngleAxis(-35, Vector3.forward) * inputDir;

        Debug.DrawRay(curHero.transform.position, (outsideVec * 3), Color.red, .5f);

        Debug.DrawRay(curHero.transform.position, (outsideVec2 * 3), Color.red, .5f);

        float dotProduct = Vector3.Dot(normalizedDir, inputDir);

        float degrees = Mathf.Rad2Deg * Mathf.Acos(dotProduct);


        if (degrees < coneAngleSize)
        {
            //Debug.Log("Normalized Direction of object " + normalizedDir);
            //Debug.Log("Normalized Joystick " + inputDir);
            //Debug.Log("is Inside!!!");
            //Debug.Log("dot Product angle " + degrees);
            //Debug.Log("cone angle size " + coneAngleSize);
            //Debug.Log("target position " + target.name + " " + target.transform.position);
            //Debug.Log("");
            return (true);
        }
        else
        {
            //Debug.Log("not inside!!!");
            return (false);
        }
    }

    private Vector3 FindDirectionOfTarget(GameObject target)
    {
        Vector3 dir =   target.transform.position - curHero.transform.position;

        //Debug.Log(target.name + " target position is " + target.transform.position);

        //Debug.Log(target.name + "unormalized dirction is " + dir);

        dir.Normalize();

        //Debug.Log(target.name + " direction is " + dir);

        return (dir);
    }

    GameObject curClosest;

    GameObject potentialClosest;

    public float selectionSensitivityThreshhold = 0.1f;

    public float selectionRepeatDealy = 0.25f;

    public float selectionConeAngle = 35f;

    public float selectionConeAngleExapnsionIncrement = 35f;

    private float modifiedSelectionConeAngle;
    public void SwitchHeroOnInput()
    {
        if (Time.time > nextSwitchAllowedTime && preventCharacterSwitch == false)
        {
            float yVal = Input.GetAxis("Vertical");
            float xVal = Input.GetAxis("Horizontal");

            if (Mathf.Abs(yVal) + Mathf.Abs(xVal) > selectionSensitivityThreshhold)
            {
                curClosest = null;
                potentialClosest = null;
                for (int i = 0; i < objectsInBattle.pcsInBattle.Count; i++)
                {
                    if (objectsInBattle.pcsInBattle[i] != curHero)
                    {
                        if (CheckIfInsideCone(objectsInBattle.pcsInBattle[i], new Vector3(xVal, yVal), modifiedSelectionConeAngle))
                        {

                            if (curClosest == null)
                            {
                                curClosest = objectsInBattle.pcsInBattle[i];
                            }
                            else
                            {
                                potentialClosest = objectsInBattle.pcsInBattle[i];
                                if (Vector3.SqrMagnitude(curHero.transform.position - curClosest.transform.position) > Vector3.SqrMagnitude(curHero.transform.position - potentialClosest.transform.position))
                                {
                                    curClosest = potentialClosest;
                                }
                            }
                        }
                    }
                }

                if (curClosest != null)
                {
                    curHero = curClosest;
                    cursor.transform.position = curHero.transform.position + new Vector3(-.33f, .3f, 0f);
                    nextSwitchAllowedTime = Time.time + selectionRepeatDealy;
                    PopulateHeroMenu();
                    modifiedSelectionConeAngle = selectionConeAngle;
                }
                else if(modifiedSelectionConeAngle <= 89f)
                {
                    if(selectionConeAngleExapnsionIncrement <= 1)
                    {
                        Debug.LogError("Expansion angle too small ");
                        return;
                    }
                    modifiedSelectionConeAngle += selectionConeAngleExapnsionIncrement;
                    modifiedSelectionConeAngle = Mathf.Clamp(modifiedSelectionConeAngle, 0f, 90f);
                    SwitchHeroOnInput(); //TODO: make this recursion a while loop instead. more better.
                }
                
            }
        }
        modifiedSelectionConeAngle = selectionConeAngle;
        TriggerAbilityOnInput();
        UpdateAbilityMenu();
    }

    public void SwitchHeroOnCharacterDeath()
    {

    }

    public void WaitForAbilityAndUpdateDisplay()
    {
        UpdateAbilityMenu();
    }


    #region Managers

    public List<MiniGame> miniGames = new List<MiniGame>();

    public void UpdateMinigames()
    {
        foreach(MiniGame mG in miniGames)
        {
            mG.UpdateMiniGame();
        }
    }

    #endregion Managers

    #region ExternalMethods
    public int genericSwitchIndx = 0;

    public void TurnOnSecondaryPointer()
    {
        secondaryCursor.SetActive(true);
    }
    public void TurnOffSecondaryPointer()
    {
        secondaryCursor.SetActive(false);
    }

    #region SelectionMethods

    List<GameObject> objectsToSwtichBetween = new List<GameObject>();

    private GameObject tempTarget;

    private List<GameObject> tempGroupTarget;

    private SubAbility selectingSubAbility;

    private delegate void DelOnSelectionFinished(List<GameObject> selectedObjects);
    DelOnSelectionFinished OnSelectionFinished;

    private delegate List<GameObject> DelGetRelations(Type requesterType);

    private void BaseSelection(SubAbility subAb, Type requesterType, DelCurSelectionBehavior SelectionBehavior, DelGetRelations GetRelations)
    {
        objectsToSwtichBetween = GetRelations(requesterType);
        CurSelectionBehavior = SelectionBehavior;
        if(requesterType == typeof(BattlePC))
        {
            InitializeSelection(subAb);
            

        }
        else if(requesterType == typeof(BaseEnemy))
        {
            OnSelectionFinished = subAb.OnSelectionFinished;
        }
    }

    private void StartSelectFromFriendlies(SubAbility subAb, Type requesterType)
    {
        BaseSelection(subAb, requesterType, LinearSelection, objectsInBattle.GetFriendsOfType);

        //InitializeSelection(subAb);
        //objectsToSwtichBetween = objectsInBattle.GetFriendsOfType(requesterType);

        //CurSelectionBehavior = LinearSelection;
    }

    private void StartSelectFromOpponents(SubAbility subAb, Type requesterType)
    {
        BaseSelection(subAb, requesterType, LinearSelection, objectsInBattle.GetOpponentsOfType);
        //InitializeSelection(subAb);
        //objectsToSwtichBetween = objectsInBattle.GetOpponentsOfType(requesterType);
        //CurSelectionBehavior = LinearSelection;
    }

    private void StartSelectFromOpponents_AI(SubAbility subAb, Type requesterType)
    {
        BaseSelection(subAb, requesterType, AISingleRandomSelection, objectsInBattle.GetOpponentsOfType);
    }

    private void StartSelectFromFriendlies_AI(SubAbility subAb, Type requesterType)
    {
        BaseSelection(subAb, requesterType, AISingleRandomSelection, objectsInBattle.GetFriendsOfType);
    }


    private void StartSelectAllFriendlies(SubAbility subAb,Type requesterType)
    {
        BaseSelection(subAb, requesterType, OneGroupSelection, objectsInBattle.GetFriendsOfType);
        //InitializeSelection(subAb);
        //objectsToSwtichBetween = objectsInBattle.pcsInBattle;
        //CurSelectionBehavior = OneGroupSelection;

    }

    private void StartSelectAllFriendlies_AI(SubAbility subAb, Type requesterType)
    {
        BaseSelection(subAb, requesterType, AIOneGroupSelection, objectsInBattle.GetFriendsOfType);
        //InitializeSelection(subAb);
        //objectsToSwtichBetween = objectsInBattle.pcsInBattle;
        //CurSelectionBehavior = OneGroupSelection;

    }

    private void StartSelectAllOpponents(SubAbility subAb, Type requesterType)
    {
        BaseSelection(subAb, requesterType, OneGroupSelection, objectsInBattle.GetOpponentsOfType);
        //InitializeSelection(subAb);
        //objectsToSwtichBetween = objectsInBattle.GetOpponentsOfType(requesterType);        
        //CurSelectionBehavior = OneGroupSelection;
    }


    private void StartSelectAllOpponents_AI(SubAbility subAb, Type requesterType)
    {
        BaseSelection(subAb, requesterType, AIOneGroupSelection, objectsInBattle.GetOpponentsOfType);
        //InitializeSelection(subAb);
        //objectsToSwtichBetween = objectsInBattle.GetOpponentsOfType(requesterType);        
        //CurSelectionBehavior = OneGroupSelection;
    }


    private void StartSelectAllFriendliesButCurrent(SubAbility subAb, Type requesterType)//TODO: maybe Just make this a call back rather than passing the whole sub ability
    {
        InitializeSelection(subAb);
        objectsToSwtichBetween = new List<GameObject> (objectsInBattle.GetFriendsOfType(requesterType));
        for(int i = 0; i < objectsToSwtichBetween.Count; i++)
        {

            if(objectsToSwtichBetween[i] == curHero)
            {
                objectsToSwtichBetween.RemoveAt(i);
                break;
            }
        }
        CurSelectionBehavior = OneGroupSelection;
    }

    private void StartSelectAllFriendliesButCurrent_AI(SubAbility subAb, Type requesterType)//TODO: maybe Just make this a call back rather than passing the whole sub ability
    {
        Debug.LogError("Method Not Created For AI. Please Write this method");
    }

    private void InitializeSelection(SubAbility subAb)
    {
        OnSelectionFinished = subAb.OnSelectionFinished;
        secondaryCursor.SetActive(true);
    }

    private void EndSelection(List<GameObject> selectedObjects)
    {
        OnSelectionFinished(selectedObjects);
        CurSelectionBehavior = SwitchHeroOnInput;
        secondaryCursor.SetActive(false);
        
    }

    public void LinearSelection()
    {
        float yVal = Input.GetAxis("Vertical");

        if (Time.time > nextSwitchAllowedTime)
        {

            if (yVal > 0f)
            {
                nextSwitchAllowedTime = Time.time + delaySwitchTime;
               genericSwitchIndx++;
                if (genericSwitchIndx > objectsToSwtichBetween.Count - 1)
                {
                    genericSwitchIndx = 0;
                }
                
            }
            else if (yVal < 0f)
            {
                nextSwitchAllowedTime = Time.time + delaySwitchTime;
                genericSwitchIndx--;
                if (genericSwitchIndx < 0)
                {
                    genericSwitchIndx = objectsToSwtichBetween.Count - 1;
                }
                
            }
            secondaryCursor.transform.position = objectsToSwtichBetween[genericSwitchIndx].transform.position;
        }
        if(Input.GetButtonDown("A"))
        {
            List<GameObject> tempSelectedObjects = new List<GameObject>();
            tempSelectedObjects.Add(objectsToSwtichBetween[genericSwitchIndx]);
            EndSelection(tempSelectedObjects);
        }        
    }

    public void AISingleRandomSelection()
    {
        int randomOpponentIndx = UnityEngine.Random.Range(0, objectsToSwtichBetween.Count);
        List<GameObject> tempSelectedObjects = new List<GameObject>();
        tempSelectedObjects.Add(objectsToSwtichBetween[randomOpponentIndx]);
        EndSelection(tempSelectedObjects);
    }

    private void OneGroupSelection() //this is the old school way of doing this. need to make partially see through selection indicators and turn them on and off or move all of them. This does nothing except visually show the selection and wait for confirmation
    {
        
        secondaryCursor.transform.position = objectsToSwtichBetween[genericSwitchIndx].transform.position;
        genericSwitchIndx++;
        if (genericSwitchIndx >= objectsToSwtichBetween.Count)
        {
            genericSwitchIndx = 0;
        }
        if (Input.GetButtonDown("A"))
        {

            EndSelection(objectsToSwtichBetween);
        }
    }

    private void AIOneGroupSelection()
    {
        EndSelection(objectsToSwtichBetween);
    }

    #endregion SelectionMethods

    private void KickOffMiniGame(MiniGame mG)
    {
        miniGames.Add(mG);
        mG.StartMiniGame(this);
    }


    #region SubAbilityMethods



    //public GameObject UserSelectPC()//This might now work. use UserSelectFromCategory as your model
    //{
    //    secondaryPointer.transform.position = objectsToSwtichBetween[genericSwitchIndx].transform.position;
    //    return UserSelectFromCategory(SelectionCategories.PCs);
    //}

    //public GameObject UserSelectFromCategory(SelectionCategories sC)
    //{
    //    objectsToSwtichBetween = SwitchObdjectsToSwitchBetweenByEnum(sC);
    //    tempTarget = LinearSelection(objectsToSwtichBetween);

    //    if (Input.GetButtonDown("A"))
    //    {
    //        EndLinearSwitchSelection();
    //        return tempTarget;
    //    }
    //    else
    //    {
    //        return null;
    //    }   
    //}

    //public List<GameObject> UserSelectEntireCategory(SelectionCategories sC)
    //{
        
    //    objectsToSwtichBetween = SwitchObdjectsToSwitchBetweenByEnum(sC);
    //    tempGroupTarget = OneGroupSelection(objectsToSwtichBetween);

    //    if(Input.GetButtonDown("A"))
    //    {
    //        EndLinearSwitchSelection();
    //        return tempGroupTarget;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}

    //public List<GameObject> UserSelectEntireCategoryButOne(SelectionCategories sC, GameObject objectToOmit)
    //{
    //    Debug.Log("select entire category but one");
    //    objectsToSwtichBetween = SwitchObdjectsToSwitchBetweenByEnum(sC);
    //    tempGroupTarget = OneGroupSelection(objectsToSwtichBetween);

    //    if (Input.GetButtonDown("A"))
    //    {
    //        //tempGroupTarget.Remove(objectToOmit);
    //        EndLinearSwitchSelection();
    //        return tempGroupTarget;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}


    #endregion SubAbilityMethods

    /* external methods that battle ui should have
    */
    #region ExternalUIMethods
    public GameObject InstantiateInWorldSpaceCanvas(GameObject toInstantiate, Vector3 position)
    {
        return GameObject.Instantiate(toInstantiate, position, Quaternion.identity, worldSpaceCanvas.transform);
    }


    #endregion ExternalUIMethods
    #endregion ExternalMethods

}

public enum SelectionCategories
{
    Enemies = 1,
    PCs = 2,
    Everyone = 3,
}

public abstract class MiniGame:ScriptableObject
{
    
    public GameObject mGuI;//TODO: make this private but show it in inspector

    private BattleMenuManager bMM;

    private Ability aB;

    public virtual void StartMiniGame(BattleMenuManager inBMM)
    {
        bMM = inBMM;
   
        isFinished = false;
    }

    public virtual void SetAbility(Ability inAB)
    {
        aB = inAB;
    }

    public abstract void UpdateMiniGame();

    public virtual void EndMiniGame()
    {
        isFinished = true;
        bMM.miniGames.Remove(this);
    }

    private bool isFinished = true;
    public virtual bool IsFinished()//TODO: possibly make something like this a delegate that gets called on the sub ability when the mini game is finsished
    {
        return isFinished;
    }
}