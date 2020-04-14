using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSegmentStateMachine : MonoBehaviour
{
    private GameSegment curInteraction;

    private GameSegment defaultInteraction;

    private GameSegment startingInteraction;

    public void Awake()
    {
        GameSegment.gameStateMachine = this;
    }

    public void Start()
    {

    }

    public void Update()
    {
        curInteraction.UpdateGameSegment(); //this is the state machien basically     
    }

    public void ReturnToDefaultInteraction()
    {
        Debug.Log("return called" + defaultInteraction);
        curInteraction = defaultInteraction;
    }

    public void SetDefaultGameSegment(GameSegment newDefaultInteraction)
    {
        defaultInteraction = newDefaultInteraction;
        curInteraction = defaultInteraction; // there might be a more resonable time to set this at the start of entering a town but after setdefualtinteraction has been called.
    }

    public void SetCurrentGameSegment(GameSegment newCurrentInteraction)//these set interactions look to similar in code, easy to mix up when on pain killers. Change names?
    {
        curInteraction = newCurrentInteraction;
    }

}
