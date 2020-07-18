using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell : MonoBehaviour, ISelectionBehavior
{

    public Item sellItem;

    public ISelectionController selectionBehavior;

    public void Start()
    {
        selectionBehavior = gameObject.GetComponentInParent<ISelectionController>();

    }

    public void DoSelectionBehavior()
    {
        PartyManager.curParty.gp += sellItem.sellPrice;

        PartyManager.curParty.items.Remove(sellItem);

        selectionBehavior.RepopulateSelections();
    }
}
