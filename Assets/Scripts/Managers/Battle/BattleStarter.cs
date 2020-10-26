﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleStarter : MonoBehaviour
{

    public GameObject battleFolder;
    public GameObject overworldFolder;
    public BattleSelectionManager battleMenuManager;

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

    public MVCHelper battleTypeDisplay;

    public MVCHelper victoryDisplay;

    public Status victory;

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


    public void TurnOnBattleScene(BattleDef recivedBattleDef)
    {
        battleDef = recivedBattleDef;
        SetUpBattle();
        battleFolder.SetActive(true);
        Debug.Log("SET OVERWORLD TO FALSE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        overworldFolder.SetActive(false);
        Invoke("ShowTitle", .3f);

    }

    private void ShowTitle()
    {
        battleTypeDisplay.StartUI(battleDef.encounterType.ToString());
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


        objectsInBattle.ExitBattle = Victory;

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
            pcBlanks.Add(pc.battler);
            objectsInBattle.AddPCToList(pc.battler);
            pc.battler.SetActive(true);
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

    private List<string> victoryInfo = new List<string>();
    public void Victory(float gold, float exp)
    {
        Debug.Log("victory happened");
        victoryInfo.Add("Victory!");
        victoryInfo.Add(gold.ToString());
        victoryInfo.Add(exp.ToString());
        BattleStats expCarrier = new BattleStats();
        expCarrier.modified = new Stats();
        expCarrier.modified.exp = exp/objectsInBattle.pcsInBattle.Count;

        foreach(GameObject bPC in objectsInBattle.pcsInBattle)
        {
            Status statusToAdd;
            statusToAdd = victory.CreateStatusInstance(expCarrier);
            bPC.GetComponent<BattlePC>().AddStatus(statusToAdd);

            if((bPC.GetComponent<BattlePC>().stats.modified.exp + expCarrier.modified.exp) > bPC.GetComponent<BattlePC>().stats.modified.nextLevel)
            {
                victoryInfo.Add(bPC.GetComponent<BattlePC>().displayName + " Leveled Up");
            }
        }
        victoryDisplay.StartUI(victoryInfo);
    }

    public void Defeat()
    {
        victoryInfo.Add("temporary defeat screen");
        victoryDisplay.StartUI(victoryInfo);
    }

    public void ExitBattle()
    {
        foreach (GameObject bPC in objectsInBattle.pcsInBattle)
        {
            bPC.GetComponent<BaseBattleActor>().EndBattle();
        }

        foreach (GameObject blank in pcBlanks)
        {
            blank.SetActive(false);
        }
        pcBlanks.Clear();
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

