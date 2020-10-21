using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeElapsed", menuName = "DurationTypes/TimeElapsed", order = 1)]
[System.Serializable]
public class TimeElapsed : DurationType
{
    public float timeDuration;
    [HideInInspector]
    public float endTime;
}
