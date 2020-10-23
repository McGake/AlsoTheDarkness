using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldModel : MonoBehaviour, IGoldModel
{

    public int GetCurrentGold()
    {
        return PartyManager.curParty.gp;
    }

}
