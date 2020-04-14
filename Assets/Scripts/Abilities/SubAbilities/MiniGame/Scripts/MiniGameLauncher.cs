using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameLauncher : SubAbility
{
    MiniGame mG;

    public void Awake()
    {
        mG = Instantiate(mG);
    }

    public override void DoInitialSubAbility(Ability aB)
    {
        LaunchMiniGame(aB);
    }

    private void LaunchMiniGame(Ability aB)
    {
        mG.SetAbility(aB);
        aB.KickOffMiniGame(mG);
    }

    public override void DoSubAbility(Ability aB)
    {
        if (mG.IsFinished())//TODO: turn this into a callback or something on the miniGame
        {
            EndSubAbility();
        }
    }

}
