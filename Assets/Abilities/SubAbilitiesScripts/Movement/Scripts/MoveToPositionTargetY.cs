using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToPositionTargetY", menuName = "SubAbilities/Movement/MoveToPositionTargetY", order = 1)]
public class MoveToPositionTargetY : SubAbility
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
        direction = new Vector3(0, ab.positionTargets[0].y - ab.Owner.transform.position.y, 0);
       // direction.z = ab.owner.transform.position.z;
        totalMovement = Vector3.zero;
        distance = ab.Owner.transform.position.y - ab.positionTargets[0].y;
        sqrDistance = Mathf.Pow(distance, 2);
        previousRotation = ab.Owner.transform.rotation;
    }

    public override void DoSubAbility(Ability ab)
    {
        ChangeDirectionOnRotationChange(ab);
        movementThisFrame = (direction * flipValue) * moveSpeed * Time.deltaTime;
        ab.Owner.transform.position += movementThisFrame;
        totalMovement += new Vector3(Mathf.Abs(movementThisFrame.x), Mathf.Abs(movementThisFrame.y), Mathf.Abs(movementThisFrame.z));

        if (totalMovement.sqrMagnitude > sqrDistance)
        {
            ab.Owner.transform.position = new Vector3(ab.Owner.transform.position.x, ab.positionTargets[0].y, 0);
            SetNewAnimation("stand", ab);
            EndSubAbility();
        }
    }

    private Quaternion previousRotation;
    private void ChangeDirectionOnRotationChange(Ability ab)
    {
        if (previousRotation.eulerAngles.y != ab.Owner.transform.rotation.eulerAngles.y)
        {
            Debug.Log("rotation change " + previousRotation + " " + ab.Owner.transform.rotation);
            direction = direction * -1;
            previousRotation = ab.Owner.transform.rotation;
        }
    }
}