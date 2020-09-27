using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using System;

public class AdvanceTextController : UIMVC
{
    private int curLevelOfAdvancement;
    private List<string> text;

    public override void MVCStart(object obj)
    {
        base.MVCStart(obj);
        mVCHelper.Subscribe(UIEvents.dataChanged, OnDataChanged);
    }

    private void OnDataChanged(object obj)
    {
        text = obj as List<string>;
        mVCHelper.CallEvent(UIEvents.display, text[curLevelOfAdvancement]);
    }

    private void Update()
    {
        if(MultiInput.GetAButtonDown())
        {
            AdvanceDisplay();
        }
    }

    private void AdvanceDisplay()
    {
        curLevelOfAdvancement++;
        if(curLevelOfAdvancement >= text.Count)
        {
            mVCHelper.CallEvent(UIEvents.end, null);
        }
        else
        {
            mVCHelper.CallEvent(UIEvents.display, text[curLevelOfAdvancement]);
        }
    }
}
