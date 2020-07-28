using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnContact : StatusDeliverer
{
    public List<string> targetTypes;
    public List<string> otherCollideableTypes;

#pragma warning disable 649
    [SerializeField]
    private List<Status> inspectorStatusesToAdd;
#pragma warning restore 649

    private List<Collider2D> alreadyColidedWith;

    private List<GameObject> objectsHit = new List<GameObject>();

    public delegate void DelSendObjectsHit(List<GameObject> osHit);
    public DelSendObjectsHit SendObjectsHit;

    public LayerMask mask;

    public bool continuous;

    public float repeatInterval;
    private float nextInterval;

    public void OnEnable()
    {
        objectsHit.Clear();
        GetInspectorStatuses();
        SetUpStatuses();
    }

    private void GetInspectorStatuses()
    {
        if (statusesToAdd.Count < inspectorStatusesToAdd.Count)
        {
            foreach (Status status in inspectorStatusesToAdd)
            {
                Status tempStat = Instantiate(status);
                statusesToAdd.Add(tempStat);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(((mask >> col.gameObject.layer)) == 1)
            {
            ApplyEffect(col);
            objectsHit.Add(col.gameObject);
            nextInterval = Time.time + repeatInterval;
        }

    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if(continuous)
        {

            if (nextInterval < Time.time)
            {
                if (((mask >> col.gameObject.layer)) == 1)
                {
                    ApplyEffect(col);
                    nextInterval = Time.time + repeatInterval;
                }
                
            }
        }
    }

    #region Effects To Apply
    private void ApplyEffect(Collider2D col)
    {
        BaseBattleActor actorToAddEffectTo;
        actorToAddEffectTo = col.GetComponent<BaseBattleActor>();
        //Debug.Log("effect applying");
        //if (actorToAddEffectTo != null)
        //{
            foreach (Status status in statusesToAdd)
            {
                actorToAddEffectTo.AddStatus(status);
            }
        //}
    }

    private void OnHitOther(Collider2D col)
    {

    }

    private void EndWorldEffect()
    {

    }

    private void OnDisable()
    {
        if (SendObjectsHit != null)
        {
            SendObjectsHit(objectsHit);
        }
    }
    #endregion Effects To Apply

}

public class StatusDeliverer:MonoBehaviour
{
    public Ability sourceAbility;

    public void SetSourceAbility(Ability newSource)
    {
        sourceAbility = newSource;
        SetPhysicsLayer();
    }

    public void SetUpStatuses()
    {
        foreach (Status status in statusesToAdd)
        {
            status.SetUpStatus(sourceAbility, gameObject);
        }
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

    protected List<Status> statusesToAdd = new List<Status>();
}
