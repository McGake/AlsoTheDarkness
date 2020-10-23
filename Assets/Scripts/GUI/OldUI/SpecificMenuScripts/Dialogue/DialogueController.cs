using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour, IDialogueController
{

    public IDialogueView dialogueView;

    public IDialogueModel dialogueModel;

    private List<string> dialogue;

    private int curDialogueIndx;
    // Update is called once per frame
    void Update()
    {
        if(MultiInput.GetAButtonDown())
        {
            AdvanceDialogue();
        }
        if(MultiInput.GetBButtonDown())
        {
            EndDialogue();
        }
    }

    public void StartDialogue(List<string> tempDialogue, IDialogueModel dM)
    {
        dialogueView = GetComponent<IDialogueView>();
        dialogueView.OpenView();
        curDialogueIndx = 0;
        dialogue = tempDialogue;
        dialogueModel = dM;
        
        DisplayDialogue(dialogue[curDialogueIndx]);
    }

    public void AdvanceDialogue()
    {
        curDialogueIndx++;
        if (curDialogueIndx >= dialogue.Count)
        {
            EndDialogue();
        }
        else
        {
            DisplayDialogue(dialogue[curDialogueIndx]);
        }
    }

    public void DisplayDialogue(string toDisplay)
    {
        dialogueView.PopulateDialogueBox(toDisplay);
    }

    public void EndDialogue()
    {
        dialogueView.CloseView();
        dialogueModel.EndDialogueGameSegment();
    }


}
