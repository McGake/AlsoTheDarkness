using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DelayAndPlaySpecialAnim", menuName = "SubAbilities/Test/DelayAndPlaySpecialAnim", order = 1)]
public class DelayAndPlaySpecialAnim : SubAbility
{
    public AnimationClip testAnim;

    public float animPlayTime;

    private float endTime;

    public override void DoInitialSubAbility(Ability ab)
    {
        endTime = Time.time + animPlayTime;
        SetNewSpecialAnimation(testAnim, ab);
    }

    public override void DoSubAbility(Ability ab)
    {
        if(Time.time > endTime)
        {
            SetNewAnimation("stand", ab);
            EndSubAbility();
        }        
    }

    public override void OnSelectionFinished(List<GameObject> selectedObjects)
    {

    }


}
 
