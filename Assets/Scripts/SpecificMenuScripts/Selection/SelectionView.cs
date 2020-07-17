using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionView : MonoBehaviour, ISelectionView
{
    
    public List<GameObject> selections;

    [SerializeField]
    private GameObject cursor;

    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private Vector3 cursorOffset;

    [SerializeField]
    private GameObject layoutObject;



    public void OpenView(List<GameObject> tempSelections)
    {
        layoutObject.SetActive(true);
        gameObject.SetActive(true);
        selections = tempSelections;
        foreach(GameObject selection in selections)
        {
            selection.transform.SetParent(layoutObject.transform);
            selection.transform.localScale = new Vector3(1, 1, 1);
            selection.SetActive(true);
        }

        cursor.SetActive(true);
        MoveCursorToIndex(0);
    }

    public void CloseView()
    {
        gameObject.SetActive(false);
        cursor.SetActive(false);
        //MoveCursorToIndex(0);
    }

    public void MoveCursorToIndex(int indx)
    {
        cursor.transform.position = selections[indx].transform.position + cursorOffset;
    }



    public void RepopulateSelections(List<GameObject> selections)
    {
        List<string> selectionsText = new List<string>();
        for (int i = 0; i <= selections.Count; i++)
        {
            selectionsText.Add(selections[i].GetComponent<ISelectionBehavior>().displayName);
        }
        int numberToCreate = 0;
        if(this.selections.Count < selections.Count)
        {
            numberToCreate = selections.Count - this.selections.Count;
        }

        for(int i = 0; i< this.selections.Count; i++)
        {
            this.selections[i].GetComponentInChildren<Text>().text = selectionsText[i];
        }

        for(int j = 0; j < numberToCreate; j++)
        {
            GameObject tempButton = Instantiate(buttonPrefab);
            tempButton.GetComponent<Text>().text = selectionsText[this.selections.Count + j];
            this.selections.Add(tempButton);
        }

        for(int x = 0; x < this.selections.Count; x++)
        {
            this.selections[x].transform.SetParent(layoutObject.transform);
        }

    //    int numberToDestroy= 0;
    //    if(selectableButtons.Count > selections.Count)
    //    {
    //        numberToDestroy = selectableButtons.Count - selections.Count;
    //    }

    //    for (int i = 0; i < numberToDestroy; i++)
    //    {
    //        selectableButtons.RemoveAt(selectableButtons.Count - 1);
    //    }
    }

}
