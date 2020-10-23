using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseObjectInBattle : BaseBattleActor
{
    public override void Die()
    {
        //OnDeathCallback(gameObject);
        gameObject.SetActive(false);
    }
}
