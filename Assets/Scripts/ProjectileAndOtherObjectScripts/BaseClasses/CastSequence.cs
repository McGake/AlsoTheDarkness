using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CastSequence : MonoBehaviour
{

    private bool isFinished = false;
    public abstract void OnCast();

    public abstract void OnCastRepeating();



    public bool IsFinished()
    {
        return isFinished;
    }

    public void SetFinished()
    {
        isFinished = true;
    }
}
