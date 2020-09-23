using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class TurnManager
{

    private static List<MonoBehaviour> turnTakingScripts = new List<MonoBehaviour>();

    private static int curTurnTaker;

    public static void EndTurn(MonoBehaviour endingScript)
    {
        Debug.Log(endingScript + " ended########");
        endingScript.enabled = false;
        StartNextTurn();
    }

    private static void StartNextTurn()
    {
        curTurnTaker++;

        if(curTurnTaker >= turnTakingScripts.Count)
        {
            curTurnTaker = 0;
        }
        Debug.Log(turnTakingScripts[curTurnTaker].name + " started555");
        turnTakingScripts[curTurnTaker].enabled = true;
    }

    public static void RegisterTurnTakerAtPosition(MonoBehaviour scriptToRegister, int pos)
    {
        Debug.Log(scriptToRegister.name + "registered");
        turnTakingScripts.Insert(pos, scriptToRegister);
    }

    public static void RegisterTurnTakerAsFirst(MonoBehaviour scriptToRegister)
    {
        Debug.Log(scriptToRegister.name + "registered");
        turnTakingScripts.Insert(0, scriptToRegister);
    }

    public static void RegisterTurnTakerAsLast(MonoBehaviour scriptToRegister)
    {
        Debug.Log(scriptToRegister.name + "registered");
        turnTakingScripts.Add(scriptToRegister);
    }

    public static void ManagerTest(string test)
    {
        Debug.Log("manager test!!!! " + test);
    }
}
