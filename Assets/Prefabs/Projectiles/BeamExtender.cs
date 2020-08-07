using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamExtender : MonoBehaviour
{
    public float chargeTime;

    public float extentionTime;

    public float sustainTime;

    public float extentionLength;

    public float startLength;

    private float endChargeTime;

    private float endExtentionTime;

    private float endSustainTime;

    private float lerpVal;

    private float startTime;

    private float curLength;

    public SpriteRenderer sR;
    public void OnEnable()
    {
        startTime = Time.time;
        endChargeTime = startTime + chargeTime;
        endExtentionTime = endChargeTime + extentionTime;
        endSustainTime = endExtentionTime + sustainTime;        
    }

    public void Update()
    {
        if(Time.time <= endChargeTime)
        {
            sR.size = new Vector2(startLength, sR.size.y);
        }
        else if (Time.time <= endExtentionTime)
        {
            lerpVal = (endExtentionTime - Time.time) / extentionTime;
            curLength = Mathf.Lerp(extentionLength, startLength, lerpVal);
            sR.size = new Vector2(curLength, sR.size.y);
        }
        if (Time.time >= endSustainTime)
        {
            sR.size = new Vector2(startLength, sR.size.y);
            gameObject.SetActive(false);          
        }
    }
}
