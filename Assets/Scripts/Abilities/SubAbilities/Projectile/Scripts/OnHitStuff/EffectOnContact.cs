using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnContact : Fireable
{

#pragma warning disable 649
    [SerializeField]
    private List<Status> inspectorStatusesToAdd;
#pragma warning restore 649

    private List<Collider2D> alreadyColidedWith;

    private List<GameObject> objectsHit = new List<GameObject>();

    public List<Action<List<GameObject>>> ObjectsHitSubs = new List<Action<List<GameObject>>>();

    public LayerMask mask;

    public bool continuous;

    public float repeatInterval;
    private float nextInterval;

    protected List<Status> statusesToAdd = new List<Status>();


    public void SubscribeToObjectsHit(Action<List<GameObject>> subscriber)
    {
        ObjectsHitSubs.Add(subscriber);
    }

    public void UnsubscribeToObjectsHit(Action<List<GameObject>> subscriber)
    {
        ObjectsHitSubs.Remove(subscriber);
    }

    public override void SetupProjectile(Ability newSource)
    {
        base.SetupProjectile(newSource);
        objectsHit.Clear();
        GetInspectorStatuses();
        SetUpStatuses();
    }

    public void SetUpStatuses()
    {
        foreach (Status status in statusesToAdd)
        {
            status.SetReferences(sourceAbility, gameObject);
        }
    }





    private void GetInspectorStatuses()
    {
        if (statusesToAdd.Count < inspectorStatusesToAdd.Count)
        {
            foreach (Status status in inspectorStatusesToAdd)
            {
                Debug.Log(status);
                Debug.Log(sourceAbility);
                Debug.Log(sourceAbility.stats);


                Status tempStat = status.CreateStatusInstance(sourceAbility.stats);
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
        foreach(Action<List<GameObject>> sub in ObjectsHitSubs)
        {
            sub?.Invoke(objectsHit);
        }
        if(ObjectsHitSubs.Count > 0)
        {
            Debug.LogError("not all subscribers were unsubscribbed on disable");
        }
    }
    #endregion Effects To Apply

}




