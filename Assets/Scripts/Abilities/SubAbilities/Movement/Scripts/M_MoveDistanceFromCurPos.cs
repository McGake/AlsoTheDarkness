using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveDistance", menuName = "SubAbilities/Movement/DistanceFromCurPos", order = 1)]
public class M_MoveDistanceFromCurPos : SubAbility
{
    public float moveSpeed;
    public Vector3 distanceToMove;
    private Vector3 moveTarget;

    public override void DoInitialSubAbility(Ability ab)
    {
        SetNewAnimation("walk", ab);
        CalculateTarget(ab.owner);
    }

    public override void DoSubAbility(Ability ab)
    {
        if (ArrivedAtTarget(ab.owner,moveTarget) == false)
        {
            MoveToTarget(ab.owner, moveTarget);
        }
        else
        {
            SetNewAnimation("stand", ab);
            EndSubAbility();      
            
        }    
    }

    private void CalculateTarget(GameObject owner)
    {

        moveTarget = owner.transform.position + distanceToMove;
    }


    private void MoveToTarget(GameObject owner, Vector2 target)
    {
        owner.transform.position = Vector3.MoveTowards(owner.transform.position, target, moveSpeed * Time.deltaTime);
    }

    private bool ArrivedAtTarget(GameObject owner, Vector2 target)
    {


        return (Vector3)target == owner.transform.position;
    }
}
