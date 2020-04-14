using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectDefiner", menuName = "ScriptableObjects/EffectDefiner", order = 1)]
public class EffectDefiner : ScriptableObject
{
    public string effectName;
    public List<EffectDef> effects;
    
    
}

[System.Serializable]
public class EffectDef
{
    public float interval;
    public float duration;
    public float strength;
    public EffectTypes effectType;
    public ApplicationTypes applicationType; 
}

public enum EffectTypes
{
    None = 0,
    Damage = 1,
    Poison = 2,
    Freeze = 3,
    ShowingDamage = 4,
}

public enum ApplicationTypes
{
    Instant = 1,
    Interval = 2,
    Continuous = 3,
}
