using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnContact : StatusDeliverer
{
    public LayerMask layerMask;

    public List<string> targetTypes;
    public List<string> otherCollideableTypes;

#pragma warning disable 649
    [SerializeField]
    private List<Status> inspectorStatusesToAdd;
#pragma warning restore 649



    private List<Collider2D> alreadyColidedWith;

    public WorldEffect worldEffectScript;





    public void OnEnable()
    {
        Debug.Log("effect on contact onenable called " + this.name);
        GetInspectorStatuses();
        SetUpStatuses();
    }



    public void Start()
    {
        
        //alreadyColidedWith.Clear();


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
        Debug.Log("enter2d");
        foreach(string type in targetTypes)
        {
            if(type == col.tag)
            {
                ApplyEffect(col);
            }            
        }
        foreach(string type in otherCollideableTypes)
        {
            if(type == col.tag)
            {
                OnHitOther(col);
            }
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {

    }

    public void OnTriggerExit2D(Collider2D col)
    {
        
    }

    #region Effects To Apply
    private void ApplyEffect(Collider2D col)
    {
        BaseBattleActor actorToAddEffectTo;
        actorToAddEffectTo = col.GetComponent<BaseBattleActor>();
        Debug.Log("effect applying");
        foreach(Status status in statusesToAdd)
        {
            actorToAddEffectTo.AddStatus(status);
        }
    }

    private void OnHitOther(Collider2D col)
    {

    }

    private void EndWorldEffect()
    {

    }
    #endregion Effects To Apply

}

public class StatusDeliverer:MonoBehaviour
{
    public Ability sourceAbility;

    public void SetSourceAbility(Ability newSource)
    {
        sourceAbility = newSource;
    }

    public void SetUpStatuses()
    {
        foreach (Status status in statusesToAdd)
        {
            Debug.Log("set up called for " + status.name);
            status.SetUpStatus(sourceAbility, gameObject);
        }
    }

    protected List<Status> statusesToAdd = new List<Status>();
}
