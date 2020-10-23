using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueViewOld : MonoBehaviour, IDialogueView
{


    public TextMeshProUGUI textMesh;

    public void OpenView()
    {
        gameObject.SetActive(true);
    }

    public void PopulateDialogueBox(string dialogue)
    {
        textMesh.text = dialogue;
    }

    public void CloseView()
    {
        gameObject.SetActive(false);
    }
}
