using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWithEquipmentType : MonoBehaviour, ISelectionBehavior
{

    public MVCHelper panelToOpen;

    public MVCHelper panelToPause;

    public System.Type slotType;
    public void DoSelectionBehavior(object obj)
    {
        panelToPause.Pause();
        panelToOpen.Unpause(slotType);

    }

}
