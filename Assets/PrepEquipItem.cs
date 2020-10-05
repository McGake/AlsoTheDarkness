using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepEquipItem : MonoBehaviour, ISelectionBehavior
{
    public Equipable equipable;

    public MVCHelper menuToUnpause;
    public MVCHelper menuToPause;

    public void DoSelectionBehavior(object notUsed)
    {
        menuToPause.Pause();
        menuToUnpause.Unpause(equipable);
    }

}