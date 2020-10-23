using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteraction : MonoBehaviour, Iinteractable
{

    public List<ShopItem> itemsForSale;

    public MenuStarter menuStarter;
    public void Interact()
    {
        menuStarter.StartMenu(itemsForSale,"Item");
    }

}

[System.Serializable]
public class ShopItem
{
    public Item item;
    public int price;
}


