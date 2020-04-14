using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{

    public GameObject battleFolder;
    public GameObject overworldFolder;

    public List<GameObject> pcBlanks;

    private BattleDef bd;

    public ObjectsInBattle objectsInBattle;

    const int maxBlanksCount = 4;

    public BattleDef defaultBd;

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
    }

    public void SendBattleDef(BattleDef recivedBattleDef)
    {
        bd = recivedBattleDef;
    }

    public void TurnOnBattleScene()
    {
        battleFolder.SetActive(true);
        overworldFolder.SetActive(false);
    }

    public void SetUpBattle()
    {
        
        if(bd==null)
        {
            bd = defaultBd;
        }
        SetupBackground();
        SetupEncounter();
        SetupSituations();
        SetupPCBlanks();
        SetupMonsters();




        battleFolder.SetActive(true);
        objectsInBattle.GetObjectsInBattle();
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

    }

    private void SetupSituations()
    {

    }

    private void SetupPCBlanks()
    {
        int curPCBlankIndex = 0;
        foreach (PC pc in bd.pcsInBattle)
        {
            pcBlanks[curPCBlankIndex].SetActive(true);
            pcBlanks[curPCBlankIndex].GetComponent<Animator>().runtimeAnimatorController = pc.battleAnimOverride;
            curPCBlankIndex++;
            if(curPCBlankIndex >= maxBlanksCount)
            {
                break;
            }
        }
    }

    private void SetupMonsters()
    {

    }


    public void ExitBattle()
    {
        foreach(GameObject blank in pcBlanks)
        {
            blank.SetActive(false);
        }
        battleFolder.SetActive(false);
        overworldFolder.SetActive(true);
    }
}

[System.Serializable]
public class BattleDef
{
    public List<PC> pcsInBattle;
    public EncounterTypes encounterType;
    public EnemyMix enemyMix;
}


