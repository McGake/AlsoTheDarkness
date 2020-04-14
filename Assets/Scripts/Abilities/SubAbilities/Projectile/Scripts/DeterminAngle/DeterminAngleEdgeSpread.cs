using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EdgeSpread", menuName = "SubProjectileAbility/DeterminAngle/EdgeSpread", order = 1)]
public class DeterminAngleEdgeSpread : SubProjectileAbility
{
    public float spreadAngle;
    public float projectileFireAngle;
    private float invertingValue = 1f;
    public bool clockwise;
    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {
        if (clockwise == true)
        {
            invertingValue = -1f;
        }
        else
        {
            invertingValue = 1f;
        }

        float curAngleAmount = ((spreadAngle * 2) / (pa.numberOfProjectiles - 1)) * pa.projectilesFired * invertingValue;

        float startingEdgeOfCone = projectileFireAngle - spreadAngle * invertingValue;

        float angleToFireAt = startingEdgeOfCone + curAngleAmount;

        pa.quatProjectileFireAngle = Quaternion.Euler(0f, 0f, angleToFireAt);

        EndProjectileSubAbility();
    }
}
