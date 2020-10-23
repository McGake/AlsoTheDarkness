using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectSlotController : UIMVC
{

    public MVCHelper subPannelToClose;

    public MVCHelper pausePanel;

    public MonoBehaviour viewToPause;

    public MVCHelper returnPanel;

    private PC pcToEquip;
    public override void MVCSetup(object obj)
    {
        base.MVCSetup(obj);
        mVCHelper.Subscribe(UIEvents.backout, BackoutOrExit);
        mVCHelper.Subscribe(UIEvents.execute, Select);
        mVCHelper.Subscribe(UIEvents.pause, Pause);
        mVCHelper.Subscribe(UIEvents.unpause, Unpause);

    }

    private void Pause(object obj)
    {
        viewToPause.GetComponent<UIMVC>().enabled = false;
    }

    private void Unpause(object obj)
    {
        viewToPause.GetComponent<UIMVC>().enabled = true;
    
    }

    private void Select(object obj)
    {
        GameObject selected = (GameObject)obj;
        selected.GetComponent<ISelectionBehavior>().DoSelectionBehavior(pcToEquip);
    }

    private void BackoutOrExit(object obj) //TODO: make a seperate exit monobehavior maybe or at least make the end panels a public MVCHelper list
    {

        mVCHelper.CallEvent(UIEvents.end, null);
        subPannelToClose.CallEvent(UIEvents.end, null);
        //pausePanel.CallEvent(UIEvents.pause, null);
        if (returnPanel != null)
        {
            returnPanel.Return();
        }
        //else
        //{
        //    //Pauser.UnpauseGame();
        //}
    }

    public override void MVCEnd(object obj)
    {
        base.MVCEnd(obj);
        mVCHelper.Unsubscribe(UIEvents.pause, Pause);
        mVCHelper.Unsubscribe(UIEvents.unpause, Unpause);
        mVCHelper.Unsubscribe(UIEvents.backout, BackoutOrExit);
        mVCHelper.Unsubscribe(UIEvents.execute, Select);
    }
}