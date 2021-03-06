﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreModel : GeneralSelectionModel
{

    List<ShopItemOld> shopItems;

    public override List<GameObject> GetSelections()
    {
        CreateButtonsForItems();
        return selections;
    }

    public void CreateButtonsForItems()
    {
        for(int i =0; i < shopItems.Count; i++)
        {
            if(selections.Count <= i)
            {
                selections.Add(GameObject.Instantiate(buttonTemplate));
            }

            ShopItemOld curShopItem = shopItems[i];

            selections[i].GetComponent<Buy>().thisItem = curShopItem;
            
            selections[i].GetComponentInChildren<TextMeshProUGUI>().text = curShopItem.displayName + " " + curShopItem.price;
            
        }
    }

    public void SetShopItems(List<ShopItemOld> sI)
    {
        shopItems = sI;
    }

}
