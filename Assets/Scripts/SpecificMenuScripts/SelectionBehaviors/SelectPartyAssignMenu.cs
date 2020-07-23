using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPartyAssignMenu : MonoBehaviour, ISelectionBehavior
{
    public string displayName => "Buy";

    public SelectHeroToApplyModel uiToOpen;

    public GameObject uiToClose;

    public Item item;

    public void DoSelectionBehavior()
    {
        uiToClose.GetComponent<SelectionController>().EndSelection();
        uiToOpen.itemToUse = item;
        uiToOpen.gameObject.SetActive(true);
    }

}
