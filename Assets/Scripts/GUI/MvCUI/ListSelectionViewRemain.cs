using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListSelectionViewRemain : ListSelectionView
{
    public override void MVCStart(object obj)
    {
        base.MVCStart(obj);
        this.enabled = true;
    }

    public override void MVCEnd(object obj)
    {
        //cursor.SetActive(false);
        mVCHelper.Unsubscribe(UIEvents.dataChanged, SetButtons);
        this.enabled = false;
    }
}



