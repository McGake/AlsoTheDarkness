using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ChantingWithAnim", menuName = "SubAbilities/Prep/ChantingWithAnim", order = 1)]
public class ChantingWithAnim : SubAbility
{
#pragma warning disable 649
    [SerializeField]
    private float prepTime = 0f;

    private float endPrepTime=0f;

    [SerializeField]
    private GameObject animObjectInsp;

    private GameObject animObject;
#pragma warning restore 649

    public override void DoInitialSubAbility(Ability ab)
    {
        endPrepTime = prepTime + Time.time;
        SetNewAnimation("chantSpell", ab);
        if(animObject == null)
        {
            animObject = Instantiate(animObjectInsp, ab.owner.transform,false);
            //animObject.transform.position = ab.owner.transform.position;
        }
        
        animObject.SetActive(true);
    }

    public override void DoSubAbility(Ability ab)
    {
         
        if (endPrepTime <= Time.time)
        {
            animObject.SetActive(false);
            EndSubAbility();
        }
    }
}
