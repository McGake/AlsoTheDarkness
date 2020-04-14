using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShowingDamage", menuName = "Effects/UI/ShowingDamage", order = 1)]
public class ShowingDamage : Status //TODO: perhaps to turn this and things like this to an event that the battle actor listens for and self applies 
{
    public SpriteRenderer sR;

    public float damage;

    public override void SetUpStatus(BaseBattleActor bbA)
    {
        sR = bbA.transform.GetComponent<SpriteRenderer>();
    }

    public override void DoStatusInitialEffect(BaseBattleActor bbA)
    {

        BattleMenuManager.battleMenuManager.ExplodeDamageText(damage, bbA.gameObject);
    }

    public override void DoStatus(BaseBattleActor bbA)
    {
        if (sR.color.a == 0)
        {
            sR.color = Color.white;
            //sR.material.SetColor("whiteColor", Color.white);
        }
        else
        {
            sR.color = Color.clear;
        }
    }

    public override void DoStatusEnd(BaseBattleActor bbA)
    {
        sR.color = Color.white;
    }
}
