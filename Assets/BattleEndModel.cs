using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BattleEndModel : UIMVC
{
    private  List <string> readouts = new List<string>();

    private List<string> battleData;
    public override void MVCSetup(object obj)
    {
        base.MVCSetup(obj);
        battleData = (List<string>)obj;
        populateReadouts();
        mVCHelper.CallEvent(UIEvents.dataChanged, readouts);
    }


    private void populateReadouts()
    {
        readouts.Add( "Victory!");
        readouts.Add( "Exp Gained: " + battleData[1]);
        readouts.Add( "Gold Looted: " + battleData[2]);
        for(int i = 3; i < battleData.Count; i++)
        {
            readouts.Add(battleData[i] + "gained a level");
        }
    }


}
