﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour
{
    public static bool paused = false;

    public List<MonoBehaviour> localScriptsToPause;

    private static List<MonoBehaviour> scriptsToPause = new List<MonoBehaviour>();

    private void Awake()
    {
        scriptsToPause.AddRange(localScriptsToPause);
    }


    public static void PauseGame()
    {
        foreach(MonoBehaviour m in scriptsToPause)
        {
            m.enabled = false;
        }
    }

    public static void UnpauseGame()
    {
        foreach (MonoBehaviour m in scriptsToPause)
        {
            m.enabled = true;
        }
    }

    private void OnDestroy()
    {
        foreach (MonoBehaviour m in localScriptsToPause)
        {
            scriptsToPause.Remove(m);
        }
    }
}
