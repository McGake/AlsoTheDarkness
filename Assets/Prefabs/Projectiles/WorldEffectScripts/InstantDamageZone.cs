using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDamageZone : WorldEffect
{
    public GameObject damageZone;


    public override void InitialWorldEffect(GameObject owner)
    {
    }

    public override void DoWorldEffect(GameObject owner)
    {
        GameObject.Instantiate(damageZone, owner.transform);
    }

    public override void FinalWorldEffect(GameObject ownder)
    {
    }


}
