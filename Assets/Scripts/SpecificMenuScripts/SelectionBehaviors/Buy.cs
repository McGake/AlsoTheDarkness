using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buy : MonoBehaviour, ISelectionBehaviorOLD
{

    public ShopItemOld thisItem;


    public void DoSelectionBehavior()
    {
        if (PartyManager.curParty.gp > thisItem.price)
        {
            PartyManager.curParty.gp -= thisItem.price;

            //A copy of the item should probably be made here.

            Item tempItem = new Item(thisItem.item);

            tempItem.sellPrice = thisItem.price / 2;

            PartyManager.AddItemToCurrentParty(tempItem);

            //Make item factor to create item for inventory
        }
        //Check if can afford item. if can subtract value from gold and add item to inventory.
    }


}
