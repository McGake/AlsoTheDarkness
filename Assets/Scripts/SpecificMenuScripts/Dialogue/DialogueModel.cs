using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueModel : GameSegment, IDialogueModel
{
    private TownMenuManager tMM;
    public List<string> dialogueText;
    private GameSegmentStateMachine tSM;
    public DialogueController dialogueController;

    public void Start()
    {
        tMM = GameObject.FindObjectOfType<TownMenuManager>();
        tSM = GameObject.FindObjectOfType<GameSegmentStateMachine>();
    }

    public override GameSegment StartSegment(GameObject stateHub)
    {
        TownMovement.inMenu = true;
        tSM.SetCurrentGameSegment( this);
        dialogueController.StartDialogue(dialogueText, this);
        return (this);
        
    }

    //public void DisplayDialogue()
    //{
    //    tMM.DisplayDialoguePanel(dialogueText);
    //}

    //public override void UpdateGameSegment()
    //{
    //    DialogueModeInput();
    //}

    //public void DialogueModeInput()
    //{
    //    bool dialogueOngoing;
    //    if (MultiInput.GetAButtonDown())
    //    {
    //        dialogueOngoing = tMM.AdvanceDialguePanel();
    //        if (dialogueOngoing == false)
    //        {
    //            EndGameSegment();
    //        }
    //    }
    //}

    //public void AdvanceDialogue()
    //{

    //}

    //private void DialogueBehavior()
    //{
    //    DialogueModeInput();
    //}

    public void EndDialogueGameSegment()
    {
        TownMovement.inMenu = false;
        tSM.ReturnToDefaultInteraction();
    }
}
