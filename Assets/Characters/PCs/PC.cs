using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New PC", menuName = "character/PC")] 
public class PC : ScriptableObject
{
    public string displayName;
    public int health;
    public int lvl;
    public int atk;

    public Sprite sprite;

    public AnimatorOverrideController battleAnimOverride;

    public AnimatorOverrideController overworldAnimOverride;

}
