using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticSelectionView : MonoBehaviour
{
    public List<Button> selectableButtons;
    public Image cursor;


    public Vector2 cursorOffset;
    private int selectionIndx;

    public void OpenView()
    {
        gameObject.SetActive(true);
        cursor.gameObject.SetActive(true);
    }

    public void PopulateSelectableButtons(List<string> buttonTexts)
    {
        for(int i = 0; i<buttonTexts.Count; i++)
        {
            if(i >= selectableButtons.Count)
            {
                Debug.LogError("more texts than buttons on StaticSelectionView setup");
            }
            selectableButtons[i].GetComponent<Text>().text = buttonTexts[i];
        }
    }

    public void IncrementSelection (int incrment)
    {
        selectionIndx += incrment;
        if(selectionIndx < 0)
        {
            selectionIndx = selectableButtons.Count - 1;

        }
        else if(selectionIndx >= selectableButtons.Count)
        {
            selectionIndx = 0;
        }
    }

    public void ShowSelecitonOfIndx(int indx)
    {
        cursor.transform.position = selectableButtons[indx].transform.position + (Vector3)cursorOffset;
    }

    public void CloseView()
    {
        gameObject.SetActive(false);
    }
}
