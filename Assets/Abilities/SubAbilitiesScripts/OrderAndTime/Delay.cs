using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Delay", menuName = "SubAbility/Timing/Delay", order = 1)]
public class Delay : SubAbility
{
    public float delayTime;
    private float endTime;
    public override void DoInitialSubAbility(Ability ab)
    {
        endTime = Time.time + delayTime;
    }
    public override void DoSubAbility(Ability ab)
    {
        if(Time.time > endTime)
        {
            EndSubAbility();
        }
    }

}
