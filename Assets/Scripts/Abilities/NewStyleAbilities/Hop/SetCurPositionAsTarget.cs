﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SetCurPositionAsTarget", menuName = "SubAbilities/Targeting/SetCurPositionAsTarget", order = 1)]
public class SetCurPositionAsTarget : SubAbility
{
    public override void DoInitialSubAbility(Ability ab)
    {
        ab.positionTargets.Add(ab.owner.transform.position);
    }
    public override void DoSubAbility(Ability ab)
    {
    }
}
