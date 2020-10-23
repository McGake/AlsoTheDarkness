using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterClasses : MonoBehaviour
{

}

[System.Serializable]
public class EncounterTypeProb
{
    public EncounterTypes encounterType;
    public float prob;
}

[System.Serializable]
public class EnemyMixProb
{
    public EnemyMix enemyMix;
    public float prob;
}

[System.Serializable]
public class EnemyMix
{
    public List<GameObject> encounterMonsters;
}

[System.Serializable]
public enum EncounterTypes
{
    none = 0,
    standard = 1,
    surrounded = 2,
}



