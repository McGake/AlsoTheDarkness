using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConfirmController : UIMVC
{
    private ShopItem shopItemToConfirm;

    public MVCHelper returnPage;

    public ShopGoldModel shopGoldModel;

    public override void MVCSetup(object obj)
    {
        base.MVCSetup(obj);
        mVCHelper.Subscribe(UIEvents.dataChanged, SetItem);
        mVCHelper.Subscribe(UIEvents.execute, BuyItem);
        mVCHelper.Subscribe(UIEvents.backout, Backout);

    }

    private void SetItem(object obj)
    {
        shopItemToConfirm = (ShopItem)obj;
    }

    private void BuyItem(object obj)
    {
        PartyManager.curParty.gp -= shopItemToConfirm.price;
        PartyManager.curParty.items.Add(Instantiate(shopItemToConfirm.item));
        shopGoldModel.GoldValueChanged();
        mVCHelper.EndUI(obj);
        returnPage.Return();
        
    }

    private void Backout(object obj)
    {
        mVCHelper.CallEvent(UIEvents.end, null);
        if (returnPage != null)
        {
            returnPage.Return();
        }
    }

    public override void MVCEnd(object obj)
    {
        base.MVCEnd(obj);
        mVCHelper.Unsubscribe(UIEvents.dataChanged, SetItem);
        mVCHelper.Unsubscribe(UIEvents.execute, BuyItem);
        mVCHelper.Unsubscribe(UIEvents.backout, Backout);



    }
}
