using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectsInBattle : MonoBehaviour
{

    public static ObjectsInBattle objectsInBattle; //semi singleton so that abilities can access it without having to search for it, not sure if this is a good idea
   public List<GameObject> pcsInBattle;
   public  List<GameObject> enemiesInBattle;

    public BattlePC[] tempPCs;
    public BaseEnemy[] tempEnemies;

 

    public void GetObjectsInBattle()
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
    }

}
