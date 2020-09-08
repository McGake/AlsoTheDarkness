using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScaleOverTime", menuName = "SubAbility/Movement/ScaleOverTime", order = 1)]
public class ScaleOverTime : SubAbility
{
    public Vector3 ScaleAmount;
    public float timeToScale;
    private float startTime;
    private float timeElapsed;
    private float lerpVal;


    private float xScale = 1f;
    private float yScale = 1f;

    private float startX;
    private float startY;

    public override void DoInitialSubAbility(Ability ab)
    {
        startTime = Time.time;
        startX = ab.Owner.transform.localScale.x;
        startY = ab.Owner.transform.localScale.y;
    }

    public override void DoSubAbility(Ability ab)
    {
        timeElapsed = Time.time - startTime;
        lerpVal = timeElapsed / timeToScale;
        float xScale = Mathf.Lerp(startX, ScaleAmount.x, lerpVal);
        float yScale = Mathf.Lerp(startY, ScaleAmount.y, lerpVal);
        ab.Owner.transform.localScale = new Vector3(xScale, yScale, 1f);

        if(lerpVal >= 1f)
        {
            EndSubAbility();
        }
    }



    private int RandomSignVal()
    {
        return (Random.Range(0, 2) * 2 - 1);

    }


}
