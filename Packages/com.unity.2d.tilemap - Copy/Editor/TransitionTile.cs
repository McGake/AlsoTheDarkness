using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace UnityEditor.Tilemaps
{
    [CreateAssetMenu(fileName = "New Transition Tile", menuName = "Tiles/TransitionTile")]
    public class TransitionTile : Tile
    {
        public Levels level;
    }
}

public enum Levels
{
    None,
    Town1,
    Town2,
    City1,
    AbandonedTemple1,
}