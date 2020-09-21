using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ArmyFollowPath : MonoBehaviour
{
    public Transform target; //This will be assigned by a decision making script

    public float waypointArrivalThreshold;

    Path path;

    int currentWaypoint = 0;
    private Seeker seeker;

    public int maxMovement;
    private int movmentSoFar = 0;

    public Vector2 direction;

    public OverworldView overworldView;

    public OverworldMovement oM;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        oM.ownerTransform = transform;
        StartTurn();
    }

    void StartTurn()
    {
        movmentSoFar = 0;
        seeker.StartPath(transform.position, target.position, PathFinished);
    }

    void PathFinished(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 1;
            oM.nextCellCenter = path.vectorPath[currentWaypoint];

        }
    }

    void Update()
    {
        Walk();
        UpdateView();
    }

    void UpdateView()
    {
        overworldView.SetAnimDirection(direction);
    }
    void Walk()
    {
        if(path == null)
        {
            return;
        }

        float wpDistance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if(wpDistance <= waypointArrivalThreshold)
        {
            transform.position = path.vectorPath[currentWaypoint];
            currentWaypoint++;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            EndTurn();
            return;
        }

        direction = oM.CalculateDirectionWithTarget(path.vectorPath[currentWaypoint]);

        if (oM.ArivedAtNextSquare())
        {
            IncrementMovement();
            oM.CalculateNextSquare(direction);
            CheckForInteraction();
        }

        if(MaxMovementReached())
        {
            EndTurn();
        }

        oM.MoveInDirection(direction);

    }

    void IncrementMovement()
    {
        movmentSoFar++;
    }
    public LayerMask mask;

    public LayerMask playerParty;

    public LayerMask settlement;

    public LayerMask army;

    public DangerZoneDef tempArmyDangerZone; //This is temporary untill the actual army threat level system gets installed wich will pick from a range of dangerzones probably

    public BattleStarter battleStarter;
    void CheckForInteraction()
    {

        Debug.Log("check for interation");
       RaycastHit2D rh2d = Physics2D.Raycast(transform.position, direction,.5f, mask);

        if (rh2d.transform != null)
        {
            Debug.Log("not null");
            if (rh2d.transform.gameObject.layer == LayerMask.NameToLayer("BattlePC"))
            {
                Debug.Log("reached battle PC!!!!!");

                BattleDef newBattle = GetComponent<EncounterRoller>().RollEncounter(tempArmyDangerZone);
                if (newBattle != null)
                {
                    //returnPosition = transform.position;
                    newBattle.pcsInBattle = PartyManager.curParty.partyMembers;
                    battleStarter.GetComponent<BattleStarter>().TurnOnBattleScene(newBattle);
                }

                    EndTurn();
                //Start special battle
            }
            else if (rh2d.transform.gameObject.layer == settlement.value)
            {
                EndTurn();
                //Resolve 1 round of strategic battle
            }
            else if (rh2d.transform.gameObject.layer ==army.value)
            {
                EndTurn();
                //Resolve 1 round of strategic battle
            }

        }
    }
    bool MaxMovementReached()
    {
        if(movmentSoFar >= maxMovement)
        {
            return true;
        }

        return false;
    }

    void EndTurn()
    {
        Debug.Log("TURN ENDED");
        direction = Vector2.zero;
        overworldView.SetAnimDirection(direction);
        enabled = false;
    }
}

[System.Serializable]
public class OverworldMovement
{
    public Tilemap terrainMap;
    public float speed;
    public float pointArrivalThreshold;
    const float tileSize = .5f;
    public Transform ownerTransform;
    public Vector2? nextCellCenter = null;

    public Vector2 CalculateDirectionWithTarget(Vector2 targetPoint )
    {
        return (targetPoint - (Vector2)ownerTransform.position).normalized;
    }

    public Vector2 CalculateDirectionWithInput(Vector2 input)
    {
        if(Mathf.Abs(input.x)>Mathf.Abs(input.y))
        {
            return new Vector2(input.x, 0f).normalized;
        }

        return new Vector2(0f, input.y).normalized;
    }

    

    public void CalculateNextSquare(Vector2 direction)
    {
        
        Vector2 tempCellCenter = (Vector2)ownerTransform.position + (direction * tileSize);
        Vector3Int nextCellCoords = terrainMap.WorldToCell(tempCellCenter);
        //This makes sure that the next cell position is exact
        nextCellCenter = terrainMap.GetCellCenterWorld(nextCellCoords);
        Debug.Log("next cell center " + nextCellCenter);
        //Debug.Log("calcultate next square " + transform.position + " " + nextCellCenter + " " + tempCellCenter + " " + (direction * tileSize) + " " + direction + " " + tileSize);
    }

    public bool ArivedAtNextSquare()
    {

        float distance = Vector2.Distance((Vector2)ownerTransform.position, (Vector2)nextCellCenter);
        if (Mathf.Abs(distance) <= pointArrivalThreshold)
        {
            return true;
        }
        return false;
    }

    public void MoveInDirection(Vector2 dir)
    {
        Vector2 translation = dir * speed * Time.deltaTime;
        ownerTransform.Translate(translation);

    }
}
