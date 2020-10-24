using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CenterSpread", menuName = "SubProjectileAbility/DeterminAngle/CenterSpread", order = 1)]
public class DeterminAngleCenterSpread : SubProjectileAbility
{
    public float spreadAngle;
    public float inspectorSpreadOrientation;
    private float spreadOrientation;
    private float invertingValue = 1f;
    public override void DoInitialProjectileSubAbility(ProjectileAbility pa)
    {
        spreadOrientation = inspectorSpreadOrientation;
        if(pa.sources[0].transform.right.x == -1f) 
        {
            spreadOrientation = 180 - inspectorSpreadOrientation;
        }
    }

    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {
        CalculateNextSpreadAngle(pa);
        EndProjectileSubAbility();
    }

    //This looks convoluted because it is slightly. The math on determining a spread division has even/odd exceptions which make it look strange. 
    //However, notice how isolated this complexity is. This plus a fire sub ability and a source sub ability make a spreadshot. 
    //Change it out for a straight shot or a targeted shot or any kind of shot and you would never notice the difference.
    private void CalculateNextSpreadAngle(ProjectileAbility pa) 
    {
        float angleIndexMultiplyer = 0f;

        if (pa.numberOfProjectiles % 2 == 0)//if number of prjectiels is even then begin sending one projectile along calculated divided angles
        {
            float valToCeil = (pa.projectilesFired + 1f) / 2f;
            angleIndexMultiplyer = Mathf.Ceil(valToCeil);
        }
        else
        {
            angleIndexMultiplyer = Mathf.Ceil(pa.projectilesFired / 2f); //if number of projectiles is odd, the first angle is right down the middle
        }

        invertingValue = -invertingValue; //flip between positive and negative angle for each new projectile for an even spread. so one at +15 and one at -15

        float sizeOfDivisions = (spreadAngle * 2) / (pa.numberOfProjectiles - 1); //Calculate how large is the angle between each projectile

        float angleToFireAt = spreadOrientation + ((angleIndexMultiplyer * sizeOfDivisions) * invertingValue); //actual angle for this projectile

        if (float.IsNaN(angleToFireAt)) //prevent gimble lock
        {
            angleToFireAt = 180f * invertingValue;
        }

        pa.quatProjectileFireAngle = Quaternion.Euler(0f, 0f, angleToFireAt);
    }
}
