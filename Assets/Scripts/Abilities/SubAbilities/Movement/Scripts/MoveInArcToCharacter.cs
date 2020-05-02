using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "MoveInArcToCharacter", menuName = "SubAbilities/Movement/MoveInArcToCharacter", order = 1)]
public class MoveInArcToCharacter : SubAbility
{
    public float arcModifier;

    public float horizontalSpeed;

    private Vector3 movementThisFrame;

    private Vector3 direction;

    private float yPos;

    private Vector3 startPos;

    private Vector3 targetPos;

    public float timeModifier;

    public float height;

    private float time;

    public override void DoInitialSubAbility(Ability ab)
    {
        startPos = ab.owner.transform.position;
        targetPos = ab.objectTargets[0].transform.position;
        time = 0;
    }

    public override void DoSubAbility(Ability ab)
    {

        time += Time.time * timeModifier;
       ab.owner.transform.position= Parabola(startPos, targetPos,height, time);

        //movementThisFrame.x = direction.x * horizontalSpeed * Time.deltaTime;
        //ab.owner.transform.position += movementThisFrame;
        //if(Arrived(ab.singleObjectTarget.transform.position, ab.owner.transform.position))
        //{
        //    ab.owner.transform.position = new Vector3( ab.singleObjectTarget.transform.position.x, ab.owner.transform.position.y, ab.owner.transform.position.z);
        //    EndSubAbility();
        //}




    }

    public bool Arrived(Vector3 target, Vector3 curPos)
    {
        Debug.Log("target x " + target.x + " curPos x " + curPos.x);
        if(Mathf.Round(target.x) ==Mathf.Round( curPos.x))
        {
            return true;
        }
        return false;
    }

    public Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }


}
