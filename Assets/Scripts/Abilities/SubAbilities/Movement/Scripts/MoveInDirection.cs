using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveInDirection", menuName = "SubAbilities/Movement/MoveInDirection", order = 1)]
public class MoveInDirection : SubAbility
{
    public float moveSpeed;

    public float distance;

    private float sqrDistance;

    private Vector3 direction;

    private Vector3 movementThisFrame;

    private Vector3 totalMovement;

    public override void DoInitialSubAbility(Ability ab)
    {
        SetNewAnimation("walk", ab);
        direction = ab.positionTargets[0];
        direction.z = ab.owner.transform.position.z;
        totalMovement = Vector3.zero;
        sqrDistance = Mathf.Pow(distance, 2);        
    }

    public override void DoSubAbility(Ability ab)
    {
        //if (ArrivedAtTarget(ab.owner,moveTarget) == false)
        //{
        //    MoveToTarget(ab.owner, moveTarget);
        //}
        movementThisFrame = direction * moveSpeed * Time.deltaTime;
        ab.owner.transform.position += movementThisFrame;
        totalMovement += movementThisFrame;

        if(totalMovement.sqrMagnitude > sqrDistance)
        {
            ab.pcAnimator.SetBool("walk", false);
            ab.pcAnimator.SetBool("stand", true);

            EndSubAbility();
        }
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
