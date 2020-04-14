using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldOnCast : CastSequence
{

    public Animator onCastAnim;

    public override void OnCast()
    {
        
    }

    public override void OnCastRepeating()
    {
        throw new NotImplementedException();
    }
}
