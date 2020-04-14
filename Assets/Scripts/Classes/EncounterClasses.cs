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
    public string name;
    //this should contain a list of enemies that describes an encounter eg: 2 twisted tigers and 3 evil hienas and a probability int  
    public List<string> encounterMonsters; // this should be the monster definitions themselves rather than list of strings
}

[System.Serializable]
public enum EncounterTypes
{
    none = 0,
    standard = 1,
    surrounded = 2,
}



