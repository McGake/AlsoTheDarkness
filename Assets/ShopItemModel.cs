using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItemModel : UIMVC
{
    public GameObject shopButton;

    private List<GameObject> buttonList = new List<GameObject>();
    private List<ShopItem> shopItemList = new List<ShopItem>();

    public MVCHelper menuToOpen;
    public MVCHelper menuToClose;

    public override void MVCSetup(object obj)
    {
        base.MVCSetup(obj);
        if(obj is List<ShopItem> == false)
        {
            Debug.Log("its not even a shop item");
        }

        shopItemList = (List<ShopItem>)obj;
        CreateButtons();

    }

    public override void MVCStart(object obj)
    {
        base.MVCStart(obj);
        mVCHelper.CallEvent(UIEvents.dataChanged, buttonList);
    }


    private void CreateButtons()
    {
        for(int i = 0; i < shopItemList.Count; i++)
        {
            Debug.Log("order " + shopItemList[i].price + " index: " + i);
            GameObject tempButton;
            tempButton = BattlePooler.ProduceObject(shopButton);
            TextMeshProUGUI nameDisplay = tempButton.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            nameDisplay.text = shopItemList[i].item.listName;

            TextMeshProUGUI priceDisplay = tempButton.transform.Find("Price").GetComponent<TextMeshProUGUI>();
            priceDisplay.text = shopItemList[i].price.ToString();
            tempButton.GetComponent<PrepBuyItem>().menuToOpen = menuToOpen;
            tempButton.GetComponent<PrepBuyItem>().menuToClose = menuToClose;
            tempButton.GetComponent<PrepBuyItem>().shopItem = shopItemList[i];
            buttonList.Add(tempButton);
        }
    }

    public override void MVCEnd(object obj)
    {
        base.MVCEnd(obj);
        Debug.Log("1313131 cleared mode 1313131");
        foreach(GameObject button in buttonList)
        {
            button.SetActive(false);
        }
        buttonList.Clear();
    }
}


