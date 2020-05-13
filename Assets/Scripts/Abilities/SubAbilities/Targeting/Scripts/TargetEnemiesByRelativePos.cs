using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TargetEnemiesByRelativePosition", menuName = "SubAbilities/Targeting/TargetEnemiesByRelativePosition", order = 1)]
public class TargetEnemiesByRelativePos : SubAbility
{
    public bool noForward;
    public bool noBackward;
    public bool noUp;
    public bool noDown;

    private List<GameObject> tempEnemies = new List<GameObject>();

    private Ability abilty;

    public override void DoInitialSubAbility(Ability ab)
    {
        abilty = ab;
        ab.StartSelectAllEnemeies(this, ab.actorType);
    }

    public override void DoSubAbility(Ability ab)
    {
        //if(skipFirst == true)
        //{
        //    skipFirst = false;
        //    return;//This skips one frame incase the player has already pressed a this frame to select the overall ability. TODO: do this better somehow and check if this is really even needed
        //}

    }

    public override void OnSelectionFinished(List<GameObject> selectedObjects)
    {
        Debug.Log(selectedObjects.Count + " objects retuned");
        tempEnemies.AddRange(selectedObjects);
        Vector3 pos = abilty.owner.transform.position;

        Vector3 forward = abilty.owner.transform.right;
        Vector3 backward =-1f * abilty.owner.transform.right;
        Vector3 up = abilty.owner.transform.up;
        Vector3 down = -1f * abilty.owner.transform.up;


        for (int i = 0; i < tempEnemies.Count; i++)
        {
            if (noBackward)
            {
                if (Vector3.Dot(backward, abilty.owner.transform.InverseTransformPoint(tempEnemies[i].transform.position)) > 0)
                {
                    tempEnemies.Remove(tempEnemies[i]);
                    //Destroy(tempEnemies[i]);
                }
            }
            else if (noForward)
            {
                if (Vector3.Dot(forward, abilty.owner.transform.InverseTransformPoint(tempEnemies[i].transform.position)) > 0)
                {
                    tempEnemies.Remove(tempEnemies[i]);
                    //Destroy(tempEnemies[i]);
                }
            }
            else if(noUp)
            {
                if (Vector3.Dot(up, abilty.owner.transform.InverseTransformPoint(tempEnemies[i].transform.position)) > 0)
                {
                    tempEnemies.Remove(tempEnemies[i]);
                    //Destroy(tempEnemies[i]);
                }
            }
            else if (noDown)
            {
                if (Vector3.Dot(up, abilty.owner.transform.InverseTransformPoint(tempEnemies[i].transform.position)) > 0)
                {
                    tempEnemies.Remove(tempEnemies[i]);
                    //Destroy(tempEnemies[i]);
                }
            }

        }
        if (tempEnemies.Count > 0)
        {
            abilty.objectTargets.AddRange(tempEnemies);
        }
        else
        {

        }

        EndSubAbility();
    }
}
 
