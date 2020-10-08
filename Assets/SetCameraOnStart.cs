using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraOnStart : MonoBehaviour
{
    public LevelManager lm;

    public Canvas canvas;

    public void Start()
    {
        canvas.worldCamera = lm.curLevel.GetComponentInChildren<Camera>();
    }
}
