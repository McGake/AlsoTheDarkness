using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public Item CreateItem(ShopItem shopItem)
    {
        Item createdItem = new Item();
        return createdItem;
    }

    public Item CreateItem(Item itemDef)
    {
        Item placeholder = new Item();
        return placeholder;
    }

    public List<Item> CreateItems(List<ShopItem> itemsToCreate)
    {
        List<Item> createdItems = new List<Item>();
        foreach (ShopItem sI in itemsToCreate)
        {
            Item newItem = CreateItem(sI);
            createdItems.Add(newItem);
        }

        
        return createdItems;
    }

}
