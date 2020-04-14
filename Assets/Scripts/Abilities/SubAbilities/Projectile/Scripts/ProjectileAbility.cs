using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ProjectileAbility", menuName = "SubAbilities/ProjectileAbility", order = 1)]
public class ProjectileAbility : SubAbility
{
#pragma warning disable 649
    [SerializeField]
    private List<SubProjectileAbility> inspectorProjectileSubAbilities;
#pragma warning restore 649

    [System.NonSerialized]
    private List<SubProjectileAbility> projectileSubAbilities = new List<SubProjectileAbility>();

    private int curSubAbility = 0;

    [HideInInspector]
    public Ability ability;

    [HideInInspector]
    public List<Transform> sources = new List<Transform>();

    public int numberOfProjectiles;

    [HideInInspector]
    public int projectilesFired = 0;

    [HideInInspector]
    public Quaternion quatProjectileFireAngle;

    [HideInInspector]
    public float power;

    public delegate void DelProjectileSubAbilityOver();
   // public DelProjectileSubAbilityOver ProjectileSubAbilityOver;

    public void Awake()
    {
        SetUpProjectileAbility();
        curSubAbility = 0;
        projectilesFired = 0;
    }

    private void SetUpProjectileAbility()
    {
        for(int i = 0; i < inspectorProjectileSubAbilities.Count; i++)
        {
            projectileSubAbilities.Add(Instantiate(inspectorProjectileSubAbilities[i]));
        }
    }


    public override void DoInitialSubAbility(Ability ab)
    {
        sources.Clear();
        curSubAbility = 0;
        projectilesFired = 0;
        ability = ab;
        SetUpNextProjectileSubAb();
        power = 0f;
        Debug.Log("initial projectile ability ran");
    }

    public override void DoSubAbility(Ability ab)
    {
        
        projectileSubAbilities[curSubAbility].DoProjectileSubAbility(this);

      
    }

    public void ProjectileSubAbOver()
    {
        FinishLastSubAb();
        IncrementCurSubAb();

        if (ProjectileIsOver())
        {
            EndProjectile();
        }
        else
        {
            SetUpNextProjectileSubAb();
        }              
    }

    private void IncrementCurSubAb()
    {
        curSubAbility++;
        if (curSubAbility >= projectileSubAbilities.Count)
        {
            curSubAbility = 0;
        }
    }

    private bool ProjectileIsOver()
    {
        if (projectilesFired >= numberOfProjectiles)//TODO: This should be a function that determins weather or not the projectile ability is over based on arbitrary code. for exampl some might keep shooting untill hit or something
        {
            return true;
        }
        return false;
    }

    private void EndProjectile()
    {
        EndSubAbility();
    }

    private void SetUpNextProjectileSubAb()
    {
        projectileSubAbilities[curSubAbility].EndProjectileSubAbility = ProjectileSubAbOver;
        projectileSubAbilities[curSubAbility].DoInitialProjectileSubAbility(this);
    }

    private void FinishLastSubAb()
    {
        projectileSubAbilities[curSubAbility].DoFinishProjectileSubAbility(this);
    }

}
