
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BattlePC))]
public class AbilityDefInspectorSetup : Editor
{



    //public override void OnInspectorGUI()
    //{
    //    BasePC basePC = (BasePC)target;
    //    base.OnInspectorGUI();

    //    for (int i = 0; i < basePC.abilityDefs.Count; i++)
    //    {

    //        if (basePC.abilityDefs[i].abilityType == AbilityType.indirectMagic)
    //        {
    //            //Debug.Log(basePC.abilityDefs[i].GetType());

    //            IndirectMagicVars tempDef = new IndirectMagicVars();
    //            //Debug.Log(tempDef);
    //            //tempDef.abilityType = basePC.abilityDefs[i].abilityType;
    //            //basePC.abilityDefs[i] = tempDef;


    //        }
    //    }
    //}
}
