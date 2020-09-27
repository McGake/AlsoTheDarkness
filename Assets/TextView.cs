using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextView : UIMVC
{
    public TextMeshProUGUI text;

    public override void MVCStart(object obj)
    {
        base.MVCStart(obj);
        mVCHelper.Subscribe(UIEvents.display, UpdateText);
    }

    private void UpdateText(object obj)
    {
        text.text = (string)obj;
    }
}
