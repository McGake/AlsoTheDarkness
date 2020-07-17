using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreModel : MonoBehaviour, ISelectionModel
{
    public List<GameObject> selections;

    public GameObject buttonTemplate;

    List<ShopItem> shopItems;




    public List<GameObject> GetSelections()
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

            ShopItem curShopItem = shopItems[i];

            selections[i].GetComponent<Buy>().thisItem = curShopItem;
            
            selections[i].GetComponentInChildren<TextMeshProUGUI>().text = curShopItem.itemDef.name + " " + curShopItem.price;
            
        }
    }

    public void SetShopItems(List<ShopItem> sI)
    {
        shopItems = sI;
    }

}
