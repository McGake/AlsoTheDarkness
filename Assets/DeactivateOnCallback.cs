using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnCallback : MonoBehaviour
{
    private Fireable fireable;
    public void OnEnable()
    {
        fireable = GetComponent<Fireable>();
        Debug.Log(fireable.sourceAbility.name);
        fireable.sourceAbility.SubscribeToProjectileCallback(DeactivateSelf);
    }

    private void DeactivateSelf()
    {
        fireable.sourceAbility.UnsubscribeToProjectileCallback(DeactivateSelf);
        gameObject.SetActive(false);
    }
}
