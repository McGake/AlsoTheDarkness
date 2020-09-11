using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleStarter : MonoBehaviour
{

    public GameObject battleFolder;
    public GameObject overworldFolder;
    public BattleMenuManager battleMenuManager;

    public List<GameObject> pcBlanks;

    private BattleDef battleDef;

    public ObjectsInBattle objectsInBattle;

    const int maxBlanksCount = 4;

    public BattleDef defaultBd;

    public List<Transform> leftSideStartPositions;

    public List<Transform> rightSideStartPositions;

    public List<Transform> centerStartPositions;

    public List<Transform> distantRightStartPositions;

    public List<Transform> centerSurroundedStartPositions;


    public List<Transform> possiblePCStartPositions;

    public List<Transform> possibleMonsterStartPositions;

      public void Update()
    {
        if(Input.GetButtonDown("Start"))//this is temporary test code
        {
            //ExitBattle();
        }
        if(MultiInput.GetAButtonDown())
        {
            //Debug.Log("a was pressed");
        }

        if(objectsInBattle.enemiesInBattle.Count <= 0)//TEMP:this is only here till we get our event system in place. then replace this with an all enemies dead listener
        {
            //ExitBattle();
        }
    }

    public void SendBattleDef(BattleDef recivedBattleDef)
    {
        battleDef = recivedBattleDef;
    }

    public void TurnOnBattleScene()
    {
        SetUpBattle();
        battleFolder.SetActive(true);
        overworldFolder.SetActive(false);
    }

    private void ResetBattle()
    {


    }

    public void SetUpBattle()
    {
        ResetBattle();
        if(battleDef==null)
        {
            battleDef = defaultBd;
        }
        SetupBackground();
        SetupEncounter();
        SetupSituations();
        SetupPCBlanks();
        SetupMonsters();


        objectsInBattle.ExitBattle = ExitBattle;

        battleFolder.SetActive(true);
        objectsInBattle.CollectObjectsInBattle();
        battleMenuManager.SetupBattleMenuManager();

        overworldFolder.SetActive(false);
    }

    public void OnEnable()
    {
        //SetUpBattle();
    }

    private void SetupBackground()
    {

    }

    private void SetupEncounter()
    {
        Debug.Log(battleDef.encounterType);
        possiblePCStartPositions = new List<Transform>();
        possibleMonsterStartPositions = new List<Transform>();
        
        if(battleDef.encounterType == EncounterTypes.standard)
        {
            possiblePCStartPositions = rightSideStartPositions;
            possibleMonsterStartPositions = leftSideStartPositions;
        }
        else if(battleDef.encounterType == EncounterTypes.surrounded)
        {
            possiblePCStartPositions = centerSurroundedStartPositions;
            possibleMonsterStartPositions.AddRange(leftSideStartPositions);
            possibleMonsterStartPositions.AddRange(rightSideStartPositions);
        }
    }

    private void SetupSituations()
    {

    }

    private void SetupPCBlanks()
    {
        int curPCBlankIndex = 0;
        List<Transform> possiblePCStartPositionsCopy = new List<Transform>();
        possiblePCStartPositionsCopy.AddRange(possiblePCStartPositions);
        foreach (PC pc in battleDef.pcsInBattle)
        {
            Transform startPosition = TakeRandomPosition(possiblePCStartPositionsCopy);
            pc.battler.transform.position = startPosition.position;
            pc.battler.transform.rotation = startPosition.rotation;
            pcBlanks[curPCBlankIndex] = pc.battler;
            objectsInBattle.AddPCToList(pcBlanks[curPCBlankIndex]);
            pcBlanks[curPCBlankIndex].SetActive(true);
            curPCBlankIndex++;
            if(curPCBlankIndex >= maxBlanksCount)
            {
                break;
            }
        }
    }

    private void SetupMonsters()
    {
        List<GameObject> encounterMonsters = battleDef.enemyMix.encounterMonsters;

        GameObject curMonster;

        if(possibleMonsterStartPositions.Count < encounterMonsters.Count)
        {
            Debug.LogError("more monsters than start positions");
        }

        List<Transform> possibleMonsterStartPositionsCopy = new List<Transform>();
        possibleMonsterStartPositionsCopy.AddRange(possibleMonsterStartPositions);

        for (int i = 0; i < encounterMonsters.Count; i++)
        {
            curMonster = Instantiate(encounterMonsters[i]);
            Transform position;

            position= TakeRandomPosition(possibleMonsterStartPositionsCopy);
            curMonster.transform.position = position.position;
            curMonster.transform.rotation = position.rotation;
        }
    }


    private Transform TakeRandomPosition(List<Transform> transforms)
    {
        Transform returnTransform;
        int randPosIndx = Random.Range(0, transforms.Count);
        returnTransform = transforms[randPosIndx];
        transforms.RemoveAt(randPosIndx);
        return (returnTransform);
    }


    public void ExitBattle()
    {
        foreach(GameObject blank in pcBlanks)
        {
            blank.SetActive(false);
        }
        battleFolder.SetActive(false);
        overworldFolder.SetActive(true);
        objectsInBattle.DestroyMonstersInBattle();
    }
}

[System.Serializable]
public class BattleDef
{
    public List<PC> pcsInBattle;
    public EncounterTypes encounterType;
    public EnemyMix enemyMix;
}


