using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldView : MonoBehaviour, IGoldView
{
    public TextMeshProUGUI textMesh;
    public void SetGoldText(int gold)
    {
        textMesh.text = gold.ToString();
    }

}
