using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDef", menuName = "ScriptableObjects/ItemDef", order = 1)]
public class ItemDef : ScriptableObject
{
    public Item item;

    public void Test()
    {
        item.useScript.DoSelectionBehavior();
    }

}
