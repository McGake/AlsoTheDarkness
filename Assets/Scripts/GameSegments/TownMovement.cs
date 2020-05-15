using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TownMovement : GameSegment
{
    private float speed = 2.0f;
    private Vector3 pos;
    private Vector3 squaresize = new Vector3(.5f, .5f, 0f);
    public Transform raycastHelper;
    private Animator anim;
    public SpriteRenderer sP;
    private TownMenuManager tMM;
    private GameSegmentStateMachine tSM;

    public Transform startPos;

    private LayerMask mask;


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

 
    }

    void Start()
    {
        gameStateMachine.SetCurrentGameSegment(this);
        gameStateMachine.SetDefaultGameSegment(this); //this is the defualt game mode for the town game scene. used by game state machine as default game mode to run when starting scene.
        pos = transform.position;
        anim = GetComponent<Animator>();
        //curState = TownStates.walking;
        tMM = GameObject.FindObjectOfType<TownMenuManager>();

        mask = LayerMask.GetMask("Default");

    }

    void OnEnable()
    {
         if(gameStateMachine != null)
        {
            gameStateMachine.SetCurrentGameSegment(this);
            gameStateMachine.SetDefaultGameSegment(this);
        }
        transform.position = startPos.position;
        pos = startPos.position;
    }

    public override GameSegment StartSegment(GameObject interactor)
    {
        //curInteraction = this;
        return (this);
    }

    public override void UpdateGameSegment()
    {
        WalkingBehavior();
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

    public void WalkModeInput()
    {        
        if(Input.GetButtonDown("A"))
        {
            CheckForInteractiveObject();
        }
    }   

    public void CheckForInteractiveObject()
    {
        RaycastHit2D rh2D = Physics2D.Raycast(raycastHelper.position, curDirection, .5f,mask);

        GameSegment gameSegmentScript;

        if (rh2D != false)
        {

            gameSegmentScript = rh2D.collider.GetComponent<GameSegment>();
            if (rh2D.collider.GetComponent<GameSegment>() != null)
            {
                Debug.Log(gameSegmentScript);
                
                gameSegmentScript.StartSegment(gameObject);
               // curState = TownStates.dialogue; //we need some kind of a call back or an enum return to determin what kind of interaction and therefor what state we should be in. 
            }
        }

    }

    private void WalkingBehavior()
    {
        WalkModeInput();

        anim.enabled = true;
        if (Input.GetAxis("Vertical") > 0.1f)
        {
            curDirection = Vector3.up;
            if (pos == transform.position)
            {
                SetMovement("walkingUp");
                if (Physics2D.Raycast(raycastHelper.position, Vector2.up, .5f,mask) == false)
                {
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
                if (Physics2D.Raycast(raycastHelper.position, Vector2.down, .5f,mask) == false)
                {
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
                sP.flipX = true;
                if (Physics2D.Raycast(raycastHelper.position, Vector2.right, .5f,mask) == false)
                {
                    pos += Vector3.right * .5f;
                }
            }
        }
        else if (Input.GetAxis("Horizontal") < -0.1f)
        {
            curDirection = curDirection = Vector3.left;
            if (pos == transform.position)
            {
                SetMovement("walkingRight");
                sP.flipX = false;
                if (Physics2D.Raycast(raycastHelper.position, Vector2.left, .5f,mask) == false)
                {
                    pos += Vector3.left * .5f;
                }
            }
        }
        else
        {
            anim.enabled = false;
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }



    private void StoreBehavior()
    {

    }

    public override void EndGameSegment()
    {

    }



}

public class GameSegment:MonoBehaviour
{
    /// <summary>
    /// This is set in awake by the game segment that should be the defualt of any given scene. Used by the GameSegmentStateMachine for which GameSegment to start with;
    /// </summary>
    public static GameSegment defaultSegment;


    /// <summary>
    /// this gives all GameSegments access to to the overall state machine for the game that calls their update. 
    /// </summary>
    public static GameSegmentStateMachine gameStateMachine; //this is currently set on awake so by the gssm itself. Maybe I should make that a singleton and maybe I should set this here on awake rather than there.

    public virtual GameSegment StartSegment(GameObject interactor)
    {
        GameSegment kay = new GameSegment();
        return kay;
    }

    /// <summary>
    /// This is called from the script acting as the state machine on update. Switches between various interacive scripts depending on current state.
    /// </summary>
    public virtual void UpdateGameSegment()
    {

    }

    public virtual void EndGameSegment()
    {

    }


}
