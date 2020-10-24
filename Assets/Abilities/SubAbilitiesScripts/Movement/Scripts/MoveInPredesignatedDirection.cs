using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MoveInPredesignatedDirection", menuName = "SubAbilities/Movement/MoveInPredesignatedDirection", order = 1)]
public class MoveInPredesignatedDirection : SubAbility
{
    public float moveSpeed;
    public float distance;
    public Vector3 direction;

    private Vector3 adjustedDirection;
    private float sqrDistance;
    private Vector3 movementThisFrame;
    private Vector3 totalMovement;
    private int flipValue = 1;
    private Quaternion previousRotation;
    public override void DoInitialSubAbility(Ability ab)
    {
        flipValue = GetOwnerFacing(ab);
        SetNewAnimation("walk", ab);
        EnsureDirectionDataIsUseable(ab);
        ResetStartValues(ab);

    }

    private void EnsureDirectionDataIsUseable(Ability ab)
    {
        adjustedDirection = direction.normalized;
        adjustedDirection.z = ab.Owner.transform.position.z;
    }

    private void ResetStartValues(Ability ab)
    {
        totalMovement = Vector3.zero;
        sqrDistance = Mathf.Pow(distance, 2);
        previousRotation = ab.Owner.transform.rotation;
    }

    public override void DoSubAbility(Ability ab)
    {
        ChangeDirectionOnRotationChange(ab);
        MoveInDirection(ab);
        CheckForMovementCompleted();
    }

    private void ChangeDirectionOnRotationChange(Ability ab)
    {
        if (previousRotation.eulerAngles.y != ab.Owner.transform.rotation.eulerAngles.y)
        {
            flipValue = flipValue * -1;
            previousRotation = ab.Owner.transform.rotation;
        }
    }

    private void MoveInDirection(Ability ab)
    {
        movementThisFrame = new Vector3((adjustedDirection.x * flipValue) * moveSpeed * Time.deltaTime, adjustedDirection.y * moveSpeed * Time.deltaTime, 0);
        ab.Owner.transform.position += movementThisFrame;
        totalMovement += new Vector3(Mathf.Abs(movementThisFrame.x), Mathf.Abs(movementThisFrame.y), Mathf.Abs(movementThisFrame.z));
    }

    private void CheckForMovementCompleted()
    {
        if (totalMovement.sqrMagnitude > sqrDistance)
        {
            EndSubAbility();
        }
    }


}

