using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionView : MonoBehaviour
{
#pragma warning disable 649
    private List<GameObject> selections;

    private GameObject cursor;

    [SerializeField]
    private Vector3 cursorOffset;

    [SerializeField]
    private GameObject layoutObject;
#pragma warning restore 649

    public void Awake()
    {
        cursor = GameObject.Find("Pointer");
    }

    public void Start()
    {
        cursor = GameObject.Find("Pointer");
    }

    public void OpenView(List<GameObject> tempSelections)
    {
        cursor = GameObject.Find("Pointer");
        Debug.Log(gameObject + " " + "Found" + cursor.name);
        layoutObject.SetActive(true);
        gameObject.SetActive(true);
        selections = tempSelections;
        foreach(GameObject selection in selections)
        {
            selection.transform.SetParent(layoutObject.transform);
            selection.transform.localScale = new Vector3(1, 1, 1);
            selection.SetActive(true);
        }
        if (selections.Count > 0)
        {
            cursor.SetActive(true);
            MoveCursorToIndex(0);
        }
    }

    public void CloseView()
    {
        gameObject.SetActive(false);
        
        //MoveCursorToIndex(0);
    }

    public void LeaveMenus()
    {
        cursor.SetActive(false);
    }

    public void MoveCursorToIndex(int indx)
    {
        
        cursor.transform.position = selections[indx].transform.position + cursorOffset;
    }



    public void RepopulateSelections(List<GameObject> selections)
    { 

    }

}
