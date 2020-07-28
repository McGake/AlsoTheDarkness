using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleStarter : MonoBehaviour
{

    public GameObject battleFolder;
    public GameObject overworldFolder;

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
        if(Input.GetButtonDown("A"))
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
        battleFolder.SetActive(true);
        overworldFolder.SetActive(false);
    }

    public void SetUpBattle()
    {
        
        if(battleDef==null)
        {
            battleDef = defaultBd;
        }
        SetupBackground();
        SetupEncounter();
        SetupSituations();
        SetupPCBlanks();
        SetupMonsters();




        battleFolder.SetActive(true);
        objectsInBattle.CollectObjectsInBattle();
        overworldFolder.SetActive(false);
    }

    public void OnEnable()
    {
        SetUpBattle();
    }

    private void SetupBackground()
    {

    }

    private void SetupEncounter()
    {
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
        foreach (PC pc in battleDef.pcsInBattle)
        {
            pc.battler.transform.position = TakeRandomPosition(possiblePCStartPositions);
            //pcBlanks[curPCBlankIndex].transform.position = TakeRandomPosition(possiblePCStartPositions);

            //pcBlanks[curPCBlankIndex].GetComponent<Animator>().runtimeAnimatorController = pc.battleAnimOverride;

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
        for(int i = 0; i < encounterMonsters.Count; i++)
        {
            curMonster = Instantiate(encounterMonsters[i]);
            curMonster.transform.position = TakeRandomPosition(possibleMonsterStartPositions);
        }
    }


    private Vector3 TakeRandomPosition(List<Transform> transforms)
    {
        Vector3 returnVector;
        int randPosIndx = Random.Range(0, transforms.Count);
        returnVector = transforms[randPosIndx].position;
        transforms.RemoveAt(randPosIndx);
        return (returnVector);
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


