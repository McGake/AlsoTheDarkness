using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDialogue : GameSegment
{
    private TownMenuManager tMM;
    public string dialogueText;
    private GameSegmentStateMachine tSM;
    public void Start()
    {
        tMM = GameObject.FindObjectOfType<TownMenuManager>();
        tSM = GameObject.FindObjectOfType<GameSegmentStateMachine>();
    }

    public override GameSegment StartSegment(GameObject stateHub)
    {
        tSM.SetCurrentGameSegment( this);
        DisplayDialogue();
        return (this);
    }

    public void DisplayDialogue()
    {
        tMM.DisplayDialoguePanel(dialogueText);
    }

    public override void UpdateGameSegment()
    {
        DialogueModeInput();
    }

    public void DialogueModeInput()
    {
        bool dialogueOngoing;
        if (Input.GetButtonDown("A"))
        {
            dialogueOngoing = tMM.AdvanceDialguePanel();
            if (dialogueOngoing == false)
            {
                EndGameSegment();
            }
        }
    }

    public void AdvanceDialogue()
    {

    }

    private void DialogueBehavior()
    {
        DialogueModeInput();
    }

    public override void EndGameSegment()
    {
        tSM.ReturnToDefaultInteraction();
    }
}
