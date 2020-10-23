using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentController : UIMVC
{
    public MVCHelper panelToBackoutWith;

    public override void MVCSetup(object obj)
    {
        panelToBackoutWith.Subscribe(UIEvents.backout, Exit);
    }

    private void Exit(object obj)
    {
        mVCHelper.CallEvent(UIEvents.end, null);
    }

}
