using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayOneTimeEffectOnMultiple", menuName = "SubAbilities/AddStatus/PlayOneTimeEffectOnMultiple", order = 1)]
public class PlayOneTimeEffectAnimationOnMultiple : SubAbility
{


#pragma warning disable 649

    [SerializeField]
    private AnimatorOverrideController animOverrideController;
#pragma warning restore 649
    private float endTime;


    public override void DoInitialSubAbility(Ability ab)
    {
        foreach (GameObject gO in ab.objectTargets)
        {
            gO.GetComponent<BattleActorView>().ShowOneTimeEffect(animOverrideController);
            //statusToAddInstance = Instantiate(statusToAdd);
            //gO.GetComponent<BaseBattleActor>().AddStatus(statusToAddInstance);
        }
        Debug.Log(animOverrideController.animationClips[0].name + " Info!!!");
        endTime = animOverrideController.animationClips[0].length + Time.time;
        
    }

    public override void DoSubAbility(Ability ab)
    {
        if(Time.time > endTime)
        {
            EndSubAbility();
        }
    }
}

