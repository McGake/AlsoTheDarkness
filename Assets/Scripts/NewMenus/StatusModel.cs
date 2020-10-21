using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatusModel : UIMVC
{

    // Start is called before the first frame update

    PC pc;
    public override void MVCSetup(object obj)
    {
        Debug.Log("SETUP CALLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLED");
        pc = (PC)obj;
        Debug.Log("pc recived " + pc.battlePC.name);
    }
    public override void MVCStart(object obj)
    {
        Debug.Log("START");
        mVCHelper.CallEvent(UIEvents.dataChanged, pc);
    }
 
}
