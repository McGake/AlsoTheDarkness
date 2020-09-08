using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;
using System.Xml;
using UnityEditor;

public class BattleMenuManager : GameSegment //TODO: I think the best way to break this class up is to make seperate monobehaviors for Manual, and Auto Selection and also a seperate mono behavior for manual selecting abilities that references the currently selected pc. The auto and manual can implement the same interface.
{
    [SerializeField]
    private GameObject primaryCursor, secondaryCursor;
    
    [SerializeField]
    private Transform worldSpaceCanvas;

    [SerializeField]
    private GameObject curHeroAbilitiesDisplay;

    private ObjectsInBattle objectsInBattle;

    private GameObject curHero;

    private GameObject curSecondarySelection;

    private SelectObject selectObject = new SelectObject();

    private MenuManagerView view = new MenuManagerView();

    private delegate void DelCurSelectionBehavior(SelectionTask selectionTask);
    private delegate void DelSelectionFinishedCallback(List<GameObject> selectedObjects);
    
    private class SelectionTask
    {
        public DelCurSelectionBehavior CurSelectionTask;
        public List<GameObject> objectsForSelection;
        public DelSelectionFinishedCallback SelectionFinishedCallback;

        public SelectionTask(DelCurSelectionBehavior CurSelectionBehavior, List<GameObject> objectsForSelection, DelSelectionFinishedCallback SelectionFinishedCallback)
        {
            this.CurSelectionTask = CurSelectionBehavior;
            this.objectsForSelection = objectsForSelection;
            this.SelectionFinishedCallback = SelectionFinishedCallback;
        }
    }

    private List<SelectionTask> AutoSelectionTasks = new List<SelectionTask>();
    private List<SelectionTask> ManualSelectionTasks = new List<SelectionTask>();

    public void SetupBattleMenuManager()
    {
        FindExternalReferences();
        AddAbilityClustersToDictionary();
        AddAllAbilitybuttonsToListInProperOrder();
        AddCallbacksToAbilities();
        SetInitialPCSelectionVars();
        view.PopulateHeroAbilityMenu(curHero);

        gameStateMachine.SetCurrentGameSegment(this); //this is what actually kicks off the battle being updated. I wonder if I should somehow put this in the battle start script
        gameStateMachine.SetDefaultGameSegment(this);

    }

    #region SetupMethods
    void FindExternalReferences()
    {
        objectsInBattle = FindObjectOfType<ObjectsInBattle>();
    }

    void AddAbilityClustersToDictionary()
    {
        //this will eventually add all the ability button clusters open, righ trigger down, left trigger down, and both down
        view.abilityDisplayClusters.Add(curHeroAbilitiesDisplay.GetComponentInChildren<AbilityCluster>());
        view.curAbilityCluster = view.abilityDisplayClusters[0]; //temp code          
    }

    void AddAllAbilitybuttonsToListInProperOrder()
    {
        //this will eventually go through all the clusters to get all of the abilities
        AbilityCluster tempCluster = view.abilityDisplayClusters[0];
        foreach (AbilityButton aB in tempCluster.abilityButtons)
        {
            view.allAbilityButtons.Add(aB);
        }
    }

    void AddCallbacksToAbilities() 
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

    void SetInitialPCSelectionVars()
    {
        curHero = objectsInBattle.pcsInBattle[0];
        view.Highlight(curHero, primaryCursor);
        //modifiedSelectionConeAngle = selectionConeAngle;
    }
    #endregion SetupMethods
    public override void UpdateGameSegment()
    {
        RunCurrentMenuBehaviors();
        view.UpdateAbilityMenu();
    }

    #region UpdateMethods

    private void RunCurrentMenuBehaviors()
    {
        if (ManualSelectionTasks.Count == 0)
        {
            RunBaseHeroSelectionTask();
        }
        else
        {
            RunManualSelectionTasks();
        }

        RunAutoSelectionTasks();
    }
    #region RunCurrentMenuBehaviorsMethods
    private void RunBaseHeroSelectionTask()
    {
        ManualSelectCurHero();
    }

    public void ManualSelectCurHero()
    {
        Vector2 dirInput = MultiInput.GetPrimaryDirection();
        curHero = selectObject.ByDirection(objectsInBattle.pcsInBattle, dirInput, curHero);
        view.Highlight(curHero, primaryCursor);
        view.PopulateHeroAbilityMenu(curHero);
        TriggerAbilityOnInput();
    }

    #region ManualSelectCurHeroMethods

    List<Ability> curCombatAbilities = new List<Ability>();

    void TriggerAbilityOnInput()
    {
        const int aIndx = 0;
        const int xIndx = 1;
        const int yIndx = 2;
        const int bIndx = 3;

        if (Input.GetButtonDown("A"))
        {
            StartAbilityAtIndx(aIndx);
        }
        if (Input.GetButtonDown("X"))
        {
            //  Debug.Log("x");
            StartAbilityAtIndx(xIndx);
        }
        if (Input.GetButtonDown("Y"))
        {
            //  Debug.Log("y");
            StartAbilityAtIndx(yIndx);
        }
        if (Input.GetButtonDown("B"))
        {
            // Debug.Log("b");
            StartAbilityAtIndx(bIndx);
        }
    }

    void StartAbilityAtIndx(int indx)
    {
        Ability abilityToPass = null;
        foreach (Ability ab in curCombatAbilities)
        {
            if (ab == view.curAbilityCluster.abilityButtons[indx].ability)
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
            curHero.GetComponent<BaseBattleActor>().DoAbility(abilityToPass);
        }
        //else just do nothing and continue
    }
    #endregion ManualSelectuCurHero

    private void RunManualSelectionTasks()
    {
        for (int i = 0; i < ManualSelectionTasks.Count; i++)
        {
            ManualSelectionTasks[i].CurSelectionTask(ManualSelectionTasks[i]);
        }
    }

    private void RunAutoSelectionTasks()
    {
        for (int j = AutoSelectionTasks.Count - 1; j >= 0; j--)
        {
            AutoSelectionTasks[j].CurSelectionTask(AutoSelectionTasks[j]);
        }
    }

    #endregion RunCurrentMenuBehaviorMethods

    #endregion UpdateMethods

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

    #region SelectionMethods

    private delegate List<GameObject> DelGetRelations(Type requesterType);

    #region ManualSelectionMethods
    private void ManualSelectFriend(SubAbility subAb, Type requesterType)
    {
        List<GameObject> objectsForSelection;
        curSecondarySelection = curHero; //This seems like a bad side effect. Any way to integrate this with the rest of what is going on
        view.Highlight(curSecondarySelection, secondaryCursor);
        objectsForSelection = objectsInBattle.GetFriendsOfType(requesterType);
        StartManualSelection(subAb, SecondarySelection, objectsForSelection);
    }

    const int defaultFirstObject = 0;
    private void ManualSelectEnemy(SubAbility subAb, Type requesterType)
    {
        List<GameObject> objectsForSelection;
        objectsForSelection = objectsInBattle.GetEnemiesOfType(requesterType);
        curSecondarySelection = objectsForSelection[defaultFirstObject]; //This seems like a bad side effect. Any way to integrate this with the rest of what is going on
        view.Highlight(curSecondarySelection, secondaryCursor);
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
        objectsForSelection = objectsInBattle.GetEnemiesOfType(requesterType);
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
        objectsForSelection = objectsInBattle.GetEnemiesOfType(requesterType);
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
        objectsForSelection = objectsInBattle.GetEnemiesOfType(requesterType);
        StartAutoSelection(subAb, AIOneGroupSelection, objectsForSelection);
    }



    #endregion AutoSelectionMethods

    #region KickOffAndEndSelectionsMethods
    private void StartManualSelection(SubAbility subAb, DelCurSelectionBehavior SelectionBehavior, List<GameObject> objectsForSelection)
    {
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

    #endregion KickOffAndEndSelectionsMethods

    #region SelectionTaskDefinitions
    private void SecondarySelection(SelectionTask selectionTask)
    {
        Vector2 dir = MultiInput.GetPrimaryDirection();
        curSecondarySelection = selectObject.ByDirection(selectionTask.objectsForSelection, dir, curSecondarySelection);
        if (Input.GetButtonDown("A"))
        {
            List<GameObject> tempSelectedObjects = new List<GameObject>();
            tempSelectedObjects.Add(curSecondarySelection);
            EndManualSelection(selectionTask, tempSelectedObjects);
            secondaryCursor.SetActive(false);
        }
    }

    private void AISingleRandomSelection(SelectionTask selectionTask)
    {
        List<GameObject> tempSelectedObjects = new List<GameObject>();
        tempSelectedObjects.Add(selectObject.Randomly(selectionTask.objectsForSelection));
        EndAutoSelection(selectionTask,tempSelectedObjects);
    }

    int genericSwitchIndx;

    private void OneGroupSelection(SelectionTask selectionTask) 
    {
        
        view.Highlight(selectionTask.objectsForSelection[genericSwitchIndx], secondaryCursor); //This switchs rapidly between to make a see through effect. we need to add see through sprites so that we dont have to do this
        genericSwitchIndx++;
        if (genericSwitchIndx >= selectionTask.objectsForSelection.Count)
        {
            genericSwitchIndx = 0;
        }
        if (Input.GetButtonDown("A"))
        {
            List<GameObject> tempSelectedObjects = new List<GameObject>();
            tempSelectedObjects = selectObject.ByEntireGroup(selectionTask.objectsForSelection);
            EndManualSelection(selectionTask, tempSelectedObjects);
            secondaryCursor.SetActive(false);
        }
    }

    private void AIOneGroupSelection(SelectionTask selectionTask)
    {
        List<GameObject> tempSelectedObjects = new List<GameObject>();
        tempSelectedObjects = selectObject.ByEntireGroup(selectionTask.objectsForSelection);
        EndAutoSelection(selectionTask,tempSelectedObjects);
    }
    #endregion SelectionTaskDefinitions

    #endregion SelectionMethods

    private void KickOffMiniGame(MiniGame mG)
    {
        miniGames.Add(mG);
        mG.StartMiniGame(this);
    }

    private GameObject InstantiateInWorldSpaceCanvas(GameObject toInstantiate, Vector3 position)
    {
        return GameObject.Instantiate(toInstantiate, position, Quaternion.identity, worldSpaceCanvas.transform);
    }
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

public class SelectObject
{

    private const float delaySwitchTime = .5f;

    private float nextSwitchAllowedTime = 0f;

    public float selectionSensitivityThreshhold = 0.1f;

    public float selectionRepeatDealy = 0.25f;

    public float selectionConeAngle = 35f;

    public float selectionConeAngleExapnsionIncrement = 35f;

    private float modifiedSelectionConeAngle;


    public GameObject ByDirection(List<GameObject> objectsToSwitch, Vector2 direction, GameObject curSelected)
    {
        if (SwitchDelayOver())
        {
            float yVal = direction.y;
            float xVal = direction.x;

            if (OverSensitivityThreshold(xVal, yVal))
            {
                GameObject curClosest = null;
                GameObject newSelection = null;

                for (int i = 0; i < objectsToSwitch.Count; i++)
                {
                    if (NotCurrentSelection(curSelected, objectsToSwitch[i]))
                    {
                        Vector3 normalizedTargetDir = FindDirectionOfTarget(objectsToSwitch[i], curSelected);

                        Vector3 normalizedInputDir = NormalizeInputDirection(xVal, yVal);

                        float degrees = FindDegreesToCheckAgainstAngle(normalizedTargetDir, normalizedInputDir);

                        if (InsideCone(degrees, modifiedSelectionConeAngle))
                        {
                            curClosest = GetClosest(curSelected, curClosest, objectsToSwitch[i]);
                        }
                    }
                }
                newSelection = curClosest;

                if (newSelection != null)
                {
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
                    newSelection = ByDirection(objectsToSwitch, direction, curSelected); //TODO: make this recursion a while loop instead. more better.
                    return newSelection;
                }
            }
        }
        return curSelected;
    }

    #region ByDirectionMethods
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
    #endregion ByDirectionMethods

    public GameObject Randomly(List<GameObject> objectsToSwitch)
    {
        int randomIndx = UnityEngine.Random.Range(0, objectsToSwitch.Count);

        return objectsToSwitch[randomIndx];
    }

    public List<GameObject> ByEntireGroup(List<GameObject> objectsToSwitch)
    {
        return objectsToSwitch;
    }
}

public class MenuManagerView
{
    public List<AbilityButton> allAbilityButtons = new List<AbilityButton>();

    private Vector3 cursorOffset = new Vector3(-.66f, .22f, 0f);

    public List<AbilityCluster> abilityDisplayClusters = new List<AbilityCluster>();

    public AbilityCluster curAbilityCluster;

    public void Highlight(GameObject objectToShowSelected, GameObject cursor)
    {
        Highlight(objectToShowSelected, cursor, cursorOffset);
    }
    public void Highlight(GameObject objectToShowSelected, GameObject cursor, Vector3 offset)
    {
        cursor.transform.position = objectToShowSelected.transform.position + offset;

        cursor.transform.parent = objectToShowSelected.transform;

        if(cursor.activeSelf == false)
        {
            cursor.SetActive(true);
        }
    }


    private List<Ability> curCombatAbilities;
    private GameObject curHero;
    public void PopulateHeroAbilityMenu(GameObject curHero)
    {
       this.curHero = curHero;
       GetBattleAbilities();
       SetDisplayToNewAbilities();
    }

    public void SetDisplayToNewAbilities()
    {
        for (int i = 0; i < curCombatAbilities.Count; i++)
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

    public void GetBattleAbilities()
    {
        List<Ability> allAbilitiesOnPC = curHero.GetComponent<BattlePC>().abilities;
        curCombatAbilities = Ability.NewRetrunOnlyAbilitiesOfContext(UsableContexts.battleAbilityMenu, allAbilitiesOnPC);
    }

    public void UpdateAbilityMenu()//ToDo: move setting the actual circle and such to the AbilityView. Time till cooldown end should probably still be calculated here or perhaps on the ability itself
    {
        //AbilityCluster tempCluster;
        AbilityButton curButton;
        //this will grey out abilities that are curently not useable and update a recharge bar
        for (int i = 0; i < abilityDisplayClusters.Count; i++)
        {
            for (int j = 0; j < abilityDisplayClusters[i].abilityButtons.Count; j++)
            {
                curButton = abilityDisplayClusters[i].abilityButtons[j];
                if (curButton.uIButton.activeSelf)
                {
                    curButton.abilityView.UpdateAbility(curButton.ability);
                }
            }
        }
    }


}


public interface ISelect
{

    GameObject FromFriends();

    GameObject FromEnemies();

    GameObject FromPCs();

    GameObject FromMonsters();

    List<GameObject> AllFriends();

    List<GameObject> AllEnemies();

    List<GameObject> AllPCs();

    List<GameObject> AllMonsters();

    List<GameObject> allFriendsButCurrent();
}
public class PCSelectionMethods : ISelect
{
    public List<GameObject> AllEnemies()
    {
        throw new NotImplementedException();
    }

    public List<GameObject> AllFriends()
    {
        throw new NotImplementedException();
    }

    public List<GameObject> allFriendsButCurrent()
    {
        throw new NotImplementedException();
    }

    public List<GameObject> AllMonsters()
    {
        throw new NotImplementedException();
    }

    public List<GameObject> AllPCs()
    {
        throw new NotImplementedException();
    }

    public GameObject FromEnemies()
    {
        throw new NotImplementedException();
    }

    public GameObject FromFriends()
    {
        throw new NotImplementedException();
    }

    public GameObject FromMonsters()
    {
        throw new NotImplementedException();
    }

    public GameObject FromPCs()
    {
        throw new NotImplementedException();
    }
}

public class MonsterSelectionMethods : ISelect
{
    public List<GameObject> AllEnemies()
    {
        throw new NotImplementedException();
    }

    public List<GameObject> AllFriends()
    {
        throw new NotImplementedException();
    }

    public List<GameObject> allFriendsButCurrent()
    {
        throw new NotImplementedException();
    }

    public List<GameObject> AllMonsters()
    {
        throw new NotImplementedException();
    }

    public List<GameObject> AllPCs()
    {
        throw new NotImplementedException();
    }

    public GameObject FromEnemies()
    {
        throw new NotImplementedException();
    }

    public GameObject FromFriends()
    {
        throw new NotImplementedException();
    }

    public GameObject FromMonsters()
    {
        throw new NotImplementedException();
    }

    public GameObject FromPCs()
    {
        throw new NotImplementedException();
    }
}