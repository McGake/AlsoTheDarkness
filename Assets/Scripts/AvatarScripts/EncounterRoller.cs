using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterRoller : MonoBehaviour
{

    private EnemyMix selectedEnemyMix;

    public GameObject battleScene;
    public Camera overWorldCam;
    public Camera battleCam;


    private BattleDef battleDef;

    public BattleDef RollEncounter(DangerZoneDef dZ)
    {
        
        if(RollIfEncounterHappened(dZ))
        {
            battleDef = new BattleDef();
           battleDef.encounterType = RollEncounterType(dZ);
            battleDef.enemyMix = RollEnemyMix(dZ);
            return (battleDef);
        }
        else
        {
            return (null);
        }
    }

    public bool RollIfEncounterHappened(DangerZoneDef dZ)
    {
        int roll = Random.Range(0, 99);

        //Debug.Log("encounter rolled " + "prob: " + dZ.encounterProb + " roll " + roll);

        if (roll > dZ.encounterProb)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private EncounterTypes RollEncounterType(DangerZoneDef dZ)
    {

        float totalProb = 0;
        foreach (EncounterTypeProb eTP in dZ.encounterTypesProb)
        {
            totalProb += eTP.prob;
        }

        float floatRoll = Random.Range(1, totalProb);
        float curProbSpace = 0f;

        foreach (EncounterTypeProb eTP in dZ.encounterTypesProb)
        {
            curProbSpace += eTP.prob;
            if (curProbSpace >= floatRoll)
            {
                return (eTP.encounterType);

            }

        }
        Debug.LogError("after rolling positive for encounter, we did not roll an encounter type. This should never happen.");
        return (EncounterTypes.none);
    }

    private EnemyMix RollEnemyMix(DangerZoneDef dZ)
    {
        float totalProb = 0;
        foreach (EnemyMixProb eM in dZ.enemyMixTypesProb)
        {
            totalProb += eM.prob;
        }

        float floatRoll = Random.Range(1, totalProb);
        float curProbSpace = 0f;

        foreach (EnemyMixProb eM in dZ.enemyMixTypesProb)
        {
            curProbSpace += eM.prob;
            if (curProbSpace >= floatRoll)
            {
                return(eM.enemyMix);
            }

        }

        Debug.LogError("after rolling positive for encounter, we did not succesfully roll an enemy mix for the encounter. This should never happen");
        return (null);
    }

    //private void LoadEncounter()
    //{
    //    battleDef.pcsInBattle = 
    //    battleStarter.GetComponent<BattleStarter>().StartBattle(battleDef);
    //    //load encounter (or switch cams if doing everything in one scene) here
    //    Debug.Log("load encounter here");
    //    battleScene.SetActive(true);
    //    overWorldCam.enabled = false;
    //    battleCam.enabled = true;
    //}


}
