﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Levels
{
    none = 0,
    overworld =1,
    testDeasert =2,
    battle = 3,
}

public class ChangeLevelOnTouch : MonoBehaviour
{
    public string levelToTransitionTo;
    public Levels levelToTransTo;

    private LevelManager lm;
    public void Awake()
    {
        lm = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
        Debug.Log(gameObject.layer + "self layer");
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("trigger entered");
        //SceneManager.LoadScene(levelToTransitionTo);
        lm.LoadLevel(levelToTransTo);
    }

    public void Update()
    {
        //Debug.Log(gameObject.layer);
        //Debug.Log(gameObject.name);
    }
}