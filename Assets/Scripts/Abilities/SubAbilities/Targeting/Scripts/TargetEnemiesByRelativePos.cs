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

    public bool noFacingAway;
    public bool noFacingToward;


    
    private List<GameObject> selectedEnemies = new List<GameObject>();
    private List<GameObject> finalTargetedEnemies = new List<GameObject>();

    private Ability abilty;

    public override void DoInitialSubAbility(Ability ab)
    {
        selectedEnemies.Clear();
        finalTargetedEnemies.Clear();
        abilty = ab;
        ab.StartSelectAllEnemeies(this, ab.ActorType);
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
        selectedEnemies.AddRange(selectedObjects);
        finalTargetedEnemies.AddRange(selectedObjects);
        Vector3 pos = abilty.Owner.transform.position;
        Vector3 forward = abilty.Owner.transform.right;
        Vector3 backward =-1f * abilty.Owner.transform.right;
        Vector3 up = abilty.Owner.transform.up;
        Vector3 down = -1f * abilty.Owner.transform.up;
        Transform ownerTransform = abilty.Owner.transform;

        for (int i = 0; i < selectedEnemies.Count; i++)
        {
            Transform enemyTransform = selectedEnemies[i].transform;
            Vector3 enemyDirection = enemyTransform.position - ownerTransform.position;
            Vector3 playerDirection = ownerTransform.position - enemyTransform.position;

            Debug.DrawRay(enemyTransform.position, (enemyTransform.right * 3), Color.blue, 1f);
            Debug.DrawRay(enemyTransform.position, playerDirection, Color.white, 1.5f);
            if (noBackward)
            {
                if (Vector3.Dot(backward, enemyDirection) > 0)
                {
                    finalTargetedEnemies.Remove(selectedEnemies[i]);
                }
            }
            if (noForward)
            {
                if (Vector3.Dot(forward, enemyDirection) > 0)
                {
                    finalTargetedEnemies.Remove(selectedEnemies[i]);
                }
            }
            if(noUp)
            {
                if (Vector3.Dot(up, enemyDirection) > 0)
                {
                    finalTargetedEnemies.Remove(selectedEnemies[i]);
                }
            }
            if (noDown)
            {
                if (Vector3.Dot(down, enemyDirection) > 0)
                {
                    finalTargetedEnemies.Remove(selectedEnemies[i]);
                }
            }

            if(noFacingAway)
            {
                if (Vector3.Dot(enemyTransform.right * -1, playerDirection) > 0)
                {
                    finalTargetedEnemies.Remove(selectedEnemies[i]);
                }
            }
            if (noFacingToward)
            {
                if (Vector3.Dot(enemyTransform.right, playerDirection) > 0)
                {
                    finalTargetedEnemies.Remove(selectedEnemies[i]);
                }
            }
        }
        if (selectedEnemies.Count > 0)
        {
            abilty.objectTargets.AddRange(finalTargetedEnemies);
        }
        else
        {

        }
        EndSubAbility();
    }
}
 
