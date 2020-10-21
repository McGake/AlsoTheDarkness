using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BattleEndController : UIMVC
{

   public BattleStarter bStarter;

    public override void MVCSetup(object obj)
    {
        base.MVCSetup(obj);
        mVCHelper.Subscribe(UIEvents.backout, MVCEnd);
    }

    public override void MVCEnd(object obj)
    {
        bStarter.ExitBattle();
        base.MVCEnd(obj);
    }
}
