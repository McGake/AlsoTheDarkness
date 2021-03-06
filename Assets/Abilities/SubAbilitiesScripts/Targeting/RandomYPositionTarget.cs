﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RandomYPositionTarget", menuName = "SubAbility/Targeting/RandomYPositionTarget", order = 1)]
public class RandomYPositionTarget : SubAbility
{
    public float maxDist;
    public float minDist;

    private float randomDist;
    //private bool skip = true;

    public override void DoInitialSubAbility(Ability ab)
    {
        randomDist = Random.Range(minDist, maxDist);

        randomDist = randomDist * RandomSignVal();

        ab.positionTargets.Add(new Vector3(ab.Owner.transform.position.x, ab.Owner.transform.position.y + randomDist, ab.Owner.transform.position.z));
        EndSubAbility();
    }

    public override void DoSubAbility(Ability ab)
    {
        Debug.Log("this should not be called or be called only once");
    }



    private int RandomSignVal()
    {
        return (Random.Range(0, 2) * 2 - 1);
       
    }


}
