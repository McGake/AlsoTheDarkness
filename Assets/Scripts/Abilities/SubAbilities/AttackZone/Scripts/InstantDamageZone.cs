using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InstantDamageZone", menuName = "SubAbilities/Attack/InstantDamageZone", order = 1)]
public class InstantDamageZone : SubAbility
{
#pragma warning disable 649
    [SerializeField]
    private GameObject damageZonePrfb;
#pragma warning restore 649

    private GameObject damageZone;

    public void Awake()
    {

    }

    public override void DoInitialSubAbility(Ability ab)
    {
        damageZone = ab.battlePooler.ProduceObject(damageZonePrfb);
        damageZone.transform.position = ab.owner.transform.position;
        //damageZone.SetActive(true);
        WaitForMinimumExposure();

    }

    public override void DoSubAbility(Ability ab)
    {
    }


    public IEnumerator WaitForMinimumExposure()
    {
        yield return new WaitForFixedUpdate();
        damageZone.SetActive(false);
        EndSubAbility();       
    }
}
