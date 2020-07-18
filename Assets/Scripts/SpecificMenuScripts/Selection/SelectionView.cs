using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionView : MonoBehaviour, ISelectionView
{
#pragma warning disable 649
    public List<GameObject> selections;

    [SerializeField]
    private GameObject cursor;

    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private Vector3 cursorOffset;

    [SerializeField]
    private GameObject layoutObject;
#pragma warning restore 649


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

    }

}
