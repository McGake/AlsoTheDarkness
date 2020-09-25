using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{

    public WalkingView walkingView;

    private void Update()
    {
        if(MultiInput.GetAButtonDown())
        {
            DoCamping();
        }
    }

    private void DoCamping()
    {
        this.enabled = false;
        foreach (PC pc in PartyManager.curParty.partyMembers)
        {
            pc.battler.GetComponent<BattlePC>().stats.hP = pc.battler.GetComponent<BattlePC>().stats.maxHP;
        }
        //returnPosition = transform.position;
        walkingView.PlayAnimationAndCallbackWhenDone("camp", EndCamping);

    }

    private void EndCamping()
    {
        TurnManager.EndTurn(this);
    }
}
