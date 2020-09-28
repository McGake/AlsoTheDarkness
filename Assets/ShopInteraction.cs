using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteraction : MonoBehaviour, Iinteractable
{

    public List<ShopItem> itemsForSale;
    public void Interact()
    {
        
    }

}

public class ShopItem
{
    public Item item;
    public int price;
}


