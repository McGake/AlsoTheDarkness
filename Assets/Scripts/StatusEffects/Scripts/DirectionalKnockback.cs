using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DirectionalKnockback", menuName = "StatusEffects/DirectionalKnockback", order = 1)]
public class DirectionalKnockback : Status //TODO: perhaps to turn this and things like this to an event that the battle actor listens for and self applies 
{
    public float knockBackDistance;

    public float knockBackSpeedMod;

    private float squareKnockBackDistance;

    private Vector3 thisKnockBackAmount = Vector3.zero;

    private Vector3 curKnockBackAmount = Vector3.zero;

    private Vector3 knockBackDirection;

    private GameObject retainedDeliveryObject; 

    public override void SetReferences(Ability sourceAbility, GameObject deliveryObject)
    {
        retainedDeliveryObject = deliveryObject;

    }

    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {
        curKnockBackAmount = Vector3.zero;
        squareKnockBackDistance = Mathf.Pow(knockBackDistance, 2f);
        knockBackDirection = (bbA.gameObject.transform.position - retainedDeliveryObject.transform.position).normalized;
    }

    public override void DoStatus(BaseBattleActor bbA)
    {
        if (curKnockBackAmount.sqrMagnitude >= squareKnockBackDistance)
        {
            //TODO: this should End knockback
            return;
        }
        thisKnockBackAmount = knockBackDirection * knockBackSpeedMod * Time.deltaTime;
        bbA.transform.position += thisKnockBackAmount;
        curKnockBackAmount += thisKnockBackAmount;

    }

    public override void DoStatusEnd(BaseBattleActor bbA)
    {
  
    }
}
