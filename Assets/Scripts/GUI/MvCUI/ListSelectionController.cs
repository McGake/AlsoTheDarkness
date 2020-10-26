using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSelectionController : UIMVC
{
    int curSelection = 0;

    List<GameObject> selectionList;

    object selectionInfo;

    public MVCHelper goldPanel;
    public MVCHelper shopTypePanel;
    public MVCHelper messagePanel;

    public MVCHelper returnPanel;

    public override void MVCSetup(object obj)
    {
        Pauser.PauseGame();
        base.MVCSetup(obj);
        selectionInfo = obj;
        mVCHelper.Subscribe(UIEvents.backout, BackoutOrExit);
        mVCHelper.Subscribe(UIEvents.execute, Select);
    }

    public override void MVCStart(object obj)
    {
        base.MVCStart(obj);
    }

    protected void Select(object obj)
    {
        GameObject selected = (GameObject)obj;
        selected.GetComponent<ISelectionBehavior>().DoSelectionBehavior(selectionInfo);
    }

    protected void BackoutOrExit(object obj) //TODO: make a seperate exit monobehavior maybe or at least make the end panels a public MVCHelper list
    {

        goldPanel?.CallEvent(UIEvents.end, null);
        shopTypePanel?.CallEvent(UIEvents.end, null);
        messagePanel?.CallEvent(UIEvents.end, null);
        mVCHelper.CallEvent(UIEvents.end, null);

        if (returnPanel != null)
        {
            returnPanel.Return();
        }
        else
        {
            Pauser.UnpauseGame();
        }
    }

    public override void MVCEnd(object obj)
    {
        base.MVCEnd(obj);
        mVCHelper.Unsubscribe(UIEvents.backout, BackoutOrExit);
        mVCHelper.Unsubscribe(UIEvents.execute, Select);
    }
}
