using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class OverworldView : MonoBehaviour
{
    [SerializeField] private Animator overworldAnimator;
    // Start is called before the first frame update

    private string lastAnimSet = "walkingDown";

    const float velocityThreshold = .01f;

    private Vector2 direction;

    int curPartyMemberIndx = 0;

    private Vector2 leftDir = new Vector2(0f,180f);
    public void SetAnimDirection(Vector2 direction)
    {
        this.direction = direction;
        if (direction.x >= velocityThreshold)
        {
            PlayAnimation("walkingRight");
            transform.rotation = Quaternion.Euler(Vector2.zero);
            return;
        }
        else if (direction.x <= -velocityThreshold)
        {
            PlayAnimation("walkingLeft");
            transform.rotation = Quaternion.Euler(leftDir);
            return;
        }

        if (direction.y >= velocityThreshold)
        {
            PlayAnimation("walkingUp");
            transform.rotation = Quaternion.Euler(Vector2.zero);
            return;
        }
        else if (direction.y <= -velocityThreshold)
        {
            PlayAnimation("walkingDown");
            transform.rotation = Quaternion.Euler(Vector2.zero);
            return;
        }

        //PlayAnimation("stopped");
    }

    public void PauseAnimation()
    {
        overworldAnimator.speed = 0f;
    }

    public void PlayAnimation(string animToPlay)
    {
        //overworldAnimator.CrossFade(animToPlay, 0f);
        overworldAnimator.speed = 1f;
        overworldAnimator.SetBool(lastAnimSet, false);
        overworldAnimator.SetBool(animToPlay, true);
        lastAnimSet = animToPlay;
    }

    public void EndLastAnimation()
    {
        overworldAnimator.SetBool(lastAnimSet, false);
    }

    private void Update()
    {
      
      
    }
    public void SwitchDisplayCharacterUp(List<PC> partyMembers)
    {
        curPartyMemberIndx++;
        if (curPartyMemberIndx >= partyMembers.Count)
        {
            curPartyMemberIndx = 0;
        }
        if (curPartyMemberIndx < 0)
        {
            curPartyMemberIndx = partyMembers.Count - 1;
        }
        SetDisplayCharacter(curPartyMemberIndx,partyMembers);
    }

    public void SwitchDisplayChacterDown(List<PC> partyMembers)
    {
        curPartyMemberIndx--;
        if (curPartyMemberIndx >= partyMembers.Count)
        {
            curPartyMemberIndx = 0;
        }
        if (curPartyMemberIndx < 0)
        {
            curPartyMemberIndx = partyMembers.Count - 1;
        }
        SetDisplayCharacter(curPartyMemberIndx,partyMembers);
    }

    private void SetDisplayCharacter(int cPMI, List<PC> partyMembers)
    {
        if (partyMembers[cPMI].overworldAnimOverride == null)
        {
            Debug.LogError("no overworld animation override found. Check your characters prefabs to make sure one is added");
        }
        overworldAnimator.runtimeAnimatorController = partyMembers[cPMI].overworldAnimOverride;
        PlayAnimation(lastAnimSet);
    }

    AnimatorClipInfo[] animatorClipInfos;
    public void PlayAnimationAndCallbackWhenDone(string animationName, Action callback)
    {
        //PlayAnimation(animationName);
        overworldAnimator.speed = 1f;
        //EndLastAnimation();
        //overworldAnimator.Play(animationName);
        PlayAnimation("camp");
        
        Debug.Log("camp check " + overworldAnimator.GetCurrentAnimatorStateInfo(0).length + " " + overworldAnimator.GetCurrentAnimatorStateInfo(0).IsName("camp"));
        
        animatorClipInfos = overworldAnimator.GetCurrentAnimatorClipInfo(0);
        //InvokeRepeating("ShowCallback",.5f, .5f);
        StartCoroutine(Test());

       // float length = overworldAnimator.Get;
       
        

       
    }

    private IEnumerator Test()
    {
        yield return 0;
        float length = overworldAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        Invoke("EndTurn", length);
    }

    private void ShowCallback()
    {
        foreach(AnimatorClipInfo ac in overworldAnimator.GetCurrentAnimatorClipInfo(0))
        {
            Debug.Log(overworldAnimator.GetCurrentAnimatorClipInfo(0).Length);
            Debug.Log(ac.clip.name);
            Debug.Log(animatorClipInfos[0].clip.name + "WTF");
        }
    }

    private void EndTurn()
    {
        TurnManager.EndTurn(this);
    }

}
