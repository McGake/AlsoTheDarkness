using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSelectionControllerRemain : ListSelectionController
{
    public override void MVCEnd(object obj)
    {
        mVCHelper.Unsubscribe(UIEvents.backout, BackoutOrExit);
        mVCHelper.Unsubscribe(UIEvents.execute, Select);
    }
}
