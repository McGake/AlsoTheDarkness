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

    private float flashSpeed = .2f;
    private float flashDuration = 1f;
    private float flashEnd;
    private Color flashColor = Color.green;
    private int timesToFalsh = 2;
    private int timesFlashed = 0;
    private float nextFlash;

    private float fillPercentage;
    private Image[] highlights;
    private bool useableWasFalse = true;

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
    public void UpdateAbility(Ability ab)
    {
        if(ab.Useable == false)
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
        if (ab.AbilityOver == false)
        {
            radialProgress.fillAmount = 1f;
            greyMask.SetActive(true);
            SetHighlightColor(Color.yellow);
        }
        else
        {
            fillPercentage = (ab.CurCooldownEndTime - Time.time) / ab.cooldownTime;
            if (ab.CurCooldownEndTime <= Time.time)
            {
                
                fillPercentage = 1;
                
            }
            radialProgress.fillAmount = fillPercentage;
            if (ab.Useable == false)
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
