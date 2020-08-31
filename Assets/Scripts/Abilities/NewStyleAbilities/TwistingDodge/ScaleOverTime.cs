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
    public override void DoInitialSubAbility(Ability ab)
    {
        startTime = Time.time;
    }

    public override void DoSubAbility(Ability ab)
    {
        timeElapsed = Time.time - startTime;

        lerpVal = timeElapsed / timeToScale;

        float xScale = Mathf.Lerp(1, ScaleAmount.x, lerpVal);
        float yScale = Mathf.Lerp(1, ScaleAmount.y, lerpVal);

        ab.owner.transform.localScale = new Vector3(xScale, yScale, 1f);

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
