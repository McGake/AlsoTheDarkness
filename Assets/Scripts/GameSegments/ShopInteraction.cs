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
public class Item
{
    public string listName;
    //public Sprite icon;
    public int sellPrice;
    public UseItemInOverworld useItem;

    public Item(Item itemToCopy)
    {
        listName = itemToCopy.listName;
       // icon = itemToCopy.icon;
        sellPrice = itemToCopy.sellPrice;
        useItem = itemToCopy.useItem;
    }

    public Item()
    {

    }
    //this will be the parent class for all equipment and items in the game. place hoder for now.
}

[System.Serializable]
public class ShopItem
{
    public ItemDef itemDef;
    public int price;
    public int numberInInventory;
}

public abstract class UseItemInOverworld : MonoBehaviour
{
    public virtual void UseItem()
    {

    }
}


public class TempHealingItem:UseItemInOverworld
{
    public float healing;

    public override void UseItem()
    {
        Debug.Log("start player selection screen here");
        //Start player selection screen
    }
}