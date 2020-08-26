using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToPositionTarget", menuName = "SubAbilities/Movement/MoveToPositionTarget", order = 1)]
public class MoveToPositionTarget : SubAbility
{
    public float moveSpeed;

    private float distance;

    private float sqrDistance;

    private Vector3 direction;

    private Vector3 movementThisFrame;

    private Vector3 totalMovement;

    private int flipValue = 1;
    public override void DoInitialSubAbility(Ability ab)
    {
        SetNewAnimation("walk", ab);
        direction = ab.positionTargets[0];
        direction.z = ab.owner.transform.position.z;
        totalMovement = Vector3.zero;
        distance = Vector3.Magnitude(ab.owner.transform.position - ab.positionTargets[0]);
        sqrDistance = Mathf.Pow(distance, 2);
        previousRotation = ab.owner.transform.rotation;
    }

    public override void DoSubAbility(Ability ab)
    {
        ChangeDirectionOnRotationChange(ab);
        movementThisFrame = (direction * flipValue) * moveSpeed * Time.deltaTime;
        ab.owner.transform.position += movementThisFrame;
        totalMovement += new Vector3(Mathf.Abs(movementThisFrame.x), Mathf.Abs(movementThisFrame.y), Mathf.Abs(movementThisFrame.z));

        if (totalMovement.sqrMagnitude > sqrDistance)
        {
            SetNewAnimation("stand", ab);
            EndSubAbility();
        }
    }




    private void MoveToTarget(GameObject owner, Vector2 target)
    {
        owner.transform.position = Vector3.MoveTowards(owner.transform.position, target, moveSpeed * Time.deltaTime);
    }

    private Quaternion previousRotation;
    private void ChangeDirectionOnRotationChange(Ability ab)
    {
        if (previousRotation.eulerAngles.y != ab.owner.transform.rotation.eulerAngles.y)
        {
            Debug.Log("rotation change " + previousRotation + " " + ab.owner.transform.rotation);
            direction = direction * -1;
            previousRotation = ab.owner.transform.rotation;
        }
    }
}

