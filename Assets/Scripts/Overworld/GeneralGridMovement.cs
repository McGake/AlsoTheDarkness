using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.XR.WSA.Input;

public class GeneralGridMovement : MonoBehaviour
{
    public GridMovementUtilities moveUtil;

    public WalkingView walkingView;

    public WalkingDirectionInput directionalInput;

    public float inputThreshold;
    [HideInInspector]
    public Vector2 direction;
    [HideInInspector]
    public Vector2 lastFacingDirection;

    private List<Action> subscribedStay = new List<Action>();

    private List<Action> subscribedArrival = new List<Action>();

    public void SubscribeToSquareStay(Action subMethod)
    {
        subscribedStay.Add(subMethod);
    }

    public void UnsubscribeToSquareStay(Action unsubMethod)
    {
        subscribedStay.Remove(unsubMethod);
    }

    public void SubscribeToSquareArrival(Action subMethod)
    {
        subscribedArrival.Add(subMethod);
    }

    public void UnsubscribeToSquareArrival(Action unsubMethod)
    {
        subscribedArrival.Remove(unsubMethod);
    }

    private void NotifyStaySubscribers()
    {
        for(int i = 0; i < subscribedStay.Count; i++)
        {
            subscribedStay[i]();
        }
    }

    private void NotifyArrivalSubscribers()
    {
        Debug.Log("notify arrival subscribers");
        for (int i = 0; i < subscribedArrival.Count; i++)
        {
            subscribedArrival[i]();
        }
    }

    private void Awake()
    {
        TurnManager.RegisterTurnTakerAsFirst(this);
    }

    private void OnEnable()
    {
        moveUtil.Setup(transform);
    }

    private void OnDisable()
    {

        UnsubscribeToSquareStay(CheckForNewSquareTarget);
    }

    private void OnDestroy()
    {
        TurnManager.UnregisterTurnTaker(this);
    }

    private void Start()
    {
        SubscribeToSquareStay(CheckForNewSquareTarget);
    }

    private void Update()
    {
        DoMovement();
    }

    bool firstArrival = true;
    private void DoMovement()
    {
        if (moveUtil.ArivedAtNextSquare())
        {
            CheckForNewSquareTarget();
            if (firstArrival)
            {
                firstArrival = false;
                NotifyArrivalSubscribers();
            }
            NotifyStaySubscribers();
        }

        moveUtil.MoveInDirection(direction);
    }


    private void CheckForNewSquareTarget()
    {
        //walkingView.PauseAnimation();

        if (directionalInput.IsThereDirectionalInput(inputThreshold))
        {
            direction = moveUtil.CalculateDirectionWithInput(directionalInput.DirectionalInput());
            lastFacingDirection = direction;

            walkingView.SetDirectionAnim(direction);

            if (!moveUtil.IsObstructionIn(direction))
            {
                moveUtil.CalculateNextSquare(direction);
                firstArrival = true;
            }
            else
            {
                direction = Vector2.zero;
            }
        }
        else
        {
            direction = Vector2.zero;
            walkingView.PauseAnimation();
        }
    }
}




