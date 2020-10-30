using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace UnityEditor.Tilemaps
{
    public class Test : MonoBehaviour
    {
        [CreateTileFromPalette]
        public static TileBase TestTile(Sprite sprite)
        { 
            WorldTile tile = (WorldTile)ScriptableObject.CreateInstance<Tile>();
            tile.sprite = sprite;
            tile.name = sprite.name;
            tile.color = Color.red;
            return tile;
        }
    }

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
    flat = (1 << 0),
    deepSea = (1 << 1),
    mountain = (1 << 2),
    rough = (1 << 3),
    shallowWater = (1 << 4),
    Everything = 0b1111
}
