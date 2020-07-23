using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string listName;
    //public Sprite icon;
    public int sellPrice;
    public UseItemScript useScript;

    public Item(Item itemToCopy)
    {
        listName = itemToCopy.listName;
        // icon = itemToCopy.icon;
        sellPrice = itemToCopy.sellPrice;
        useScript = itemToCopy.useScript;
    }

    public Item()
    {

    }
    //this will be the parent class for all equipment and items in the game. place hoder for now.
}
