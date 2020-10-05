using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArmy : MonoBehaviour
{
    public OverworldBattleView overworldBattleView;

    public int size;

    public float dangerRating;

    public int defences;

    BattleArmy enemy;
    public void DoAttackTurn(BattleArmy enemy)
    {
        this.enemy = enemy;
        //enemy.overworldBattleView.PlayAttackAndCallbackWhenDone(EndAttack);
        int damage = (int)((size * dangerRating) - enemy.defences);
        enemy.size -= damage;
        enemy.overworldBattleView.PlayAttackAndCallbackWhenDone(EndAttack,damage);
        
    }

    void EndAttack()
    {
        Debug.Log("battle army callback");
        enemy.overworldBattleView.PlayOngoingBattleAnim();

    }

}
