using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Casting", menuName = "SubAbilities/Prep/Casting", order = 1)]
public class Casting : SubAbility
{
    public override void DoInitialSubAbility(Ability ab)
    {
        SetNewAnimation("castSpell", ab);
        EndSubAbility();
    }

    public override void DoSubAbility(Ability aB)
    {     
    }
}
