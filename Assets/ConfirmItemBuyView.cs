using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfirmItemBuyView : UIMVC
{
    ShopItem shopItemToConfirm;

    public TextMeshProUGUI costText;

    public TextMeshProUGUI nameText;

    public TextMeshProUGUI descriptionText;

    public override void MVCSetup(object obj)
    {
        base.MVCSetup(obj);
        mVCHelper.Subscribe(UIEvents.dataChanged, SetItem);
        
    }


    private void PopulateView()
    {
        costText.text = shopItemToConfirm.price.ToString();
        nameText.text = shopItemToConfirm.item.listName;
        descriptionText.text = shopItemToConfirm.item.description;
    }

    private void SetItem(object obj)
    {
        shopItemToConfirm = (ShopItem)obj;
        PopulateView();
    }


    private void Update()
    {
        if(MultiInput.GetAButtonDown())
        {
            mVCHelper.CallEvent(UIEvents.execute, shopItemToConfirm);
        }
    }

    public override void MVCEnd(object obj)
    {
        base.MVCEnd(obj);
        mVCHelper.Unsubscribe(UIEvents.dataChanged, SetItem);

    }
}
