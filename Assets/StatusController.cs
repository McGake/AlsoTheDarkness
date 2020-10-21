using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : UIMVC
{

    public MVCHelper returnPanel;

    public MVCHelper subPannelToClose;

    public override void MVCSetup(object obj)
    {
        base.MVCSetup(obj);
        mVCHelper.Subscribe(UIEvents.backout, BackoutOrExit);
        mVCHelper.Subscribe(UIEvents.execute, BackoutOrExit);
    }

    private void Select(object obj)
    {
        Debug.Log("selected hero controllerUUUU");

        GameObject selected = (GameObject)obj;
        Debug.Log(selected.name);
        selected.GetComponent<ISelectionBehavior>().DoSelectionBehavior(null);
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
        mVCHelper.Unsubscribe(UIEvents.backout, BackoutOrExit);
        mVCHelper.Unsubscribe(UIEvents.execute, Select);
    }

}