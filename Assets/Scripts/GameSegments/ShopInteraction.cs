using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopInteraction : GameSegment
{
    private TownMenuManager tMM;
    public List<ShopItem> itemsInShop;
    public GameObject ItemDisplayPrefab;
    public SelectionController initialStoreMenu;
    public StoreModel storeModel;
    public GameObject topLevelStoreMenu;


    public void Start()
    {
        tMM = GameObject.FindObjectOfType <TownMenuManager>();
    }

    public override GameSegment StartSegment(GameObject interactor)
    {
        Debug.Log("start segment");
       // gameStateMachine.SetCurrentGameSegment(this);
        //DisplayStore();
        topLevelStoreMenu.SetActive(true); //figure out how to have this set when we call startselection (perhaps another controller for starting the overall store
        initialStoreMenu.gameObject.SetActive(true);
        storeModel.SetShopItems(itemsInShop);
        return (this);
    }

    //private void DisplayStore()
    //{
        
    //    tMM.OpenStore(this);
    //}

    //public override void UpdateGameSegment()//This gets called on update from the state machien
    //{
    //    if(Input.GetButtonDown("B"))
    //    {
    //        BackOutOfCurrentMenu();
    //    }
    //}

    //public void GenerateBuyingList()
    //{
    //    tMM.GenerateBuyingList(itemsInShop);
    //}

    //public void GenerateSellingList()
    //{

    //}

    //#region MethodsCalledFromOnClickOnButtons

    //public void StartBuy()
    //{
    //    Debug.Log("start buy called");
    //    tMM.CloseInitialPanel();
    //    tMM.OpenBuyPanel();
    //    GenerateBuyingList();
    //}
    //public void StartSell()
    //{
    //    //Generate selling list once we have a player inventory
    //}
    //public void StartExit()
    //{
    //    tMM.CloseDialoguePanel();
    //}
    //#endregion MethodsCalledFromOnClickOnButtons
    
    //public void BackOutOfCurrentMenu()
    //{
    //    tMM.BackOutOfCurrentTownMenu();
        
    //}

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
public class ShopItem
{
    public ItemDef itemDef;
    public int price;
    public int numberInInventory;
}




