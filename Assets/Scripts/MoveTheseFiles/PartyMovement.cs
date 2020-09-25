using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PartyMovement : MonoBehaviour
{
    HeatMapTileInfo heatMapTileInfo;

    GameObject battleStarter;
    public List<PC> partyMembers;

    public static bool inMenu = false;

    Vector2 direction = new Vector2(0f, 0f);
    private const float inputThreshold = 0.1f;

    public WalkingView overworldView;

    public GridMovementUtilities movementUtilities;
    void OnEnable()
    {      
        movementUtilities.nextCellCenter = (Vector2)transform.position + movementUtilities.nextCellCenter;
        StartCoroutine(GetPartyMembers());
        movementUtilities.ownerTransform = transform;
        returnPosition = transform.position;
        ContiuneFacingOnResume(); //this setsup the caracter switching. this is a temporary slightly hacky use of this function
        RestorePositionOnResume();
    }

    

    public void Start()
    {
        TurnManager.ManagerTest("WTF is happening");
        TurnManager.RegisterTurnTakerAsFirst(this);
    }


    void Update()
    {
        if (inMenu == false)
        {
            WalkingBehavior();
            SwitchingCharacterDisplayBehavior();
        }
    }

    private void UpdateView()
    {
        overworldView.SetDirectionAnim(direction);
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
        if (movementUtilities.ArivedAtNextSquare())
        {
            if (encounterChecked == false)
            {
                overworldView.PauseAnimation();
                encounterChecked = true;
                //CheckForEncounter();
            }

            if (Mathf.Abs(MultiInput.GetPrimaryDirection().x) > inputThreshold || Mathf.Abs(MultiInput.GetPrimaryDirection().y) > inputThreshold)
            {
                direction = movementUtilities.CalculateDirectionWithInput(MultiInput.GetPrimaryDirection());
                UpdateView();


                if (!movementUtilities.IsObstructionIn(direction))
                {
                    movementUtilities.CalculateNextSquare(direction);
                }
                else
                {
                    direction = Vector2.zero;
                }
            }
            else
            {
                direction = Vector2.zero;
                overworldView.PauseAnimation();
            }
            
        }
        else
        {
            encounterChecked = false;
        }

        movementUtilities.MoveInDirection(direction);

        if(MultiInput.GetAButtonDown())
        {
            DoCamping();
        }
    }

    private void DoCamping()
    {
        this.enabled = false;
        foreach (PC pc in PartyManager.curParty.partyMembers)
        {
            pc.battler.GetComponent<BattlePC>().stats.hP = pc.battler.GetComponent<BattlePC>().stats.maxHP;
        }
        returnPosition = transform.position;
        overworldView.PlayAnimationAndCallbackWhenDone("camp", EndCamping);

    }

    private void EndCamping()
    {
        TurnManager.EndTurn(this);
    }



    private void ContiuneFacingOnResume()//unity jettisons current animation on disable, this just restores the characters animation that she had when she was disabled
    {
 
    }

    private void RestorePositionOnResume()
    {
        movementUtilities.nextCellCenter = returnPosition + movementUtilities.raycastOffset;
        transform.position = returnPosition;
    }

    private IEnumerator GetPartyMembers()
    {
        if (PartyManager.curParty?.partyMembers != null)
        {
            partyMembers = PartyManager.curParty.partyMembers;
        }
        else
        {
            yield return 0;
            StartCoroutine(GetPartyMembers());
        }
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

