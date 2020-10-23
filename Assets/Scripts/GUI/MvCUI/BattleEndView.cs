using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleEndView : UIMVC
{

    private List<string> messages;

    public TextMeshProUGUI textMesh;

    private int curMessageIndx;
    public override void MVCSetup(object obj)
    {
        curMessageIndx = 0;
        mVCHelper.Subscribe(UIEvents.dataChanged, ChangeMessages);
    }

    void ChangeMessages(object obj)
    {
        messages = (List<string>)obj;
        UpdateDisplay();
        
    }

    void UpdateDisplay()
    {
        if(curMessageIndx >= messages.Count)
        {
            mVCHelper.CallEvent(UIEvents.end, null);
        }
        else
        {
            textMesh.text = messages[curMessageIndx];
            curMessageIndx++;
        }
    }

    private void Update()
    {
        if(MultiInput.GetAButtonDown())
        {
            UpdateDisplay();
        }
        if(MultiInput.GetBButtonDown())
        {
            mVCHelper.CallEvent(UIEvents.backout, null);
        }
    }



}
