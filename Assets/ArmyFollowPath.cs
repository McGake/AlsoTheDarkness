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

    public WalkingView overworldView;

    public GridMovementUtilities oM;

    private void Awake()
    {
        TurnManager.RegisterTurnTakerAsLast(this);

    }

    void Start()
    {
        //seeker = GetComponent<Seeker>();
        //oM.ownerTransform = transform;
        
        //StartTurn();
    }

    private void OnEnable()
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


            direction = oM.CalculateDirectionWithTarget(path.vectorPath[currentWaypoint]);
            CheckForInteraction();

        }
    }

    void Update()
    {
        Walk();
        UpdateView();
    }

    void UpdateView()
    {
        overworldView.SetDirectionAnim(direction);
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

       RaycastHit2D rh2d = Physics2D.Raycast(((Vector2)transform.position + oM.raycastOffset), direction,.5f, mask);

        if (rh2d.transform != null)
        {
            Debug.Log("HitSOmething "  + rh2d.transform.name);
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
            else if (rh2d.transform.gameObject.layer == LayerMask.NameToLayer("Settlement"))
            {
                Debug.Log("Settlement detectedCOXOXOXOXOXOXOXOX");
                this.enabled = false;
                BattleArmy enemyBattleArmy = rh2d.transform.gameObject.GetComponent<BattleArmy>();
                BattleArmy thisBattleArmy = GetComponent<BattleArmy>();

                thisBattleArmy.DoAttackTurn(enemyBattleArmy);

                enemyBattleArmy.DoAttackTurn(thisBattleArmy);
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
        overworldView.SetDirectionAnim(direction);
        TurnManager.EndTurn(this);
    }
}



