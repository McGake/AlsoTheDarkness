﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item:ScriptableObject
{
    public string listName;
    public string description;
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

public class Equipable:Item
{
    public List<Status> statusesOnEquip;

    [HideInInspector]
    public List<Status> statusesToUnequip;
}





public enum WeaponType
{
    sword,
    spear,
    wand,
}

public enum ArmorType
{
    robe,
    leather,
    plate,
}

public enum HelmetType
{
    cap,
    leather,
    metal,
}
