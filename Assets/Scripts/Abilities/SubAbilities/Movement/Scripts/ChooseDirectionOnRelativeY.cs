using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ChooseDirecitonOnRelativeY", menuName = "SubAbility/AIMovement/ChooseDirectionOnRelativeY", order = 1)]
public class ChooseDirectionOnRelativeY : SubAbility
{
    public Vector3 direction0;
    public Vector3 direction1;

    public override void DoInitialSubAbility(Ability ab)
    {
        if (ab.objectTargets[0] != null)
        {

            if ((ab.objectTargets[0].transform.position.y - ab.owner.transform.position.y) > 0)
            {
                ab.positionTargets.Add(direction0.normalized * ab.owner.transform.right.x);
                EndSubAbility();
            }
            else
            {
                ab.positionTargets.Add(direction1.normalized * ab.owner.transform.right.x);
                EndSubAbility();
            }
        }
    }


    public override void DoSubAbility(Ability ab)
    {
    }

}
 
