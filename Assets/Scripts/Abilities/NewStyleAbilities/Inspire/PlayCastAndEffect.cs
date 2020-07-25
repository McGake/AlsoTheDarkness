using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This requires that 
/// </summary>


[CreateAssetMenu(fileName = "PlayCastAndEffect", menuName = "SubAbilities/AddStatus/PlayCastAndEffect", order = 1)]
public class PlayCastAndEffect : SubAbility
{


#pragma warning disable 649

    [SerializeField]
    private AnimatorOverrideController animOverrideController;

    [SerializeField]
    private float overlapTime;

    [SerializeField]
    private float effectStartDelay;
#pragma warning restore 649
    private float endTime;

    private float effectStartTime;

    private bool animationTriggered = false;

    public override void DoInitialSubAbility(Ability ab)
    {



        animationTriggered = false;
        effectStartTime = Time.time + effectStartDelay;
        
            endTime = (effectStartDelay) + animOverrideController.animationClips[1].length + Time.time;
        
    }

    public override void DoSubAbility(Ability ab)
    {
        if (Time.time > endTime)
        {
            EndSubAbility();
        }
        else if(Time.time> effectStartDelay && animationTriggered == false)
        {
            animationTriggered = true;
            foreach (GameObject gO in ab.objectTargets)
            {
                gO.GetComponent<BattleActorView>().ShowOneTimeEffect(animOverrideController);
                //statusToAddInstance = Instantiate(statusToAdd);
                //gO.GetComponent<BaseBattleActor>().AddStatus(statusToAddInstance);
            }
        }
    }
}