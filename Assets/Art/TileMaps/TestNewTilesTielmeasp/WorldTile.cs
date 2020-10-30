using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "New World Tile", menuName = "Tiles/World Tile")]
public class WorldTile : TileBase
{
    public TerrainTypes terrainType;

}
[Flags]
public enum NavigationTypes
{
    Nothing = 0,
    walk = (1<<0),
    sail = (1<<1),
    climb = (1<<2),
    raft = (1<<3),
      Everything = 0b1111
}

public enum TerrainTypes
{
    Nothing = 0,
    flat = (1 << 0),
    deepSea = (1 << 1),
    mountain = (1 << 2),
    rough = (1 << 3),
    shallowWater = (1 << 4),
    Everything = 0b1111
}
