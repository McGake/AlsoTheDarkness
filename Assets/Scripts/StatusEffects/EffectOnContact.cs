using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnContact : MonoBehaviour
{
    public LayerMask layerMask;

    public List<string> targetTypes;
    public List<string> otherCollideableTypes;

    public EffectDefiner effect;

#pragma warning disable 649
    [SerializeField]
    private List<Status> inspectorStatusesToAdd;
#pragma warning restore 649

    private List<Status> statusesToAdd = new List<Status>();

    private List<Collider2D> alreadyColidedWith;

    public WorldEffect worldEffectScript;



    public void Start()
    {
        //alreadyColidedWith.Clear();

        foreach(Status status in inspectorStatusesToAdd)
        {
            Status tempStat = Instantiate(status);
            statusesToAdd.Add(tempStat);                           
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {        
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

    #region Effects To Apply
    private void ApplyEffect(Collider2D col)
    {
        BaseBattleActor actorToAddEffectTo;
        actorToAddEffectTo = col.GetComponent<BaseBattleActor>();

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
