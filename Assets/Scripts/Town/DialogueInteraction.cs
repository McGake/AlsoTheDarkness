using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour, Iinteractable
{

    public string[] dialogue;

    public MVCHelper dialogueWindow;



    public void Interact()
    {
        dialogueWindow.StartUI(dialogue);
    }

    public void InteractionUpdate()
    {
        throw new System.NotImplementedException();
    }
}
