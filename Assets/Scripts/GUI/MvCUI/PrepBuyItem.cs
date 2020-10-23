using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepBuyItem : MonoBehaviour, ISelectionBehavior
{
    public ShopItem shopItem;

    public MVCHelper menuToOpen;
    public MVCHelper menuToClose;

    public void DoSelectionBehavior(object notUsed)
    {
        menuToOpen.StartUI(shopItem);
        menuToClose.EndUI(null);
    }
}
