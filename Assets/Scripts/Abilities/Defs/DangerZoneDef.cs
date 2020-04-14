using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DangerZoneDef", menuName = "ScriptableObjects/DangerZone", order = 1)]
public class DangerZoneDef : ScriptableObject
{
    public string zoneName;
    public float encounterProb;
    public List<EnemyMixProb> enemyMixTypesProb;
    public List<EncounterTypeProb> encounterTypesProb;
}