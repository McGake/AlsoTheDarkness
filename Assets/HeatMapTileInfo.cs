using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HeatMapTileInfo : MonoBehaviour
{
    public List<DangerZoneDef> dangerZones;
    public Tilemap heatMap;

    public DangerZoneDef GetCurZone(Vector3 curPos)
    {
        DangerZoneDef curZone;

        TileBase zoneTile = GetTileFromWorldPos(curPos);

        if(zoneTile == null)
        {
            return null;
        }

        curZone = GetZoneFromName(zoneTile.name);

        return curZone;
    }

    private TileBase GetTileFromWorldPos(Vector3 worldPos)
    {
        TileBase tileAtPos;

        Vector3Int tilePos = new Vector3Int();

        Debug.Log(worldPos + "world pos");

        Debug.Log(heatMap + " heat map");

        Debug.Log(heatMap.layoutGrid + "layoutgrid");

        tilePos = heatMap.layoutGrid.WorldToCell(worldPos);

        tileAtPos = heatMap.GetTile(tilePos);

        return tileAtPos;
    }

    private DangerZoneDef GetZoneFromName(string name)
    {
        foreach (DangerZoneDef dZD in dangerZones)
        {
            if(dZD.zoneName == name)
            {
                return dZD;
            }
        }
        return null;
    }

    


}

