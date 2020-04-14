using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CenterSpread", menuName = "SubProjectileAbility/DeterminAngle/CenterSpread", order = 1)]
public class DeterminAngleCenterSpread : SubProjectileAbility
{
    public float spreadAngle;
    public float projectileFireAngle;//TODO:This is the base/center angle. the angle of the arc so to say. rename this as such
    private float invertingValue = 1f;

    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {       
        float angleIndexMultiplyer = 0f;



        if (pa.numberOfProjectiles % 2 == 0)//if number of prjectiels is even
        {
            float valToCeil = (pa.projectilesFired + 1f) / 2f;
            angleIndexMultiplyer = Mathf.Ceil(valToCeil);
        }
        else
        {
            angleIndexMultiplyer = Mathf.Ceil(pa.projectilesFired / 2f);
        }

        invertingValue = -invertingValue;

        float sizeOfDivisions = (spreadAngle * 2) / (pa.numberOfProjectiles - 1);

        float angleToFireAt = projectileFireAngle + ((angleIndexMultiplyer * sizeOfDivisions) * invertingValue);

        pa.quatProjectileFireAngle = Quaternion.Euler(0f, 0f, angleToFireAt);

        EndProjectileSubAbility();
    }
}
