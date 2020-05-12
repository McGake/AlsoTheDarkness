using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LimitToClosestTarget", menuName = "SubAbilities/Targeting/LimitToClosestTarget", order = 1)]
public class LimitToClosestTarget : SubAbility//TODO: THIs is no longer target single. rename to target single pc or something
{

    private Ability ability;

    private GameObject leader;

    private float closestDistance = 10000f;

    private Vector3 tempDistanceVec;

    private float tempSqrMag;

    public override void DoInitialSubAbility(Ability ab)
    {
        leader = null;
        for(int i = 0; i < ab.objectTargets.Count; i++)
        {
            tempDistanceVec = ab.owner.transform.position - ab.objectTargets[i].transform.position;
            tempSqrMag = tempDistanceVec.sqrMagnitude;

            if(tempSqrMag < closestDistance)
            {
                closestDistance = tempSqrMag;
                leader = ab.objectTargets[i];
            }
        }
        if(leader != null)
        {
            ab.objectTargets[0] = leader;
        }


        EndSubAbility();
    }

    public override void DoSubAbility(Ability ab)
    {
            
        
    }

}
 
