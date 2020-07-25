using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCFactory
{
    public PC CreatePCFromDef(PCDef pcDef)
    {
        PC newPC =  new PC();
        newPC.displayName = pcDef.displayName;
        newPC.battler = pcDef.battler;

        newPC.portrait = pcDef.portrait;

        newPC.battleAnimOverride = pcDef.battleAnimOverride;

        newPC.overworldAnimOverride = pcDef.overworldAnimOverride;

        return newPC;
    }

}
