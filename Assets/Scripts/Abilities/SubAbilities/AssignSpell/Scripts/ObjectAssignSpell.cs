using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ObjectAssignSpell", menuName = "SubAbilities/Object/ObjectAssignSpell", order = 1)]
public class ObjectAssignSpell : SubAbility
{
#pragma warning disable 649
    [SerializeField]
    private GameObject objectToAdd;
#pragma warning restore 649

    private GameObject objectToAddInstance;

    private CastSequence castSequence;

    public override void DoInitialSubAbility(Ability ab)
    {
        objectToAddInstance = ab.battlePooler.ProduceObject(objectToAdd,ab.objectTargets[0].transform);
        castSequence = objectToAddInstance.GetComponent<CastSequence>();
        castSequence.OnCast();
        EndSubAbility();
    }

    public override void DoSubAbility(Ability ab)
    {

    }

}


