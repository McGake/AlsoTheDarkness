using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour, ISelectionBehavior
{
    public MVCHelper menuToOpen;

    public MVCHelper menuToClose;

    public void DoSelectionBehavior(object obj)
    {
        menuToClose.EndUI(null);
        menuToOpen.StartUI(obj);
    }

}

public interface ISelectionBehavior
{
    void DoSelectionBehavior(object obj);
}
