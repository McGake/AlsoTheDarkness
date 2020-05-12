using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "RandomTwoDirections", menuName = "SubAbility/AIMovement/RandomTwoDirections", order = 1)]
public class RandomTwoDirections : SubAbility
{
    public Vector3 direction0;
    public Vector3 direction1;

    private int randomNum;

    private bool skip = true;

    public override void DoInitialSubAbility(Ability ab)
    {
        randomNum = Random.Range(0, 2);
        if (randomNum == 0)
        {
            ab.positionTargets.Add(direction0.normalized *ab.owner.transform.right.x);
            EndSubAbility();
        }
        else if (randomNum == 1)
        {
            ab.positionTargets.Add(direction1.normalized * ab.owner.transform.right.x);
            EndSubAbility();
        }
    }


    public override void DoSubAbility(Ability ab)
    {
    }

}
 
