using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : UIMVC
{

    public float displayTime = 3f;
    private float endTime = 0f;

    public override void MVCStart(object obj)
    {
        base.MVCStart(obj);
        endTime = Time.time + displayTime;
    }

    private void Update()
    {
        if(endTime< Time.time)
        {
            mVCHelper.CallEvent(UIEvents.end, null);
        }
    }

}
