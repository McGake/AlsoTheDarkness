using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLineModel : UIMVC
{

    private string line;

    public override void MVCSetup(object obj)
    {
        base.MVCSetup(obj);
    }

    public override void MVCStart(object obj)
    {
        base.MVCStart(obj);
        line = (string)obj;
        mVCHelper.CallEvent(UIEvents.dataChanged, line);
    }
}
