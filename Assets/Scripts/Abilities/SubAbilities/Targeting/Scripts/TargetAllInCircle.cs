using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TargetAllInCircle", menuName = "SubAbilities/Targeting/TargetAllInCircle", order = 1)]
public class TargetAllInCircle : SubAbility
{


    public float circleSize;

    public LayerMask mask;

    public override void DoInitialSubAbility(Ability ab)
    {
        //ab.StartSelectAllPCsButCurrent(this);
        Collider2D[] colInCircle = Physics2D.OverlapCircleAll(ab.owner.transform.position, circleSize,mask);

        for(int i = 0; i < colInCircle.Length; i++)
        {
            ab.objectTargets.Add(colInCircle[i].gameObject);
        }
    }

    public override void DoSubAbility(Ability ab)
    {


    }

    //public override void OnSelectionFinished(List<GameObject> selectedObjects)
    //{
    //    ability.objectTargets.AddRange(selectedObjects);
    //    EndSubAbility();
    //}
}
 
