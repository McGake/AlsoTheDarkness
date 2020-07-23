using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New PC", menuName = "character/PC")] 
public class PC : ScriptableObject
{
    public string displayName;
    public BattlePC battlePC;

    public Sprite portrait;

    public AnimatorOverrideController battleAnimOverride;

    public AnimatorOverrideController overworldAnimOverride;

}
