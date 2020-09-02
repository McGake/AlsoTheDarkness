using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy : BaseBattleActor
{
    public override void Update()
    {
        base.Update();
        SimpleAbilitySelector();
    }

    public float abilityIntervalMax;

    public float abilityIntervalMin;

    private float nextAbilityInterval;

    public override void Awake()
    {
        base.Awake();
        nextAbilityInterval = Random.Range(abilityIntervalMin, abilityIntervalMax);
    }

    void SimpleAbilitySelector()
    {
        if (nextAbilityInterval <= Time.time)
        {
            if (AbilityManager.abManager.IsCharacterCurrentlyDoingAbility(gameObject))
            {
                return;
            }
            else
            {          
                nextAbilityInterval = Time.time + Random.Range(abilityIntervalMin, abilityIntervalMax);
                int randAbilIndx = Random.Range(0, abilities.Count);
                AbilityManager.abManager.TurnOnAbility(abilities[randAbilIndx]);
            }
        }
    }


    public override void Die()
    {
        OnDeathCallback(gameObject);
        gameObject.SetActive(false);
    }
}
