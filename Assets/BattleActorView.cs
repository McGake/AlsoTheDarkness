﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Animations;

public class BattleActorView : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField]
    private GameObject popoutTextBoxPrefab;

    [SerializeField]
    private GameObject worldSpaceCanvas;

    [SerializeField]
    private Animator animator;


    [SerializeField]
    private Animator pcAnimator;

    [SerializeField]
    private List<Animator> oneTimeEffectAnimators;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private float stopBlinkTime;

    private float blinkDuration = 2f;

    private delegate void DelOngoingDisplayBehavior();
    private DelOngoingDisplayBehavior OngoingDisplaybehavior;//TODO: if needed, make this a list of behaviors

#pragma warning restore 649

    public void Start()
    {
        SetupBattleActorView();
        OngoingDisplaybehavior = Nothing;//TODO: this is a cludge
    }

    private void SetupBattleActorView()
    {
        worldSpaceCanvas = GameObject.Find("WorldSpaceUI");
    }

    public void Update()
    {
        OngoingDisplaybehavior();
    }

    public void ShowDamage(int damage)
    {
        GameObject explodingText = GameObject.Instantiate(popoutTextBoxPrefab, transform.position, Quaternion.identity, worldSpaceCanvas.transform);
        int damageAsInt = Mathf.RoundToInt(damage);
        explodingText.GetComponent<TextMeshProUGUI>().text = damageAsInt.ToString();
        Collider2D tempCollider = transform.GetChild(0).GetComponent<Collider2D>();

        explodingText.GetComponent<OnlyCollideWithOneThing>().SetThingToCollideWith(tempCollider);
        Vector2 force = new Vector2(Random.Range(-25f, 25f), 75f);
        force.Normalize();
        explodingText.GetComponent<Rigidbody2D>().AddForce(force * 250);
    }


    public void ShowStatus(Status statusToShow)
    {
        string statusName = statusToShow.GetType().ToString();
        animator.SetBool(statusName,true);
    }

    public void StopShowStatus(Status statusToRemove)
    {
        string statusName = statusToRemove.GetType().ToString();
        animator.SetBool(statusName, false);
    }

    public void ShowOneTimeEffect(AnimatorOverrideController oneTimeAnimatorController)
    {
        oneTimeEffectAnimators[0].runtimeAnimatorController = oneTimeAnimatorController;

        oneTimeEffectAnimators[0].Play("OneTimeEffect", 0, 0f);
        //oneTimeEffectAnimators[0].SetBool("OneTimeEffect", true);
        //make the game instantiate more oneTimeEffectAnimators if there are not enough here rather than interupting animations in progress

    }

    public void ShowOneTimeCast(AnimatorOverrideController oneTimeAnimatorController)
    {
        oneTimeEffectAnimators[0].runtimeAnimatorController = oneTimeAnimatorController;
       

        oneTimeEffectAnimators[0].Play("OneTimeCast", 0, 0f);
        //oneTimeEffectAnimators[0].SetBool("OneTimeEffect", true);
        //make the game instantiate more oneTimeEffectAnimators if there are not enough here rather than interupting animations in progress

    }

    public void PlayInteruptingCharacterAnimation(string stateName)
    {
        pcAnimator.Play("down", 0, 0f);
    }

    private float nextBlinkTime;
    private float blinkInterval = .18f;

    public void StartBlink()
    {
        nextBlinkTime = Time.time;
        stopBlinkTime = Time.time + blinkDuration;
        OngoingDisplaybehavior = Blink;
    }

    private void Blink()
    {
        if(Time.time >= nextBlinkTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            nextBlinkTime = Time.time + blinkInterval;
            
        }

        if(Time.time >= stopBlinkTime)
        {
            StopBlink();
        }
    }

    public void StopBlink()
    {
        spriteRenderer.enabled = true;
        OngoingDisplaybehavior = Nothing;
    }

    private void Nothing()//TODO: remove this cludge
    {

    }

}
