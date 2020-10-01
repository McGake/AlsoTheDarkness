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

    private int startCalled = 0;



    public override void MVCSetup(object obj)
    {
        base.MVCSetup(obj);
        startCalled++;
        shopItemList = (List<ShopItem>)obj;
        Debug.Log("shop items " + shopItemList.Count);
        Debug.Log("button List " + buttonList.Count);
        Debug.Log("settup called " + startCalled);
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


