using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OverworldBattleView : MonoBehaviour
{
    public Animator overworldAnimator;

    public TextMeshPro tMP;

    private string lastAnimSet = "";

    TextMeshPro sizeTM;

    TextMeshPro damageRatingTM;

    TextMeshPro defencesTM;

    private void PlayAnimation(string animToPlay)
    {
        //overworldAnimator.CrossFade(animToPlay, 0f);
        //overworldAnimator.speed = 1f;
        overworldAnimator.SetBool(lastAnimSet, false);
        overworldAnimator.SetBool(animToPlay, true);
        lastAnimSet = animToPlay;
    }

    public void PlayOngoingBattleAnim()
    {
        PlayAnimation("UnderAttack");
    }

    private void EndLastAnimation()
    {
        overworldAnimator.SetBool(lastAnimSet, false);
    }

    public void StopOngoingBattleAnim()
    {
        overworldAnimator.SetBool("UnderAttack", false);
    }

    public void UpdateStrengthDisplay(BattleArmy bA)
    {
        sizeTM.text = bA.size.ToString();
        damageRatingTM.text = bA.dangerRating.ToString();
        defencesTM.text = bA.defences.ToString();
    }

    Action callback;
    public void PlayAttackAndCallbackWhenDone(Action callback , int damage)
    {
        //Get direction of attack and put the attack anim there
        tMP.text = damage.ToString();
        tMP.gameObject.SetActive(true);
        PlayAnimation("TurnAttack");
        StartCoroutine("DelayAndCallback");
        this.callback = callback;
    }

    public IEnumerator DelayAndCallback()
    {
        yield return 0;
        float length;
        length = overworldAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        Invoke("Callback", length);
    }

    private void Callback()
    {
        tMP.gameObject.SetActive(false);
        callback();
    }

}
