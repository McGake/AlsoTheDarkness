using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLineController : UIMVC
{
    public override void MVCSetup(object obj)
    {
        base.MVCSetup(obj);
 
        mVCHelper.Subscribe(UIEvents.dataChanged, OnDataChanged);
    }

    string text;
    private void OnDataChanged(object obj)
    {

        text = obj as string;
        mVCHelper.CallEvent(UIEvents.display, text);
    }
}
