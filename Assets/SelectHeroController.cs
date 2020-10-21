using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHeroController : UIMVC
{

    public MVCHelper returnPanel;

    public GameObject viewToPause;

    public GameObject tempOldUIToReturnTo;
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
        Debug.Log("selected hero controllerUUUU");
       
        GameObject selected = (GameObject)obj;
        Debug.Log(selected.name);
        selected.GetComponent<ISelectionBehavior>().DoSelectionBehavior(null);
    }

    private void BackoutOrExit(object obj) //TODO: make a seperate exit monobehavior maybe or at least make the end panels a public MVCHelper list
    {

        mVCHelper.CallEvent(UIEvents.end, null);

        if (tempOldUIToReturnTo != null)
        {
            tempOldUIToReturnTo.GetComponent<SelectionController>().StartSelection();
        }
        else
        {

            //Pauser.UnpauseGame();
        }
    }

    public override void MVCEnd(object obj)
    {
        base.MVCEnd(obj);
        mVCHelper.Unsubscribe(UIEvents.backout, BackoutOrExit);
        mVCHelper.Unsubscribe(UIEvents.execute, Select);
        mVCHelper.Unsubscribe(UIEvents.pause, Pause);
        mVCHelper.Unsubscribe(UIEvents.unpause, Unpause);
    }
}
