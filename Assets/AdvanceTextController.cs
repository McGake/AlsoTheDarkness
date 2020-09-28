﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using System;

public class AdvanceTextController : UIMVC
{
    private int curLevelOfAdvancement;
    private string[] text;

    public override void MVCSetup(object obj)
    {
        base.MVCSetup(obj);
        curLevelOfAdvancement = 0;
        mVCHelper.Subscribe(UIEvents.dataChanged, OnDataChanged);
    }

    private void OnDataChanged(object obj)
    {
      
        text = obj as string[];
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
        if(curLevelOfAdvancement >= text.Length)
        {
            mVCHelper.CallEvent(UIEvents.end, null);
        }
        else
        {
            mVCHelper.CallEvent(UIEvents.display, text[curLevelOfAdvancement]);
        }
    }

    public override void MVCEnd(object obj)
    {
        base.MVCEnd(obj);
        mVCHelper.Unsubscribe(UIEvents.dataChanged, OnDataChanged);
        Pauser.UnpauseGame();
    }
}
