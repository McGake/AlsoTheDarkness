using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TargetEnemiesByRelativePosition", menuName = "SubAbilities/Targeting/TargetEnemiesByRelativePosition", order = 1)]
public class TargetEnemiesByRelativePos : SubAbility
{
    public bool below;
    public bool behind;
    public bool inFront;
    public bool above;

    public List<GameObject> tempEnemies;

    private Ability abilty;

    public override void DoInitialSubAbility(Ability ab)
    {
        abilty = ab;
        ab.StartSelectAllEnemeis(this);
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
        tempEnemies.AddRange(selectedObjects);
        Vector3 pos = abilty.owner.transform.position;

        Vector3 forward = new Vector3(-1f, 0f, 0f);
        Vector3 backward = new Vector3(1f, 0f, 0f);
        Vector3 up = new Vector3(0f, 1f, 0f);
        Vector3 down = new Vector3(0f, -1f, 0f);


        for (int i = 0; i < tempEnemies.Count; i++)
        {
            if(Vector3.Dot(backward, abilty.owner.transform.InverseTransformPoint(tempEnemies[i].transform.position)) > 0)
            {
                Destroy(tempEnemies[i]);
            }
        }

        EndSubAbility();
    }
}
 
