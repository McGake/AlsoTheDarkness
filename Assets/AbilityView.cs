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
    private float flashDuration = .2f;
    private float flashOff;
    private Color flashColor = Color.green;
    private int timesToFalsh = 5;
    private int timesFlashed = 0;
    private float flashOn;

    private float fillPercentage;
    private Image[] highlights;
    private bool useableWasFalse = true;

    private Color semiTranparent = new Color(1f, 1f, 1f, .3f);
    private Color opaque = new Color(1f, 1f, 1f, 1f);

    public void Update()
    {
        ongoingDisplayBehavior?.Invoke();
    }
    public void OnAbilityTurnsUsable()
    {
        StartFlashOnTrunsUsable();
    }

    public void BecomeTransparent()
    {
        Image[] imageComponents = GetComponentsInChildren<Image>();
        foreach(Image image in imageComponents)
        {
            if (!image.gameObject.name.Contains("Grey"))
            {
                Color tempColor;
                tempColor = image.color;
                tempColor = new Color(tempColor.r, tempColor.g, tempColor.b, semiTranparent.a);
                image.color = tempColor;
            }
        }
    }

    public void BecomeOpaque()
    {
        Image[] imageComponents = GetComponentsInChildren<Image>();
        foreach (Image image in imageComponents)
        {
            if (!image.gameObject.name.Contains("Grey"))
            {
                Color tempColor;
                tempColor = image.color;
                tempColor = new Color(tempColor.r, tempColor.g, tempColor.b, opaque.a);
                image.color = tempColor;
            }
        }
    }
    private void StartFlashOnTrunsUsable()
    {
        ongoingDisplayBehavior += Flash;
        flashOn = 0f;
        flashOff = float.PositiveInfinity;

        timesFlashed = 0;
    }
    private void Flash()
    {
        if(Time.time > flashOn)
        {
            timesFlashed++;
            flashOff = Time.time + flashSpeed;
            flashOn = float.PositiveInfinity;
            UsingHighlight.SetActive(false);
        }

        if(Time.time > flashOff)
        {
            flashOn = Time.time + flashSpeed;
            flashOff = float.PositiveInfinity;
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
            Color tempColor = highlights[i].color;
            tempColor = new Color(color.r, color.g, color.b, tempColor.a);
            highlights[i].color = tempColor;
        }
    }

    public void NewAbilitySet()
    {
        ongoingDisplayBehavior = null;
        useableWasFalse = false;
    }

    public void UpdateAbility(Ability ab)
    {
        if(ab.IsUseable() == false)
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
            if (ab.IsUseable() == false)
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
