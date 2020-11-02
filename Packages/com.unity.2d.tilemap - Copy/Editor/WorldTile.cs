using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace UnityEditor.Tilemaps
{
    public class WorldTile : Tile
    {
        public TerrainTypes terrainType;
    }
}


[Flags]
public enum NavigationTypes
{
    Nothing = 0,
    walk = (1 << 0),
    sail = (1 << 1),
    climb = (1 << 2),
    raft = (1 << 3),
    Everything = 0b1111
}

[Flags]
public enum TerrainTypes
{
    Nothing = 0,
    grassland = (1<<0),
    desert = (1<<1),
    mountain = (1<<2),
    foothills = (1<<3),
    iceFields = (1<<4),
    deadlands = (1<<5),
    shallowWater = (1<<6),
    deepWater = (1<<7),
    road = (1<<8),
    forrest = (1<<9),
    Everything = 0b1111
}

public enum DifficultyTypes
{
    Nothing = 0,
    prepared = (1<<0),
    smooth = (1 << 2),
    rough = (1 << 3),
    difficult= (1<<4),
    broken = (1<<5),
    impossible= (1<<6),
    Everything = 0b1111
}

public enum DangerTypes
{
    Nothing = 0,
    poison = (1 << 0),
    burning = (1<<1),
    splitting = (1<<2),
    sinking = (1<<3),
    Everything = 0b1111
}
