﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTriggerEnter : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided with " + collision.gameObject.name);
    }
}