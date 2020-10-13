using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextView : UIMVC
{
    public TextMeshProUGUI text;


    public override void MVCSetup(object obj)
    {
        mVCHelper.Subscribe(UIEvents.dataChanged, UpdateText);
    }

    private void UpdateText(object obj)
    {
        text.text = (string)obj;
    }

    public override void MVCEnd(object obj)
    {
        base.MVCEnd(obj);
        mVCHelper.Unsubscribe(UIEvents.dataChanged, UpdateText);
    }
}
