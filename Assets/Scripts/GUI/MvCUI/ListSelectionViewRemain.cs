using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListSelectionViewRemain : ListSelectionView
{
    public override void MVCEnd(object obj)
    {
        //cursor.SetActive(false);
        mVCHelper.Unsubscribe(UIEvents.dataChanged, SetButtons);
    }
}



