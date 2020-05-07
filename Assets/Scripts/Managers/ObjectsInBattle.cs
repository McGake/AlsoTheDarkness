using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class ObjectsInBattle : MonoBehaviour
{

    public static ObjectsInBattle objectsInBattle; //semi singleton so that abilities can access it without having to search for it, not sure if this is a good idea
   public List<GameObject> pcsInBattle;
   public  List<GameObject> enemiesInBattle;

    public BattlePC[] tempPCs;
    public BaseEnemy[] tempEnemies;


    public Dictionary<string, int> testDict;

    public Dictionary<Type, List<GameObject>> objectsInBattleDict = new Dictionary<Type, List<GameObject>>();


    public void CollectObjectsInBattle()
    {
        pcsInBattle.Clear();
        enemiesInBattle.Clear();
        objectsInBattle = this;
        tempPCs = FindObjectsOfType<BattlePC>();
        foreach (BattlePC bpc in tempPCs)
        {
            pcsInBattle.Add(bpc.gameObject);
            
        }

        tempEnemies = FindObjectsOfType<BaseEnemy>();
        foreach(BaseEnemy be in tempEnemies)
        {
            enemiesInBattle.Add(be.gameObject);
            
        }

        objectsInBattleDict.Add(typeof(BaseEnemy), enemiesInBattle);
        objectsInBattleDict.Add(typeof(BattlePC), pcsInBattle);
    }


    public List<GameObject> GetBattleActorsOfType(Type type)
    {
        if (objectsInBattleDict.ContainsKey(type))
        {
            List<GameObject> tempList = new List<GameObject>();
            objectsInBattleDict.TryGetValue(type, out tempList);
            return (tempList);
        }
        return null;
    }


    public List<GameObject> GetFriendsOfType(Type type)
    {
        List<GameObject> tempList = new List<GameObject>();
        if (type == typeof(BaseEnemy))
        {
            objectsInBattleDict.TryGetValue(typeof(BaseEnemy), out tempList);
            return tempList;
        }
        else if (type == typeof(BattlePC))
        {
            objectsInBattleDict.TryGetValue(typeof(BattlePC), out tempList);
            return tempList;
        }
        Debug.LogError("GetFriendsOfType Has returned null. Are you trying to get a non actor type?");
        return null;
    }

    public List<GameObject> GetOpponentsOfType(Type type)
    {
        List<GameObject> tempList = new List<GameObject>();
        if (type == typeof(BaseEnemy))
        {
            objectsInBattleDict.TryGetValue(typeof(BattlePC), out tempList);
            return tempList;
        }
        else if (type == typeof(BattlePC))
        {
            objectsInBattleDict.TryGetValue(typeof(BaseEnemy), out tempList);
            return tempList;
        }
        Debug.LogError("GetFriendsOfType Has returned null. Are you trying to get a non actor type?");
        return null;
    }
}
