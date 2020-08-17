using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityView : MonoBehaviour, iAbilityView
{

    public TextMeshProUGUI label;

    public TextMeshProUGUI uses;

    public Image radialProgress;
    public GameObject greyMask;

    public void FlashOnReady()
    {
        throw new System.NotImplementedException();
    }

    public void SetButtonLabel(string inLabel)
    {
        label.text = inLabel;
    }

    public void SetUsesLeft(string inUses)
    {
        uses.text = inUses;
    }

    private float fillPercentage;
    public void UpdateAbility(Ability ab)
    {
        fillPercentage = (ab.curCooldownEndTime - Time.time) / ab.cooldownTime;
        if (fillPercentage > 1)
        {
            fillPercentage = 1;
        }
        radialProgress.fillAmount = fillPercentage;
        if (ab.useable == false)
        {
            greyMask.SetActive(true);
        }
        else
        {
            greyMask.SetActive(false);
        }
        SetUsesLeft((ab.maxUses - ab.uses).ToString());
    }
}


public interface iAbilityView
{
    void SetButtonLabel(string label);

    void SetUsesLeft(string uses);

    void FlashOnReady();

    void UpdateAbility(Ability ab);

}
