using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldEffect : MonoBehaviour
{
    public delegate void DelEndWorldEffect();
    public DelEndWorldEffect EndWorldEffect;

    public abstract void InitialWorldEffect(GameObject owner);

    public abstract void DoWorldEffect(GameObject owner);

    public abstract void FinalWorldEffect(GameObject ownder);
        
}
