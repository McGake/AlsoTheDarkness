using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHeroForEquiping : MonoBehaviour, ISelectionBehavior
{
    public PC pc;
    [HideInInspector]
    public MVCHelper menuToOpen;
    [HideInInspector]
    public MVCHelper menuToClose;

    public void DoSelectionBehavior(object notUsed)
    {
        menuToClose.EndUI(null);
        Debug.Log("PC SENT " + pc.battlePC.name);
        menuToOpen.StartUI(pc);
        
    }
}
