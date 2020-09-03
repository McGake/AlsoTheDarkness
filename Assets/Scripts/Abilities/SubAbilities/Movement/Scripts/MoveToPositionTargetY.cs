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
        direction = new Vector3(0, ab.positionTargets[0].y - ab.owner.transform.position.y, 0);
       // direction.z = ab.owner.transform.position.z;
        totalMovement = Vector3.zero;
        Debug.Log("number of position targets from Pace ability " + ab.positionTargets.Count);
        distance = ab.owner.transform.position.y - ab.positionTargets[0].y;
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
            ab.owner.transform.position = new Vector3(ab.owner.transform.position.x, ab.positionTargets[0].y, 0);
            SetNewAnimation("stand", ab);
            EndSubAbility();
        }
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