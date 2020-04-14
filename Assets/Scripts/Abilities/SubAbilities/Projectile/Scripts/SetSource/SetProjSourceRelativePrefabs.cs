using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SetProjSourceReleativePrefabs", menuName = "SubProjectileAbility/Source/RelativePrefabs", order = 1)]
public class SetProjSourceRelativePrefabs : SubProjectileAbility
{
#pragma warning disable 649
    [SerializeField]
    private GameObject prefabOfSourcePositions;
#pragma warning restore 649

    private GameObject goOfSourcePositions;

    public void Awake()
    {
        
        goOfSourcePositions = GameObject.Instantiate(prefabOfSourcePositions);

    }


    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {
        goOfSourcePositions.transform.SetParent(pa.ability.owner.transform);
        goOfSourcePositions.transform.position = Vector3.zero;
        pa.sources.Add(pa.ability.owner.transform);

        EndProjectileSubAbility();
    }
}
