using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAddAnim : MonoBehaviour
{

    private AnimationClip[] allClips;

    public AnimatorOverrideController aOC;

    public AnimationClip testAnimationClip;

    public Animator animator;

    public void Start()
    {


    }

    public void Update()
    {
        if(Input.GetKeyDown("j"))
        {
            animator = GetComponent<Animator>();
            Debug.Log(animator.name);

            //aOC = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = aOC;
        }
        if(Input.GetKeyDown("k"))
        {
            Debug.Log("k key down");
            Debug.Log(aOC.name);
            Debug.Log(aOC["stand"].name + " clip name");
            aOC["attack"] = testAnimationClip;
            aOC["stand"] = testAnimationClip;
            aOC["walk"] = testAnimationClip;

            Debug.Log(aOC["stand"].name + " clip name");
        }
    }
}
