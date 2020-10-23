using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveAllTargetsCrook", menuName = "SubAbilities/Movement/MoveAllTargetsCrook", order = 1)]
public class MoveAllTargetsCrook : SubAbility
{
    private Vector3 moveDirection;
    public float moveSpeed;
    public float moveDistance;
    private Vector3 toMoveThisUpdate;
    private Vector3 totalMove;
    private float moveDistanceSquared;

    public override void DoInitialSubAbility(Ability ab)
    {
        moveDirection = ab.positionTargets[0];
        moveDirection.Normalize();
        moveDistanceSquared = Mathf.Pow(moveDistance, 2);
        totalMove = Vector3.zero;
    }

    public override void DoSubAbility(Ability ab)
    {

        toMoveThisUpdate = moveDirection * moveSpeed * Time.deltaTime;
        for(int i = 0; i < ab.objectTargets.Count; i++)
        {
            //TODO: add logic here that will prvent movment behind pc no matter which direction pc is facing
            ab.objectTargets[i].transform.position += toMoveThisUpdate;
        }
        totalMove += toMoveThisUpdate;
        if(totalMove.sqrMagnitude >= moveDistanceSquared)
        {
            EndSubAbility();
        }

    }
}
