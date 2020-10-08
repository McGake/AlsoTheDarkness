using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueView : UIMVC
{
    public TextMeshProUGUI text;

    public void Update()
    {
        if (MultiInput.GetBButtonDown())
        {
            mVCHelper.CallEvent(UIEvents.backout, null);
        }
    }
    public override void MVCSetup(object obj)
    {
        //Debug.Log("setup called");
        mVCHelper.Subscribe(UIEvents.display, UpdateText);
    }

    private void UpdateText(object obj)
    {
        text.text = (string)obj;
    }

    public override void MVCEnd(object obj)
    {
        base.MVCEnd(obj);
        mVCHelper.Unsubscribe(UIEvents.display, UpdateText);
    }
}