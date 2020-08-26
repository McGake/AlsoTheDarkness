using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MoveInInspectorDirection", menuName = "SubAbilities/Movement/MoveInInspectorDirection", order = 1)]
public class MoveInInspectorDirection : SubAbility
{
public float moveSpeed;

public float distance;

public Vector3 direction;

private Vector3 adjustedDirection;

private float sqrDistance;

private Vector3 movementThisFrame;

private Vector3 totalMovement;

private int flipValue = 1;
public override void DoInitialSubAbility(Ability ab)
{
        if(ab.owner.transform.rotation.eulerAngles.y>178f )//TODO: figure out the correct way to do this and make it an subability utility if neccesary
        {
            Debug.Log("greater than 178");
            flipValue = -1;
        }
        else if(ab.owner.transform.rotation.eulerAngles.y < 2f)
        {
            Debug.Log("less than 178");
            flipValue = 1;
        }
            SetNewAnimation("walk", ab);
        adjustedDirection = direction.normalized;
    adjustedDirection.z = ab.owner.transform.position.z;
    totalMovement = Vector3.zero;
    sqrDistance = Mathf.Pow(distance, 2);
    previousRotation = ab.owner.transform.rotation;
}

public override void DoSubAbility(Ability ab)
{
    ChangeDirectionOnRotationChange(ab);
    movementThisFrame = new Vector3((adjustedDirection.x * flipValue) * moveSpeed * Time.deltaTime, adjustedDirection.y *moveSpeed * Time.deltaTime, 0);
    ab.owner.transform.position += movementThisFrame;
    totalMovement += new Vector3(Mathf.Abs(movementThisFrame.x), Mathf.Abs(movementThisFrame.y), Mathf.Abs(movementThisFrame.z));

    if (totalMovement.sqrMagnitude > sqrDistance)
    {
            EndSubAbility();
    }
}

private Quaternion previousRotation;
private void ChangeDirectionOnRotationChange(Ability ab)
{
    if (previousRotation.eulerAngles.y != ab.owner.transform.rotation.eulerAngles.y)
    {
        Debug.Log("rotation change " + previousRotation + " " + ab.owner.transform.rotation);
        flipValue = flipValue * -1;
        previousRotation = ab.owner.transform.rotation;
    }
}
}
