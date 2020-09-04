using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;
using System.Xml;



public class BattleMenuManager : GameSegment
{
    public static BattleMenuManager battleMenuManager;

    public GameObject primaryCursor;

    public GameObject secondaryCursor;

    ObjectsInBattle objectsInBattle;

    public GameObject curHero;

    public GameObject curSecondarySelection;

    public GameObject abilityButtonDisplay; //figure some way to get this without using drag and drop or finding by string perhaps tag or singleton

    //public Dictionary<AbilityClusterButton, AbilityCluster> abilityClusters = new Dictionary<AbilityClusterButton, AbilityCluster>();

    public List<AbilityCluster> abilityClusters = new List<AbilityCluster>();

    public List<AbilityButton> allAbilityButtons = new List<AbilityButton>();

    public AbilityCluster curCluster;

    public Transform worldSpaceCanvas;

    private Vector3 cursorOffset = new Vector3(-.66f, .22f, 0f);


    public delegate void DelCurSelectionBehavior(SelectionTask selectionTask);
    public delegate void DelSelectionFinishedCallback(List<GameObject> selectedObjects);
    
    public class SelectionTask
    {

        public DelCurSelectionBehavior CurSelectionBehavior;
        public DelSelectionFinishedCallback SelectionFinishedCallback;
        public List<GameObject> objectsForSelection;

        public SelectionTask(DelCurSelectionBehavior CurSelectionBehavior, List<GameObject> objectsForSelection, DelSelectionFinishedCallback SelectionFinishedCallback)
        {
            this.CurSelectionBehavior = CurSelectionBehavior;
            this.objectsForSelection = objectsForSelection;
            this.SelectionFinishedCallback = SelectionFinishedCallback;
        }
    }

    private List<SelectionTask> AutoSelectionTasks = new List<SelectionTask>();
    private List<SelectionTask> ManualSelectionTasks = new List<SelectionTask>();

    public void SetUpBattleMenuManager()
    {
        battleMenuManager = this;
        objectsInBattle = FindObjectOfType<ObjectsInBattle>();//cant I just set this in the inspcetor? Not if I want multiple battles ongoing at one time i cant... Unless I make the entire battle structure a prefab which might just be crazy enough to work.

        AddAbilityClustersToDictionary();
        AddAllAbilitybuttonsToListInProperOrder();
        AddCallbacksToAbilities();

        curHero = objectsInBattle.pcsInBattle[0];
        primaryCursor.transform.position = curHero.transform.position +cursorOffset;
        modifiedSelectionConeAngle = selectionConeAngle;
        PopulateHeroMenu();

        gameStateMachine.SetCurrentGameSegment(this); //this is what actually kicks off the battle being updated. I wonder if I should somehow put this in the battle start script
        gameStateMachine.SetDefaultGameSegment(this);

    }

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
                aB.StartSelectFromPCs = ManualSelectFriend;
                aB.StartSelectFromEnemies = ManualSelectEnemy;
                aB.StartSelectAllPCs = ManualSelectAllFriends;
                aB.StartSelectAllEnemeies = ManualSelectAllEnemies;
                aB.StartSelectAllPCsButCurrent = ManualSelectAllFriendsButCurrent;               
                aB.InstantiateInWorldSpaceCanvas = InstantiateInWorldSpaceCanvas;
            }
        }  
        foreach(GameObject en in objectsInBattle.enemiesInBattle)
        {
            tempListToSet = en.GetComponent<BaseEnemy>().abilities; //TODO: Decouple this
            foreach (Ability aB in tempListToSet)
            {
                aB.StartSelectFromPCs = AutoSelectFriend;
                aB.StartSelectFromEnemies = AutoSelectEnemy;
                aB.StartSelectAllPCs = AutoSelectAllFriends;
                aB.StartSelectAllEnemeies = AutoSelectAllEnemies;
                aB.InstantiateInWorldSpaceCanvas = InstantiateInWorldSpaceCanvas;
            }
        }
    }

    public override void UpdateGameSegment()
    {
        if(ManualSelectionTasks.Count == 0)
        {
            SwitchHeroBehavior();
        }
        else
        {
            for(int i = 0; i < ManualSelectionTasks.Count; i++)
            {
                ManualSelectionTasks[i].CurSelectionBehavior(ManualSelectionTasks[i]);
            }
        }


        for (int j = AutoSelectionTasks.Count -1; j >= 0 ; j--)
        {
            AutoSelectionTasks[j].CurSelectionBehavior(AutoSelectionTasks[j]);
        }

        UpdateAbilityMenu();
    }





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
            //CurSelectionBehavior += SwitchHeroBehavior;//WaitForAbilityAndUpdateDisplay;
            curHero.GetComponent<BaseBattleActor>().DoAbility(abilityToPass);
           
            
            //curState = BattleMenuStates.WaitingOnAbilitySystem;
        }
        //else just do nothing and continue
    }


    void UpdateAbilityMenu()//ToDo: move setting the actual circle and such to the AbilityView. Time till cooldown end should probably still be calculated here or perhaps on the ability itself
    {
        //AbilityCluster tempCluster;
        AbilityButton curButton;
        //this will grey out abilities that are curently not useable and update a recharge bar
        for(int i = 0; i < abilityClusters.Count; i++)
        {
            for(int j = 0; j < abilityClusters[i].abilityButtons.Count; j++)
            {
                curButton = abilityClusters[i].abilityButtons[j];
                if (curButton.uIButton.activeSelf)
                {
                    curButton.abilityView.UpdateAbility(curButton.ability);
                }
            }
        }
    }
    #endregion HeroSelected




    private const float delaySwitchTime = .5f;

    private float nextSwitchAllowedTime = 0f;


   

    public void SwitchHeroBehavior()
    {
        curHero = SwitchObjectOnInput(objectsInBattle.pcsInBattle, primaryCursor , curHero); //We could get rid of this check by haveing an overall cur selected object that the method modifies only when a new selection was successfull
        PopulateHeroMenu();
        TriggerAbilityOnInput();
        //UpdateAbilityMenu();
    }



    public float selectionSensitivityThreshhold = 0.1f;

    public float selectionRepeatDealy = 0.25f;

    public float selectionConeAngle = 35f;

    public float selectionConeAngleExapnsionIncrement = 35f;

    private float modifiedSelectionConeAngle;

    

    private GameObject selectedObject;

    public GameObject SwitchObjectOnInput(List<GameObject> objectsToSwitch, GameObject cursor, GameObject curSelected)
    {
        if(SwitchDelayOver())
        {
            float yVal = Input.GetAxis("Vertical");
            float xVal = Input.GetAxis("Horizontal");

            if (OverSensitivityThreshold(xVal, yVal))
            {
                GameObject curClosest = null;
                GameObject newSelection = null;

                for (int i = 0; i < objectsToSwitch.Count; i++)
                {
                    if(NotCurrentSelection(curSelected, objectsToSwitch[i]))
                    {
                        Vector3 normalizedTargetDir = FindDirectionOfTarget(objectsToSwitch[i], curSelected);

                        Vector3 normalizedInputDir = NormalizeInputDirection(xVal,yVal);

                        float degrees = FindDegreesToCheckAgainstAngle(normalizedTargetDir, normalizedInputDir);

                        if (InsideCone(degrees, modifiedSelectionConeAngle))
                        {
                            curClosest = GetClosest(curSelected, curClosest, objectsToSwitch[i]);
                        }
                    }
                }
                newSelection = curClosest;

                if(newSelection != null)
                {
                    
                    
                    cursor.transform.position = newSelection.transform.position + cursorOffset;
                    cursor.transform.SetParent(newSelection.transform);
                    nextSwitchAllowedTime = Time.time + selectionRepeatDealy;

                    modifiedSelectionConeAngle = selectionConeAngle;
                    return newSelection;
                }
                else if (modifiedSelectionConeAngle <= 89f)
                {
                    if (selectionConeAngleExapnsionIncrement <= 1)
                    {
                        Debug.LogError("Expansion angle too small ");

                    }
                    modifiedSelectionConeAngle += selectionConeAngleExapnsionIncrement;
                    modifiedSelectionConeAngle = Mathf.Clamp(modifiedSelectionConeAngle, 0f, 90f);
                    newSelection = SwitchObjectOnInput(objectsToSwitch, cursor, curSelected); //TODO: make this recursion a while loop instead. more better.
                    return newSelection;
                }
            }
        }
        return curSelected;
    }

    #region SwitchObjectOnInputMethods
    private bool SwitchDelayOver()
    {
        return (Time.time > nextSwitchAllowedTime);
    }
    private bool OverSensitivityThreshold(float yVal, float xVal)
    {
        return Mathf.Abs(yVal) + Mathf.Abs(xVal) > selectionSensitivityThreshhold;
    }
    private bool NotCurrentSelection(GameObject curSelection, GameObject objectToCheck)
    {
        return curSelection != objectToCheck;
    }
    private Vector3 FindDirectionOfTarget(GameObject target, GameObject curSelected)
    {
        Vector3 dir = target.transform.position - curSelected.transform.position;
        dir.Normalize();
        return (dir);
    }
    private Vector3 NormalizeInputDirection(float xVal, float yVal)
    {
        return new Vector3(xVal, yVal).normalized;
    }
    private float FindDegreesToCheckAgainstAngle(Vector3 normalizedTargetDir, Vector3 normalizedInputDir)
    {
        float dotProduct = Vector3.Dot(normalizedTargetDir, normalizedInputDir);
        return Mathf.Rad2Deg * Mathf.Acos(dotProduct);
    }
    private bool InsideCone(float degrees, float coneAngleSize)
    {
        if (degrees < coneAngleSize)
        {
            return (true);
        }
        else
        {
            return (false);
        }
    }
    private GameObject GetClosest(GameObject curSelected, GameObject curClosest, GameObject possibleClosest)
    {
        if (curClosest == null)
        {
            curClosest = possibleClosest;
        }
        else
        {
            if (Vector3.SqrMagnitude(curSelected.transform.position - curClosest.transform.position) > Vector3.SqrMagnitude(curSelected.transform.position - possibleClosest.transform.position))
            {
                return possibleClosest;
            }
        }
        return curClosest;
    }
    #endregion SwitchObjectOnInputMethods

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


    #region SelectionMethods

    //List<GameObject> objectsToSwtichBetween = new List<GameObject>();



    private delegate List<GameObject> DelGetRelations(Type requesterType);


        


    #region ManualSelectionMethods
    private void ManualSelectFriend(SubAbility subAb, Type requesterType)
    {
        List<GameObject> objectsForSelection;
        objectsForSelection = objectsInBattle.GetFriendsOfType(requesterType);
        StartManualSelection(subAb, SecondarySelection, objectsForSelection);
    }

    private void ManualSelectEnemy(SubAbility subAb, Type requesterType)
    {
        List<GameObject> objectsForSelection;
        objectsForSelection = objectsInBattle.GetOpponentsOfType(requesterType);
        StartManualSelection(subAb, SecondarySelection, objectsForSelection);
    }
    private void ManualSelectAllFriends(SubAbility subAb, Type requesterType)
    {
        List<GameObject> objectsForSelection;
        objectsForSelection = objectsInBattle.GetFriendsOfType(requesterType);
        StartManualSelection(subAb, OneGroupSelection, objectsForSelection);
    }
    private void ManualSelectAllEnemies(SubAbility subAb, Type requesterType)
    {
        List<GameObject> objectsForSelection;
        objectsForSelection = objectsInBattle.GetOpponentsOfType(requesterType);
        StartManualSelection(subAb, OneGroupSelection, objectsForSelection);
    }
    private void ManualSelectAllFriendsButCurrent(SubAbility subAb, Type requesterType)//TODO: maybe Just make this a call back rather than passing the whole sub ability
    {
        List<GameObject> objectsForSelection;

        objectsForSelection = new List<GameObject>(objectsInBattle.GetFriendsOfType(requesterType));

        for (int i = 0; i < objectsForSelection.Count; i++)
        {
            if (objectsForSelection[i] == curHero)
            {
                objectsForSelection.RemoveAt(i);
                break;
            }
        }

        StartManualSelection(subAb, OneGroupSelection, objectsForSelection);
    }
    #endregion ManualSelectionMethods

    #region AutoSelectionMethods
    private void AutoSelectFriend(SubAbility subAb, Type requesterType)
    {
        List<GameObject> objectsForSelection;
        objectsForSelection = objectsInBattle.GetFriendsOfType(requesterType);
        StartAutoSelection(subAb, AISingleRandomSelection, objectsForSelection);
    }
    private void AutoSelectEnemy(SubAbility subAb, Type requesterType)
    {
        List<GameObject> objectsForSelection;
        objectsForSelection = objectsInBattle.GetOpponentsOfType(requesterType);
        StartAutoSelection(subAb, AISingleRandomSelection, objectsForSelection);
    }



    private void AutoSelectAllFriends(SubAbility subAb, Type requesterType)
    {
        List<GameObject> objectsForSelection;
        objectsForSelection = objectsInBattle.GetFriendsOfType(requesterType);
        StartAutoSelection(subAb, AIOneGroupSelection, objectsForSelection);
    }
    private void AutoSelectAllEnemies(SubAbility subAb, Type requesterType)
    {
        List<GameObject> objectsForSelection;
        objectsForSelection = objectsInBattle.GetOpponentsOfType(requesterType);
        StartAutoSelection(subAb, AIOneGroupSelection, objectsForSelection);
    }



    #endregion AutoSelectionMethods



    private void StartManualSelection(SubAbility subAb, DelCurSelectionBehavior SelectionBehavior, List<GameObject> objectsForSelection)
    {
        curSecondarySelection = curHero; //This seems like a bad side effect. Any way to integrate this with the rest of what is going on
        secondaryCursor.transform.position = curSecondarySelection.transform.position + cursorOffset;
        secondaryCursor.SetActive(true);
        SelectionTask selection = new SelectionTask(SelectionBehavior, objectsForSelection, subAb.OnSelectionFinished);
        ManualSelectionTasks.Add(selection);

    }

    private void StartAutoSelection(SubAbility subAb, DelCurSelectionBehavior SelectionBehavior, List<GameObject> objectsForSelection)
    {
        SelectionTask selection = new SelectionTask(SelectionBehavior, objectsForSelection, subAb.OnSelectionFinished);
        AutoSelectionTasks.Add(selection);

    }

    private void EndAutoSelection(SelectionTask selectionTask, List<GameObject> tempSelectedObjects)
    {
        selectionTask.SelectionFinishedCallback(tempSelectedObjects);
        AutoSelectionTasks.Remove(selectionTask);
    }

    private void EndManualSelection(SelectionTask selectionTask, List<GameObject> tempSelectedObjects)
    {
        selectionTask.SelectionFinishedCallback(tempSelectedObjects);
        ManualSelectionTasks.Remove(selectionTask);
    }
    public void SecondarySelection(SelectionTask selectionTask)
    {
        curSecondarySelection = SwitchObjectOnInput(selectionTask.objectsForSelection, secondaryCursor, curSecondarySelection);
        if (Input.GetButtonDown("A"))
        {
            List<GameObject> tempSelectedObjects = new List<GameObject>();
            tempSelectedObjects.Add(curSecondarySelection);
            EndManualSelection(selectionTask, tempSelectedObjects);
            secondaryCursor.SetActive(false);
        }
    }

    public void AISingleRandomSelection(SelectionTask selectionTask)
    {
        int randomOpponentIndx = UnityEngine.Random.Range(0, selectionTask.objectsForSelection.Count);
        List<GameObject> tempSelectedObjects = new List<GameObject>();
        tempSelectedObjects.Add(selectionTask.objectsForSelection[randomOpponentIndx]);
        EndAutoSelection(selectionTask,tempSelectedObjects);
    }

    private void OneGroupSelection(SelectionTask selectionTask) //this is the old school way of doing this. need to make partially see through selection indicators and turn them on and off or move all of them. This does nothing except visually show the selection and wait for confirmation
    {
        
        secondaryCursor.transform.position = selectionTask.objectsForSelection[genericSwitchIndx].transform.position +cursorOffset;
        genericSwitchIndx++;
        if (genericSwitchIndx >= selectionTask.objectsForSelection.Count)
        {
            genericSwitchIndx = 0;
        }
        if (Input.GetButtonDown("A"))
        {
            EndManualSelection(selectionTask, selectionTask.objectsForSelection);
            secondaryCursor.SetActive(false);
        }
    }

    private void AIOneGroupSelection(SelectionTask selectionTask)
    {
        EndAutoSelection(selectionTask,selectionTask.objectsForSelection);
    }

    #endregion SelectionMethods

    private void KickOffMiniGame(MiniGame mG)
    {
        miniGames.Add(mG);
        mG.StartMiniGame(this);
    }

    #region ExternalUIMethods
    public GameObject InstantiateInWorldSpaceCanvas(GameObject toInstantiate, Vector3 position)
    {
        return GameObject.Instantiate(toInstantiate, position, Quaternion.identity, worldSpaceCanvas.transform);
    }


    #endregion ExternalUIMethods
    #endregion ExternalMethods

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