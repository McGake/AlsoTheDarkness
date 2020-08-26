using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireable : MonoBehaviour
{
    public Ability sourceAbility;

    public void SetSourceAbility(Ability newSource)
    {
        sourceAbility = newSource;
        SetPhysicsLayer();
    }


    private void SetPhysicsLayer()
    {
        if (sourceAbility.actorType != null)
        {
            if (sourceAbility.actorType == typeof(BattlePC))
            {
                gameObject.layer = LayerMask.NameToLayer("PCProjectile");
            }
            else if (sourceAbility.actorType == typeof(BaseEnemy))
            {
                gameObject.layer = LayerMask.NameToLayer("EnemyProjectile");
            }
        }
    }
}
