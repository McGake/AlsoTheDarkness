using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveAllTargets", menuName = "SubAbilities/Movement/MoveAllTargets", order = 1)]
public class MoveAllTargets : SubAbility
{
    public Vector3 moveDirection;
    public float moveSpeed;
    public float moveDistance;
    private Vector3 toMoveThisUpdate;
    private Vector3 totalMove;
    private float moveDistanceSquared;

    public override void DoInitialSubAbility(Ability ab)
    {
        Debug.Log("move all targets called");
        moveDirection.Normalize();
        moveDistanceSquared = Mathf.Pow(moveDistance, 2);
        totalMove = Vector3.zero;
    }

    public override void DoSubAbility(Ability ab)
    {

        toMoveThisUpdate = moveDirection * moveSpeed * Time.deltaTime;
        for(int i = 0; i < ab.objectTargets.Count; i++)
        {
            ab.objectTargets[i].transform.position += toMoveThisUpdate;
        }
        totalMove += toMoveThisUpdate;
        if(totalMove.sqrMagnitude >= moveDistanceSquared)
        {
            EndSubAbility();
        }

    }
}
