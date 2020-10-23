using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneralSelectionModel : MonoBehaviour
{
    public List<GameObject> selections;

    public GameObject buttonTemplate;

    public virtual List<GameObject> GetSelections()
    {
        return selections;
    }

}
