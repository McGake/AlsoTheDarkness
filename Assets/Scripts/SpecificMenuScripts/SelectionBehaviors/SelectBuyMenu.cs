using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuyMenu : MonoBehaviour, ISelectionBehavior
{
    public string displayName => "Buy";

    public GameObject uiToOpen;

    public GameObject uiToClose;

    public void DoSelectionBehavior()
    {
        uiToClose.GetComponent<ISelectionController>().EndSelection();
        uiToOpen.SetActive(true);
    }

}
