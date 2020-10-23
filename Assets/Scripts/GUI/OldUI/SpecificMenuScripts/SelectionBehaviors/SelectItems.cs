using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItems : MonoBehaviour, ISelectionBehaviorOLD
{

    public GameObject uiToOpen;

    public GameObject selectionControllerToPause;
    public void DoSelectionBehavior()
    {
        uiToOpen.GetComponent<SelectionController>().StartSelection();
        selectionControllerToPause.GetComponent<SelectionController>().Pause();
    }
}
