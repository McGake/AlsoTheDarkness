using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCallback : MonoBehaviour
{
    private Fireable fireable;
    public void OnEnable()
    {
        fireable = GetComponent<Fireable>();
        fireable.sourceAbility.SubscribeToProjectileCallback(DestroySelf);
    }

    private void DestroySelf()
    {
        fireable.sourceAbility.UnsubscribeToProjectileCallback(DestroySelf);
        Destroy(gameObject);
    }
}
