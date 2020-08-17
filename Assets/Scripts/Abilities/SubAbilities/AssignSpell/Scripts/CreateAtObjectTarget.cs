using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CreateAtObjectTarget", menuName = "SubAbilities/Object/CreateAtObjectTarget", order = 1)]
public class CreateAtObjectTarget : SubAbility
{
#pragma warning disable 649
    [SerializeField]
    private GameObject objectToAdd;
#pragma warning restore 649

    private GameObject objectToAddInstance;

    private CastSequence castSequence;

    public override void DoInitialSubAbility(Ability ab)
    {
        objectToAddInstance = GameObject.Instantiate(objectToAdd, ab.objectTargets[0].transform.position, Quaternion.identity);
        castSequence = objectToAddInstance.GetComponent<CastSequence>();
        castSequence.OnCast();
        EndSubAbility();
    }

    public override void DoSubAbility(Ability ab)
    {

    }
}


