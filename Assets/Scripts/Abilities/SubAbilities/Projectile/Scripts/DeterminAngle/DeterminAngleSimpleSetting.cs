using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SimpleSetting", menuName = "SubProjectileAbility/DeterminAngle/SimpleSetting", order = 1)]
public class DeterminAngleSimpleSetting : SubProjectileAbility
{
    public float projectileFireAngle;

    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {       
        pa.quatProjectileFireAngle = Quaternion.Euler(0f, 0f, projectileFireAngle);
        EndProjectileSubAbility();
    }
}
