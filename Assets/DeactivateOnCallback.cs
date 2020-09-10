using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnCallback : FireableBehavior
{
    public override void OnFire()
    {
        fireable.sourceAbility.SubscribeToProjectileCallback(DeactivateSelf);
    }
    private void DeactivateSelf()
    {
        fireable.sourceAbility.UnsubscribeToProjectileCallback(DeactivateSelf);
        gameObject.SetActive(false);
    }
}
