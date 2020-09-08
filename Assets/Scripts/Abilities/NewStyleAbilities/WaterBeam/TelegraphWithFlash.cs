using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TelegraphWithFlash", menuName = "SubProjectileAbility/Visual/Telegraph", order = 1)]
public class TelegraphWithFlash : SubProjectileAbility
{
    public float delay;

    public int numberOfFlashes;

    public float intervalBetweenFlashes;

    private float delayOverTime;

    public override void DoInitialProjectileSubAbility(ProjectileAbility pa)
    {
        pa.ability.BattleActorView.StartFlash(numberOfFlashes, intervalBetweenFlashes);
        delayOverTime = Time.time + delay;
    }

    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {


        if (delayOverTime <= Time.time)
        {
            EndProjectileSubAbility();
        }
    }
}
