using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class AbilityView : MonoBehaviour, iAbilityView
{

    public TextMeshProUGUI label;

    public TextMeshProUGUI uses;

    public Image radialProgress;

    public GameObject UsingHighlight;
    public GameObject greyMask;

    private Action ongoingDisplayBehavior;

    public void Update()
    {
        ongoingDisplayBehavior?.Invoke();
    }


    public void OnAbilityTurnsUsable()
    {
        StartFlashOnTrunsUsable();
    }
    private void StartFlashOnTrunsUsable()
    {
        
    }

    public void SetButtonLabel(string inLabel)
    {
        label.text = inLabel;
    }

    public void SetUsesLeft(string inUses)
    {
        uses.text = inUses;
    }


    private void SetHighlightColor (Color color)
    {
        highlights = UsingHighlight.GetComponentsInChildren<Image>();
        for (int i = 0; i < highlights.Length; i++)
        {
            highlights[i].color = color;
        }
    }
    private float fillPercentage;

    private Image[] highlights;
    public void UpdateAbility(Ability ab)
    {
        if(ab.useable == false)
        {
            SetHighlightColor(Color.red);
            greyMask.SetActive(true);
        }
        else
        {
            greyMask.SetActive(false);
            SetHighlightColor(Color.green);
        }


        if (ab.abilityOver == false)
        {
            radialProgress.fillAmount = 1f;
            greyMask.SetActive(true);
            SetHighlightColor(Color.yellow);
        }
        else
        {
            fillPercentage = (ab.curCooldownEndTime - Time.time) / ab.cooldownTime;
            if (ab.curCooldownEndTime <= Time.time)
            {
                
                fillPercentage = 1;
                
            }
            radialProgress.fillAmount = fillPercentage;
            if (ab.useable == false)
            {
                
            }

        }
        SetUsesLeft((ab.maxUses - ab.uses).ToString()); //TODO: make this calculation happen on ability used so that we dont have to do this every frame
    }
}


public interface iAbilityView
{
    void SetButtonLabel(string label);

    void SetUsesLeft(string uses);

    void FlashOnReady();

    void UpdateAbility(Ability ab);

}
