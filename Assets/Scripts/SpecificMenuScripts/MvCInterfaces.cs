using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MvCInterfaces : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public interface IDialogueView
{
    void OpenView();

    void PopulateDialogueBox(string dialogue);

    void CloseView();
}

public interface IDialogueController
{
    void StartDialogue(List<string> dialogue, IDialogueModel iD);

    void DisplayDialogue(string dialogue);

    void AdvanceDialogue();

    void EndDialogue();
}

public interface IDialogueModel
{
    void EndDialogueGameSegment();
}

public interface ISelectionView
{
    void OpenView(List <GameObject> selections);

    void RepopulateSelections(List <GameObject> selectionsText);

    void MoveCursorToIndex(int indx);

    void CloseView();
}

public interface ISelectionController
{
    void StartSelection();

    void RepopulateSelections();

    void ChangeSelection(int indxChange);

    void EndSelection();

    void Select(int indx);
}

public interface ISelectionModel
{
    List<GameObject> GetSelections();
}


public interface ISelectionBehavior
{
    void DoSelectionBehavior();
}


public interface IGoldModel
{
    int GetCurrentGold();
}

public interface IGoldController
{
    void SetGoldText(int gold);
}

public interface IGoldView
{
    void SetGoldText(int gold);    
}