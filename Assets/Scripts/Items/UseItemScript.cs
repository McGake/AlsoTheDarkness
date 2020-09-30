﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public abstract class UseItemScript : ScriptableObject, ISelectionBehaviorOLD
{
    public virtual void DoSelectionBehavior()
    {
        throw new System.NotImplementedException();
    }

    public virtual void UseItem(PC pcToUse)
    {

    }
}