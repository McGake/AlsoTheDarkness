using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSellMenu : MonoBehaviour, ISelectionBehavior
{
    public string displayName => "Sell";

    public GameObject uiToOpen;

    public GameObject uiToClose;

    public void DoSelectionBehavior()
    {
        uiToClose.GetComponent<ISelectionController>().EndSelection();
        uiToOpen.SetActive(true);
    }

}
