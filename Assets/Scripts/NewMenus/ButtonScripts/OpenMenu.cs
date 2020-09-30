using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour, ISelectionBehavior
{
    public MVCHelper menuToOpen;

    public MVCHelper menuToClose;

    public void DoSelectionBehavior(object obj)
    {
        menuToOpen.StartUI(obj);
        menuToClose.EndUI(null);
    }

}

public interface ISelectionBehavior
{
    void DoSelectionBehavior(object obj);
}
