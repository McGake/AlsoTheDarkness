using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionModel : MonoBehaviour, ISelectionModel
{
    public List<GameObject> selections;
    public void DoSelection(int indx)
    {
        throw new System.NotImplementedException();
    }

    public List<GameObject> GetSelections()
    {
        return selections;
    }

}
