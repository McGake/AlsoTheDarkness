using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityView : MonoBehaviour, iAbilityView
{

    public TextMeshProUGUI label;

    public TextMeshProUGUI uses;
    public void SetButtonLabel(string inLabel)
    {
        label.text = inLabel;
    }

    public void SetUsesLeft(string inUses)
    {
        uses.text = inUses;
    }
}


public interface iAbilityView
{
    void SetButtonLabel(string label);

    void SetUsesLeft(string uses);
}
