using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStatus : MonoBehaviour, ISelectionBehaviorOLD
{

    public GameObject uiToOpen;

    public GameObject selectionControllerToPause;
    public void DoSelectionBehavior()
    {
        uiToOpen.GetComponent<MVCHelper>().StartUI(null);
        selectionControllerToPause.GetComponent<SelectionController>().Pause();
    }
}
