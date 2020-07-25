using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New PC", menuName = "character/PC")] 
public class PCDef : ScriptableObject
{
    public string displayName;
    public GameObject battler;

    public Sprite portrait;

    public AnimatorOverrideController battleAnimOverride;

    public AnimatorOverrideController overworldAnimOverride;

}

[System.Serializable]
public class PC
{
    public string displayName;
    public GameObject battler;

    public Sprite portrait;

    public AnimatorOverrideController battleAnimOverride;

    public AnimatorOverrideController overworldAnimOverride;
}
