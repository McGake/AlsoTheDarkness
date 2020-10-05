using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchSlotModel : UIMVC
{
    public GameObject itemOptionPrefab;
    private List<GameObject> itemOptions = new List<GameObject>();
    public MVCHelper panelToPause;
    public MVCHelper panelToUnpause;
    public override void MVCSetup(object obj)
    {
        foreach(GameObject iO in itemOptions)
        {
            iO.SetActive(false);
        }
        itemOptions.Clear();

        System.Type type = (System.Type)obj;
        Debug.Log("type is " + type);
        base.MVCSetup(obj);

        List<Item> items = PartyManager.curParty.items;

        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].GetType() == type)
            {
                GameObject itemOptionToAdd = BattlePooler.ProduceObject(itemOptionPrefab);
                itemOptionToAdd.GetComponentInChildren<TextMeshProUGUI>().text = items[i].listName;
                itemOptionToAdd.GetComponent<PrepEquipItem>().equipable = (Equipable)items[i];
                itemOptionToAdd.GetComponent<PrepEquipItem>().menuToPause = panelToPause;
                itemOptionToAdd.GetComponent<PrepEquipItem>().menuToUnpause = panelToUnpause;
                itemOptions.Add(itemOptionToAdd);
            }
        }
    }

    public override void MVCStart(object obj)
    {
        base.MVCStart(obj);
        mVCHelper.CallEvent(UIEvents.dataChanged, itemOptions);
    }
}
