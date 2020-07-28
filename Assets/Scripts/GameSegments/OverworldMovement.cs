using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OverworldMovement : GameSegment
{
    private float speed = 2.0f;
    private Vector3 pos;
    private Vector3 squaresize = new Vector3(.5f, .5f, 0f);
    public Transform raycastHelper;
    private Animator anim;
    public SpriteRenderer sP;
    private TownMenuManager tMM;
    private GameSegmentStateMachine tSM;

    public List<PC> partyMembers;

    public HeatMapTileInfo heatMapTileInfo;

    public GameObject battleStarter;

    private string currentAnim = "walkingLeft";

    public LayerMask mask;

    public enum TownStates
    {
        none = 0,
        walking = 1,
        dialogue = 2,
        store = 3,
        cutscene = 4,
    }

    TownStates curState;

    void Awake()
    {
        pos = transform.position;
        oldPos = pos;
        anim = GetComponent<Animator>();
        GetPartyMembers();
        mask = 1;
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

    //public Iinteractable Interact(GameObject interactor)
    //{
    //    //curInteraction = this;
    //    return (this);
    //}

    public override void UpdateGameSegment()
    {
        WalkingBehavior();
        SwitchingCharacterDisplayBehavior();
    }

    void SetMovement(string newDir)
    {
        foreach (AnimatorControllerParameter parameter in anim.parameters)
        {
            anim.SetBool(parameter.name, false);
        }
        anim.SetBool(newDir, true);
    }

    Vector3 curDirection = Vector3.up;

    //public void WalkModeInput()
    //{

    //    if (Input.GetButtonDown("A"))
    //    {
    //        CheckForInteractiveObject();
    //    }
    //}

    //public void CheckForInteractiveObject()
    //{
    //    RaycastHit2D rh2D = Physics2D.Raycast(raycastHelper.position, curDirection, .5f);

    //    Iinteractable interactableScript;

    //    if (rh2D != false)
    //    {
    //        interactableScript = rh2D.collider.GetComponent<Iinteractable>();
    //        if (rh2D.collider.GetComponent<Iinteractable>() != null)
    //        {
    //            interactableScript.Interact(gameObject);
    //            // curState = TownStates.dialogue; //we need some kind of a call back or an enum return to determin what kind of interaction and therefor what state we should be in. 
    //        }
    //    }

    //}

    private void SwitchingCharacterDisplayBehavior()
    {
        if(Input.GetButtonDown("RBumper"))
        {
            SwitchDisplayCharacterUp();
        }
        if(Input.GetButtonDown("LBumper"))
        {
            SwitchDisplayChacterDown();
        }
    }

    private bool encounterChecked = false;

    private Vector3 oldPos;

    private void WalkingBehavior()
    {
       // WalkModeInput();

        anim.enabled = true;
        if (Input.GetAxis("Vertical") > 0.1f)
        {
       
            curDirection = Vector3.up;
            if (pos == transform.position)
            {
                SetMovement("walkingUp");
                currentAnim = "walkingUp";
                if (Physics2D.Raycast(raycastHelper.position, Vector2.up, .5f,mask) == false)
                {
                    oldPos = pos;
                    pos += Vector3.up * .5f;
                }
            }
        }
        else if (Input.GetAxis("Vertical") < -0.1f)
        {
            
            curDirection = Vector3.down;
            if (pos == transform.position)
            {
                SetMovement("walkingDown");
                currentAnim = "walkingDown";
                if (Physics2D.Raycast(raycastHelper.position, Vector2.down, .5f,mask) == false)
                {
                    oldPos = pos;
                    pos += Vector3.down * .5f;
                }
            }
        }
        else if (Input.GetAxis("Horizontal") > 0.1f)
        {
         
            curDirection = Vector3.right;
            if (pos == transform.position)
            {
                SetMovement("walkingLeft");
                currentAnim = "walkingLeft";
                sP.flipX = false;
                if (Physics2D.Raycast(raycastHelper.position, Vector2.right, .5f,mask) == false)
                {
                    oldPos = pos;
                    pos += Vector3.right * .5f;
                }
                else
                {
                    Debug.Log(Physics2D.Raycast(raycastHelper.position, Vector2.right, .5f, mask).collider.gameObject.layer);
                    Debug.Log(Physics2D.Raycast(raycastHelper.position, Vector2.right, .5f, mask).collider.gameObject.name);
                }
            }
        }
        else if (Input.GetAxis("Horizontal") < -0.1f)
        {
           
            curDirection = curDirection = Vector3.left;
            if (pos == transform.position)
            {
                SetMovement("walkingRight");
                currentAnim = "walkingRight";
                sP.flipX = true;
                if (Physics2D.Raycast(raycastHelper.position, Vector2.left, .5f, mask) == false)
                {
                    oldPos = pos;
                    pos += Vector3.left * .5f;
                }
            }
        }
        else
        {
            anim.enabled = false;
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);

        if(transform.position == pos)
        {
            if(encounterChecked == false)
            {
                encounterChecked = true;
                CheckForEncounter();
            }
        }
        else if(transform.position != pos && encounterChecked == true)
        {
            encounterChecked = false;
        }
       // Debug.Log("checked " + encounterChecked + " position equant: " + (transform.position != pos));
    }

    int curPartyMemberIndx = 0;
    private void SwitchDisplayCharacterUp()
    {
        curPartyMemberIndx++;
        if(curPartyMemberIndx >= partyMembers.Count)
        {
            curPartyMemberIndx = 0;
        }
        SetDisplayCharacter(curPartyMemberIndx);

    }

    private void SwitchDisplayChacterDown()
    {
        curPartyMemberIndx--;
        if (curPartyMemberIndx < 0)
        {
            curPartyMemberIndx = partyMembers.Count - 1;
        }
        SetDisplayCharacter(curPartyMemberIndx);
    }

    private void SetDisplayCharacter(int cPMI)
    {
        if (partyMembers[cPMI].overworldAnimOverride == null)
        {
            Debug.LogError("no overworld animation override found. Check your characters prefabs to make sure one is added");
        }
        anim.runtimeAnimatorController = partyMembers[cPMI].overworldAnimOverride;
        SetMovement(currentAnim);
        anim.enabled = true;
    }

    private void ContiuneFacingOnResume()//unity jettisons current animation on disable, this just restores the characters animation that she had when she was disabled
    {
        SetMovement(currentAnim);
        anim.enabled = true;
    }

    private void RestorePositionOnResume()
    {
        pos = oldPos;
        transform.position = oldPos;
    }

    private void GetPartyMembers()
    {
        partyMembers = PartyManager.curParty.partyMembers;
    }

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
                newBattle.pcsInBattle = partyMembers;
                battleStarter.GetComponent<BattleStarter>().SendBattleDef(newBattle);
                battleStarter.GetComponent<BattleStarter>().TurnOnBattleScene();
            }
        }
    }


}

