using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SimpleSettingAnglePlusFacing", menuName = "SubProjectileAbility/DeterminAngle/SimpleSetAnglePlusFacing", order = 1)]
public class SimpleSetAnglePlusFacing : SubProjectileAbility
{
    public float projectileFireAngle;
    float dirFlip = 1;
    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {
        Vector3 rot = pa.ability.Owner.transform.rotation.eulerAngles;
        FlipAngleBasedOnFacing(rot);
        pa.quatProjectileFireAngle = Quaternion.Euler(0f, 0f, projectileFireAngle * dirFlip) * Quaternion.Euler(rot.x, rot.y, rot.z);
        EndProjectileSubAbility();
    }

    private void FlipAngleBasedOnFacing(Vector3 rot)//This is the first time I've wished I should have paid better attention in math class. There should be some way to translate a 35 degree left facing angle around in a circle untill it points the othe way on to the right. (the magnitude of the angle should be the same all the way around a circle but i don't know the math.
    {
        if (rot.y >= 179f)
        {
            dirFlip = 1;
        }
        if (rot.y <= 1f)
        {
            dirFlip = -1;
        }
    }
}
