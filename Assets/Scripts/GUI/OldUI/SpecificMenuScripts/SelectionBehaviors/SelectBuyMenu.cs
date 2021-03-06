﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuyMenu : MonoBehaviour, ISelectionBehaviorOLD
{
    public string displayName => "Buy";

    public GameObject uiToOpen;

    public GameObject uiToClose;

    public void DoSelectionBehavior()
    {
        uiToClose.GetComponent<SelectionController>().EndSelection();
        uiToOpen.SetActive(true);
    }

}
