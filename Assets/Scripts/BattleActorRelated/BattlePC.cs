using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BattlePC : BaseBattleActor //TODO: Get rid of this class
{

    private List<float> expLevels;

    public int level;
    public bool AddExp(float exp)
    {
        stats.basic.exp += exp;
        if(stats.basic.exp >= expLevels[level+1])
        {
            level = level + 1;
            stats.basic.exp = stats.basic.exp - expLevels[level];
            LevelUp();
            return true;
        }

        return false;

    }

    private void LevelUp()
    {

    }

}
