﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public int curPartyIndx = 0;
    public List<Party> parties;
    public static Party curParty;
    public TestClass tc;
    private PCFactory pcFactory = new PCFactory();
    public List<PCDef> pcDefs;

    void Awake()
    {
        curParty = parties[curPartyIndx];
        InitializeParties();
    }
    public static void AddItemToCurrentParty(Item item)
    {
        curParty.items.Add(item);
    }

    public static void RemoveItemFromCurrentParty(Item item)
    {
        curParty.items.Remove(item);
    }

    private void InitializeParties()//ToDo: this will become a full featured setup with saving and loading once saving is implemented
    {
        foreach(PCDef pcDef in pcDefs)
        {
            parties[0].partyMembers.Add(pcFactory.CreatePCFromDef(pcDef));
        }

        foreach (Party p in parties)
        {
            foreach (PC pc in p.partyMembers)
            {
                pc.battler = GameObject.Instantiate(pc.battler);//TODO: make this into a PC factory
                pc.battlePC = pc.battler.GetComponent<BattlePC>();
                pc.equipment = new PCEquipment();
                //Debug.Log(pc.battlePC);
                //Debug.Log(pc.equipment);
                //Debug.Log(pc.equipment.battlePcToEquip);

                pc.equipment.battlePcToEquip = pc.battlePC;


                pc.battler.SetActive(false);
            }
        }
    }
}

[System.Serializable]
public class TestClass
{
    public int testInt;
    public string testString;
}

[System.Serializable]
public class Party
{
    public List<PC> partyMembers = new List<PC>();

    public int gp;

    public List<Item> items;

    public GameObject overworldCharacter;
}