using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireable : MonoBehaviour
{
    public Ability sourceAbility;

    public List<FireableBehavior> fireableBehaviors;


    public void SetSourceAbility(Ability newSource)
    {
        sourceAbility = newSource;
        SetPhysicsLayer();

        foreach(FireableBehavior f in fireableBehaviors)
        {
            f.SetUpFireableBehavior(this);
        }
    }


    private void SetPhysicsLayer()
    {
        if (sourceAbility.ActorType != null)
        {
            if (sourceAbility.ActorType == typeof(BattlePC))
            {
                gameObject.layer = LayerMask.NameToLayer("PCProjectile");
            }
            else if (sourceAbility.ActorType == typeof(BaseEnemy))
            {
                gameObject.layer = LayerMask.NameToLayer("EnemyProjectile");
            }
        }
    }
}

public abstract class FireableBehavior:MonoBehaviour
{
    protected Fireable fireable;
    public virtual void SetUpFireableBehavior(Fireable fireable)
    {
        this.fireable = fireable;
    }

    public abstract void OnFire();

}
