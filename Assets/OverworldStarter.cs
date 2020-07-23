using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldStarter : MonoBehaviour
{

    public Canvas mainMenu;

    public Camera overworldCamera;
    public void OnEnable()
    {
        mainMenu.worldCamera = overworldCamera;
    }
}