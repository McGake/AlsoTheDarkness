using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PartyMovement : GameSegment
{
    public Animator anim;
    public SpriteRenderer sP;
    private TownMenuManager tMM;
    private GameManager tSM;

    public List<PC> partyMembers;

    public HeatMapTileInfo heatMapTileInfo;

    public GameObject battleStarter;

    public static bool inMenu = false;

    Vector2 direction = new Vector2(0f, 0f);
    private const float inputThreshold = 0.1f;

    public OverworldView overworldView;

    public OverworldMovement oM;


    void Awake()
    {
        oM.nextCellCenter = transform.position;
        GetPartyMembers();
        oM.ownerTransform = transform;
    }

    void OnEnable()
    {
        if (gameStateMachine != null)
        {
            gameStateMachine.SetCurrentGameSegment(this);
            gameStateMachine.SetDefaultGameSegment(this);
        }
        ContiuneFacingOnResume(); //this setsup the caracter switching. this is a temporary slightly hacky use of this function
        RestorePositionOnResume();
    }

    public void Start()
    {
            gameStateMachine.SetCurrentGameSegment(this);
            gameStateMachine.SetDefaultGameSegment(this);
    }


    public override void UpdateGameSegment()
    {
        if (inMenu == false)
        {
            WalkingBehavior();
            SwitchingCharacterDisplayBehavior();
            UpdateView();
        }
    }

    private void UpdateView()
    {
        overworldView.SetAnimDirection(direction);
    }

    private void SwitchingCharacterDisplayBehavior()
    {
        if(Input.GetButtonDown("RBumper"))
        {
            overworldView.SwitchDisplayCharacterUp(partyMembers);
        }
        if(Input.GetButtonDown("LBumper"))
        {
            overworldView.SwitchDisplayChacterDown(partyMembers);
        }
    }

    private bool encounterChecked = false;



    private void WalkingBehavior()
    {
        if (oM.ArivedAtNextSquare())
        {
            if (encounterChecked == false)
            {
                encounterChecked = true;
                CheckForEncounter();
            }

            if (Mathf.Abs(MultiInput.GetPrimaryDirection().x) > inputThreshold || Mathf.Abs(MultiInput.GetPrimaryDirection().y) > inputThreshold)
            {
                direction = oM.CalculateDirectionWithInput(MultiInput.GetPrimaryDirection());
                oM.CalculateNextSquare(direction);
            }
            else
            {
                direction = Vector2.zero;
            }
        }
        else
        {
            encounterChecked = false;
        }

        oM.MoveInDirection(direction);
    }



    private void ContiuneFacingOnResume()//unity jettisons current animation on disable, this just restores the characters animation that she had when she was disabled
    {
 
    }

    private void RestorePositionOnResume()
    {
        oM.nextCellCenter = returnPosition;
        transform.position = returnPosition;
    }

    private void GetPartyMembers()
    {
        partyMembers = PartyManager.curParty.partyMembers;
    }

    private Vector2 returnPosition;

    private void CheckForEncounter()
    {
        DangerZoneDef dangerZone = heatMapTileInfo.GetCurZone(transform.position);
        BattleDef newBattle = new BattleDef();

        if(dangerZone == null)
        {
            Debug.LogError("failed to find a danger zone at this location. Is your heat map painted? does your heat map string name correspond to the name of a danger zone?");
        }
        else
        {
            newBattle = GetComponent<EncounterRoller>().RollEncounter(dangerZone);
            if(newBattle != null)
            {
                returnPosition = transform.position;
                newBattle.pcsInBattle = partyMembers;
                battleStarter.GetComponent<BattleStarter>().TurnOnBattleScene(newBattle);
            }
        }
    }


}

