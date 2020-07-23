using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell : MonoBehaviour, ISelectionBehavior
{

    public Item sellItem;

    private SelectionController selectionController;

    public void Start()
    {
        selectionController = gameObject.GetComponentInParent<SelectionController>();

    }

    public void DoSelectionBehavior()
    {
        PartyManager.curParty.gp += sellItem.sellPrice;

        PartyManager.curParty.items.Remove(sellItem);

        selectionController.RepopulateSelections();
    }
}
