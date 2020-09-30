using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConfirmModel : UIMVC
{
    public override void MVCStart(object obj)
    {
        base.MVCStart(obj);
        mVCHelper.CallEvent(UIEvents.dataChanged, obj);
    }
}
