using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownStateMachine : MonoBehaviour
{
    private Iinteractable curInteraction;

    private Iinteractable defaultInteraction;

    void Update()
    {
        curInteraction.InteractionUpdate(); //this is the state machien basically     
    }

    public void ReturnToDefaultInteraction()
    {
        Debug.Log("return called" + defaultInteraction);
        curInteraction = defaultInteraction;
    }

    public void SetDefualtInteraction(Iinteractable newDefaultInteraction)
    {
        defaultInteraction = newDefaultInteraction;
        curInteraction = defaultInteraction; // there might be a more resonable time to set this at the start of entering a town but after setdefualtinteraction has been called.
    }

    public void SetCurrentInteraction(Iinteractable newCurrentInteraction)//these set interactions look to similar in code, easy to mix up when on pain killers. Change names?
    {
        curInteraction = newCurrentInteraction;
    }

}

public interface Iinteractable
{
    void InteractionUpdate();
}
