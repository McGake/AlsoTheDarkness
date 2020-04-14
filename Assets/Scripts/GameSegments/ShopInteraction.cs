using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInteraction : GameSegment
{
    private TownMenuManager tMM;
    public List<ShopItem> itemsInShop;
    public GameObject ItemDisplayPrefab;

    public void Start()
    {
        tMM = GameObject.FindObjectOfType <TownMenuManager>();
    }

    public override GameSegment StartSegment(GameObject interactor)
    {
        Debug.Log("start segment");
        gameStateMachine.SetCurrentGameSegment(this);
        DisplayStore();
        return (this);
    }

    private void DisplayStore()
    {
        
        tMM.OpenStore(this);
    }

    public override void UpdateGameSegment()//This gets called on update from the state machien
    {
        if(Input.GetButtonDown("B"))
        {
            BackOutOfCurrentMenu();
        }
    }

    public void GenerateBuyingList()
    {
        tMM.GenerateBuyingList(itemsInShop);
    }

    public void GenerateSellingList()
    {

    }

    #region MethodsCalledFromOnClickOnButtons

    public void StartBuy()
    {
        Debug.Log("start buy called");
        tMM.CloseInitialPanel();
        tMM.OpenBuyPanel();
        GenerateBuyingList();
    }
    public void StartSell()
    {
        //Generate selling list once we have a player inventory
    }
    public void StartExit()
    {
        tMM.CloseDialoguePanel();
    }
    #endregion MethodsCalledFromOnClickOnButtons
    
    public void BackOutOfCurrentMenu()
    {
        tMM.BackOutOfCurrentTownMenu();
        
    }

    public override void EndGameSegment()
    {
        gameStateMachine.ReturnToDefaultInteraction();
    }
}

[System.Serializable]
public class shop
{
    public string shopName;
    public List<ShopItem> shopItems;
}

[System.Serializable]
public class Item
{
    public string listName;
    public Sprite icon;
    //this will be the parent class for all equipment and items in the game. place hoder for now.
}

[System.Serializable]
public class ShopItem
{
    public ItemDef itemDef;
    public Item item;
    public int price;
    public int numberInInventory;
}

