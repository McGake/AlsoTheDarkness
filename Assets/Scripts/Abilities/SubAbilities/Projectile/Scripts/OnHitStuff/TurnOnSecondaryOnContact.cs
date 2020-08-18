﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnSecondaryOnContact : MonoBehaviour
{

    public LayerMask triggerOnEnterMask;

    public GameObject objectToTurnOn;

    public bool turnOffSelfOnContact = true;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (((triggerOnEnterMask >> col.gameObject.layer)) == 1)
        {
            objectToTurnOn.transform.SetParent(null);
            objectToTurnOn.SetActive(true);
        }
        if(turnOffSelfOnContact)
        {
            gameObject.SetActive(false);
        }

    }
}