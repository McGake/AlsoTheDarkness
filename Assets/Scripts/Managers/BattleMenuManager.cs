using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;


public class BattleMenuManager : GameSegment
{

    public enum BattleMenuStates
    {
        none = 0,
        SwitchingHero = 1,
        HeroSelected = 2,
        NoHeroSelected = 3,

        WaitingOnAbilitySystem = 12,

        BattleEnd = 4,
        DisplayFinalStats = 5,


    }

    public static BattleMenuManager battleMenuManager;

    public GameObject pointer;

    public GameObject secondaryPointer;

    public BattleMenuStates curState = BattleMenuStates.SwitchingHero;

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


    void Awake()
    {
        battleMenuManager = this;
        objectsInBattle = FindObjectOfType<ObjectsInBattle>();//cant I just set this in the inspcetor? 
    }

    void Start()
    {
        AddAbilityClustersToDictionary();
        AddAllAbilitybuttonsToListInProperOrder();
        AddCallbacksToAbilities();
        SwitchHero();
        PopulateHeroMenu();

    }

    public void OnEnable()
    {
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
        Vector2 force = new Vector2(Random.Range(-35f, 35f), 75f);
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
                aB.StartSelectFromPCs = StartSelectFromPCs;
                aB.StartSelectFromEnemies = StartSelectFromEnemies;
                aB.StartSelectAllPCs = StartSelectAllPCs;
                aB.StartSelectAllEnemeies = StartSelectAllEnemies;
                aB.StartSelectAllPCsButCurrent = StartSelectAllPCsButCurrent;               
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
    void SwitchHero()
    {
        curHero = objectsInBattle.pcsInBattle[curHeroIndx];
        pointer.transform.position = curHero.transform.position + new Vector3(-.33f, .3f,0f);
    }

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
            allAbilityButtons[i].uIButton.GetComponentInChildren<Text>().text = curCombatAbilities[i].DisplayName;
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
            Debug.Log("x");
            TriggerAbilityByIndex(xIndx);
        }
        if (Input.GetButtonDown("Y"))
        {
            Debug.Log("y");
            TriggerAbilityByIndex(yIndx);
        }
        if (Input.GetButtonDown("B"))
        {
            Debug.Log("b");
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
        if (AbilityManager.abManager.IsCharacterCurrentlyDoingAbility(curHero) == false) //another way to do this would be just to check current heros abilities to see if he has any that are currently set to not finished.
        {
            CurSelectionBehavior = WaitForAbilityAndUpdateDisplay;
            curHero.GetComponent<BaseBattleActor>().DoAbility(abilityToPass);
           
            
            //curState = BattleMenuStates.WaitingOnAbilitySystem;
        }
        //else just do nothing and continue
    }


    void UpdateAbilityMenu()
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


            }
        }
    }
    #endregion HeroSelected

    #region NoHeroSelected
    void CheckForHeroAutoSelect()
    {

    }
    #endregion NoHeroSelected

    #region BattleEnd
    void StartFinalStatsDisplay()
    {

    }
    #endregion BattleEnd

    #region DisplayFinalStats
    void DisplayFinalStats()
    {

    }
    #endregion DisplayFinalStats

    private const float delaySwitchTime = .5f;

    private float nextLegalSwitch = 0f;



    public void SwitchHeroOnInput()
    {

        float yVal = Input.GetAxis("Vertical");
        
        if (Time.time > nextLegalSwitch && preventCharacterSwitch == false)
        {
            if (yVal > 0f)
            {

                nextLegalSwitch += Time.time + delaySwitchTime;
                curHeroIndx++;
                if(curHeroIndx > objectsInBattle.pcsInBattle.Count -1)
                {
                    curHeroIndx = 0;
                }
                SwitchHero();
                PopulateHeroMenu();
            }
            else if (yVal < 0f)
            {
                nextLegalSwitch = Time.time + delaySwitchTime;
                curHeroIndx--;
                if (curHeroIndx < 0)
                {
                    curHeroIndx = objectsInBattle.pcsInBattle.Count -1;
                }
                SwitchHero();
                PopulateHeroMenu();
            }
        }

        if(yVal == 0)
        {
            nextLegalSwitch = Time.time - .01f; ;
        }

        TriggerAbilityOnInput();
        UpdateAbilityMenu();
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
        secondaryPointer.SetActive(true);
    }
    public void TurnOffSecondaryPointer()
    {
        secondaryPointer.SetActive(false);
    }

    #region SelectionMethods

    List<GameObject> objectsToSwtichBetween = new List<GameObject>();

    private GameObject tempTarget;

    private List<GameObject> tempGroupTarget;

    private SubAbility selectingSubAbility;

    private delegate void DelOnSelectionFinished(List<GameObject> selectedObjects);
    DelOnSelectionFinished OnSelectionFinished;

    private void StartSelectFromPCs(SubAbility subAb)
    {
        Debug.Log("method start " + CurSelectionBehavior.Method);
        Debug.Log("startSelectFromPCs");
        InitializeSelection(subAb);
        objectsToSwtichBetween = objectsInBattle.pcsInBattle;
        CurSelectionBehavior = LinearSelection;
        Debug.Log("method end " + CurSelectionBehavior.Method);
    }
    private void StartSelectFromEnemies(SubAbility subAb)
    {
        InitializeSelection(subAb);
        objectsToSwtichBetween = objectsInBattle.enemiesInBattle;
        CurSelectionBehavior = LinearSelection;
    }

    public void TestSelection()
    {
        Debug.Log("TestSelection 1 is playing");

        if(Input.GetButtonDown("A"))
        {
            Debug.Log("A was pressed");
            CurSelectionBehavior = LinearSelection;
        }
    }

    public void TestSelectionTwo()
    {
        Debug.Log("TestSelection 222two222 is playing");
    }

    private void StartSelectAllPCs(SubAbility subAb)
    {
        InitializeSelection(subAb);
        objectsToSwtichBetween = objectsInBattle.pcsInBattle;
        CurSelectionBehavior = OneGroupSelection;

    }

    private void StartSelectAllEnemies(SubAbility subAb)
    {
        InitializeSelection(subAb);
        objectsToSwtichBetween = objectsInBattle.enemiesInBattle;
        CurSelectionBehavior = OneGroupSelection;
    }



    private void StartSelectAllPCsButCurrent(SubAbility subAb)//TODO: maybe Just make this a call back rather than passing the whole sub ability
    {
        InitializeSelection(subAb);
        objectsToSwtichBetween = objectsInBattle.pcsInBattle;
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

    private void InitializeSelection(SubAbility subAb)
    {
        OnSelectionFinished = subAb.OnSelectionFinished;
        secondaryPointer.SetActive(true);
    }

    private void EndSelection(List<GameObject> selectedObjects)
    {
        Debug.Log("End Selection");
        OnSelectionFinished(selectedObjects);
        CurSelectionBehavior = SwitchHeroOnInput;
        secondaryPointer.SetActive(false);
        
    }

    public void LinearSelection()
    {
        float yVal = Input.GetAxis("Vertical");

        if (Time.time > nextLegalSwitch)
        {

            if (yVal > 0f)
            {
                nextLegalSwitch = Time.time + delaySwitchTime;
               genericSwitchIndx++;
                if (genericSwitchIndx > objectsToSwtichBetween.Count - 1)
                {
                    genericSwitchIndx = 0;
                }
                
            }
            else if (yVal < 0f)
            {
                nextLegalSwitch = Time.time + delaySwitchTime;
                genericSwitchIndx--;
                if (genericSwitchIndx < 0)
                {
                    genericSwitchIndx = objectsToSwtichBetween.Count - 1;
                }
                
            }
            secondaryPointer.transform.position = objectsToSwtichBetween[genericSwitchIndx].transform.position;
        }
        if(Input.GetButtonDown("A"))
        {
            List<GameObject> tempSelectedObjects = new List<GameObject>();
            tempSelectedObjects.Add(objectsToSwtichBetween[genericSwitchIndx]);
            EndSelection(tempSelectedObjects);
        }        
    }

    private void OneGroupSelection() //this is the old school way of doing this. need to make partially see through selection indicators and turn them on and off or move all of them. This does nothing except visually show the selection and wait for confirmation
    {
        
        secondaryPointer.transform.position = objectsToSwtichBetween[genericSwitchIndx].transform.position;
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

    private List<GameObject> SwitchObdjectsToSwitchBetweenByEnum(SelectionCategories sC)
    {
        List<GameObject>  tempObjects = new List<GameObject>();

        if (sC == SelectionCategories.Enemies)
        {
            tempObjects = objectsInBattle.enemiesInBattle;
        }
        else if (sC == SelectionCategories.PCs)
        {
            tempObjects = objectsInBattle.pcsInBattle;
        }
        if (sC == SelectionCategories.Everyone)
        {
            tempObjects.AddRange(objectsInBattle.enemiesInBattle);
            tempObjects.AddRange(objectsInBattle.pcsInBattle);
        }
        return tempObjects;
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