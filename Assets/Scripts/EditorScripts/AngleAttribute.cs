using UnityEngine;
using System.Collections;

public class AngleAttribute : PropertyAttribute
{
    public readonly float Snap;
    public readonly float Min;
    public readonly float Max;


    public AngleAttribute()
    {
        Snap = 1;
        Min = 0;
        Max = 360;
    }

    public AngleAttribute(float snap)
    {
        Snap = snap;
        Min = 0;
        Max = 360;
    }

    public AngleAttribute(float snap, float min, float max)
    {
        Snap = snap;
        Min = min;
        Max = max;
    }
}