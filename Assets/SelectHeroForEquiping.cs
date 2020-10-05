using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHeroForEquiping : MonoBehaviour, ISelectionBehavior
{
    public PC pc;

    public MVCHelper menuToOpen;
    public MVCHelper menuToClose;

    public void DoSelectionBehavior(object notUsed)
    {
        menuToClose.EndUI(null);
        menuToOpen.StartUI(pc);
        
    }
}
