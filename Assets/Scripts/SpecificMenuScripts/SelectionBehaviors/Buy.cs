using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buy : MonoBehaviour, ISelectionBehavior
{
    public string displayName => throw new System.NotImplementedException();

    public ShopItem thisItem;


    public void DoSelectionBehavior()
    {
        if (PartyManager.curParty.gp > thisItem.price)
        {
            PartyManager.curParty.gp -= thisItem.price;

            //A copy of the item should probably be made here.

            PartyManager.AddItemToCurrentParty(thisItem.itemDef.item);

            //Make item factor to create item for inventory
        }
        //Check if can afford item. if can subtract value from gold and add item to inventory.
    }


}
