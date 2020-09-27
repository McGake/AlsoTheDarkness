using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLineModel : UIMVC
{
    private string[] lines;

    public override void MVCStart(object obj)
    {
        base.MVCStart(obj);
        lines = (string[])obj;
        mVCHelper.CallEvent(UIEvents.dataChanged, lines);
    }

}
