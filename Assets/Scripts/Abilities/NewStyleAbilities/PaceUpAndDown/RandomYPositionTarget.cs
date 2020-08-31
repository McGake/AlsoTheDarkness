using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RandomYPositionTarget", menuName = "SubAbility/Targeting/RandomYPositionTarget", order = 1)]
public class RandomYPositionTarget : SubAbility
{
    public float maxDist;
    public float minDist;

    private float randomDist;
    //private bool skip = true;

    public override void DoInitialSubAbility(Ability ab)
    {
        randomDist = Random.Range(minDist, maxDist);

        randomDist = randomDist * RandomSignVal();

        ab.positionTargets.Add(new Vector3(ab.owner.transform.position.x, ab.owner.transform.position.y + randomDist, ab.owner.transform.position.z));
        EndSubAbility();
    }

    public override void DoSubAbility(Ability ab)
    {
        throw new System.NotImplementedException();
    }



    private int RandomSignVal()
    {
        return (Random.Range(0, 2) * 2 - 1);
       
    }


}
