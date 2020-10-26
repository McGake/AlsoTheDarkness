using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySellExitModel : UIMVC
{

    public List<GameObject> BuySellExitButtons;

    public override void MVCStart(object obj)
    {
        base.MVCStart(obj);
        mVCHelper.CallEvent(UIEvents.dataChanged, BuySellExitButtons);
    }

    public override void MVCEnd(object obj)
    {
        //this prevents calling setactive false
    }
}
