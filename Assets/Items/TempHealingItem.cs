using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestUseItemScript", menuName = "TestScripts/hmm", order = 1)]
[System.Serializable]
public class TempHealingItem : UseItemScript
{
    public float healing;

    public override void UseItem(PC pcToUse)
    {
        Debug.Log("start player selection screen here");
        //Start player selection screen
    }
}
