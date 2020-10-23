using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGoldModel : UIMVC
{
    public override void MVCStart(object obj)
    {
        base.MVCStart(obj);
        mVCHelper.CallEvent(UIEvents.dataChanged, PartyManager.curParty.gp.ToString() + "GP");
    }

    public void GoldValueChanged()
    {
        mVCHelper.CallEvent(UIEvents.dataChanged, PartyManager.curParty.gp.ToString() + "GP");
    }

}
