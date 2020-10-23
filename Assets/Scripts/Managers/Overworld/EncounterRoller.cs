using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterRoller : MonoBehaviour
{
    public BattleDef RollEncounter(DangerZoneDef dZ)
    {
        
        if(RollIfEncounterHappened(dZ))
        {
            BattleDef battleDef = new BattleDef();
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

        if (roll < dZ.encounterProb)
        {
            return true;
        }
        else
        {
            return false;
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

}
