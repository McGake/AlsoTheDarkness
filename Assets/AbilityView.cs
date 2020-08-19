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
        ongoingDisplayBehavior += Flash;
        nextFlash = 0f;
        timesFlashed = 0;
    }
    private float flashSpeed;

    private float flashDuration;

    private float flashEnd;

    private Color flashColor = Color.green;

    private int timesToFalsh = 2;

    private int timesFlashed = 0;

    private float nextFlash;
    private void Flash()
    {
        if(Time.time > nextFlash)
        {
            timesFlashed++;
            flashEnd = Time.time + flashDuration;
            UsingHighlight.SetActive(false);
        }

        if(Time.time > flashEnd)
        {
            nextFlash = Time.time + flashSpeed;
            UsingHighlight.SetActive(true);

            if(timesFlashed >= timesToFalsh)
            {
                ongoingDisplayBehavior -= Flash;
            }
        }
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

    private bool useableWasFalse = true;
    public void UpdateAbility(Ability ab)
    {
        if(ab.useable == false)
        {
            SetHighlightColor(Color.red);
            greyMask.SetActive(true);
            useableWasFalse = true;
        }
        else
        {
            greyMask.SetActive(false);
            SetHighlightColor(Color.green);
            if(useableWasFalse)
            {
                useableWasFalse = false;
                OnAbilityTurnsUsable();//TODO: get rid of this mess and add an event system
            }
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

    public void FlashOnReady()
    {
        throw new NotImplementedException();
    }
}


public interface iAbilityView
{
    void SetButtonLabel(string label);

    void SetUsesLeft(string uses);

    void FlashOnReady();

    void UpdateAbility(Ability ab);

}
