using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterChecker : MonoBehaviour
{
    public BattleStarter battleStarter;
    public HeatMapTileInfo heatMapTileInfo;
    public GeneralGridMovement generalGridMovement;

    private void OnEnable()
    {
        generalGridMovement.SubscribeToSquareArrival(CheckForEncounter);
    }

    private void OnDisable()
    {
        generalGridMovement.UnsubscribeToSquareArrival(CheckForEncounter);
    }


    private void CheckForEncounter()
    {
        Debug.Log("Get cur zone");
        DangerZoneDef dangerZone = heatMapTileInfo.GetCurZone(transform.position);
        BattleDef newBattle = new BattleDef();

        if (dangerZone == null)
        {
            Debug.Log("failed to find a danger zone at this location. Is your heat map painted? does your heat map string name correspond to the name of a danger zone?");
        }
        else
        {
            newBattle = GetComponent<EncounterRoller>().RollEncounter(dangerZone);
            if (newBattle != null)
            {
                //returnPosition = transform.position;
                Debug.Log("start new battle");
                newBattle.pcsInBattle = PartyManager.curParty.partyMembers;
                battleStarter.TurnOnBattleScene(newBattle);
            }
        }
    }
}
