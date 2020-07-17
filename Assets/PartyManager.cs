using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{




    public int curPartyIndx = 0;

    public List<Party> parties;

    public static Party curParty;

    public TestClass tc;

    // Start is called before the first frame update
    void Awake()
    {
        curParty = parties[curPartyIndx];
    }

    // Update is called once per frame
    public static void AddItemToCurrentParty(Item item)
    {
        curParty.items.Add(item);
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