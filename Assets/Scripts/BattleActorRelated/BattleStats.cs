using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BattleStats
{
    public Stats basic;
    public Stats modified;
    public Stats increaseRate;

    public BattleStats Copy()
    {
        BattleStats tempStats = new BattleStats();
        tempStats.basic = basic;
        tempStats.modified = modified;
        return tempStats;
    }

    public float GetGoverningStat(GoverningStat gS)
    {
        switch (gS)
        {
            case GoverningStat.magicalPower:
                return modified.magicalPower;
                break;
            case GoverningStat.magicalSkill:
                return modified.magicalSkill;
                break;
            case GoverningStat.physicalPower:
                return modified.physicalPower;
                break;
            case GoverningStat.physicalSkill:
                return modified.physicalSkill;
                break;
            case GoverningStat.none:
                return 1f;
                break;
        }
        Debug.LogError("we somehow searched for a governing stat that is not accounted for");
        return 0f;
    }
}
