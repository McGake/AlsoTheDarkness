using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Fireable))]
public class TurnOnSecondaryOnContact : MonoBehaviour
{

    public LayerMask triggerOnEnterMask;

    public GameObject objectToTurnOn;

    public bool turnOffSelfOnContact = true;

    private Fireable fireable;

    public void Awake()
    {
        fireable = GetComponent<Fireable>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (((triggerOnEnterMask >> col.gameObject.layer)) == 1)
        {
            Debug.Log(fireable);
            Debug.Log(fireable.sourceAbility);

            ProjectileFactory.ProduceProjectile(objectToTurnOn, fireable.sourceAbility, transform.position);
        }
        if(turnOffSelfOnContact)
        {
            gameObject.SetActive(false);
        }

    }
}
