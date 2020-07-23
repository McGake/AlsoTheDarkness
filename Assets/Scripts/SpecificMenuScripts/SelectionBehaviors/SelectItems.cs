using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItems : MonoBehaviour, ISelectionBehavior
{

    public GameObject uiToOpen;

    public GameObject selectionControllerToPause;
    public void DoSelectionBehavior()
    {
        uiToOpen.SetActive(true);
        selectionControllerToPause.GetComponent<SelectionController>().Pause();
    }
}
