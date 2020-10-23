using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectSlotModel : UIMVC
{
    // Start is called before the first frame update

    private PC pcToEquip;

    public GameObject itemSlotPrefab;

    private List<GameObject> itemSlots = new List<GameObject>();

    public MVCHelper panelToOpen;
    public MVCHelper panelToPause;
public override void MVCSetup(object obj)
    {

    }

    public override void MVCStart(object obj)
    {
        base.MVCStart(obj);
        if (obj is PC)
        {
            pcToEquip = (PC)obj;
            CreateSlotMenu();
        }
        else if (obj is Equipable)
        {
            pcToEquip.equipment.Equip((Equipable)obj);
            CreateSlotMenu();
        }
        mVCHelper.CallEvent(UIEvents.dataChanged, itemSlots);
    }

    private void CreateSlotMenu()
    {
        foreach(GameObject iS in itemSlots)
        {
            iS.SetActive(false);
        }
        itemSlots.Clear();

        GameObject itemSlotToFill = BattlePooler.ProduceObject(itemSlotPrefab);
        itemSlotToFill.transform.Find("Type").GetComponent<TextMeshProUGUI>().text = "Armor";
        itemSlotToFill.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = pcToEquip.equipment.armor?.listName;
        itemSlotToFill.GetComponent<StartWithEquipmentType>().slotType = typeof(Armor);
        itemSlotToFill.GetComponent<StartWithEquipmentType>().panelToOpen = panelToOpen;
        itemSlotToFill.GetComponent<StartWithEquipmentType>().panelToPause = panelToPause;
        itemSlots.Add(itemSlotToFill);


        itemSlotToFill = BattlePooler.ProduceObject(itemSlotPrefab);
        itemSlotToFill.transform.Find("Type").GetComponent<TextMeshProUGUI>().text = "Weapon";
        itemSlotToFill.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = pcToEquip.equipment.weapon?.listName;
        itemSlotToFill.GetComponent<StartWithEquipmentType>().slotType = typeof(Weapon);
        itemSlotToFill.GetComponent<StartWithEquipmentType>().panelToOpen = panelToOpen;
        itemSlotToFill.GetComponent<StartWithEquipmentType>().panelToPause = panelToPause;
        itemSlots.Add(itemSlotToFill);

        itemSlotToFill = BattlePooler.ProduceObject(itemSlotPrefab);
        itemSlotToFill.transform.Find("Type").GetComponent<TextMeshProUGUI>().text = "Ring";
        itemSlotToFill.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = pcToEquip.equipment.ring?.listName;
        itemSlotToFill.GetComponent<StartWithEquipmentType>().slotType = typeof(Ring);
        itemSlotToFill.GetComponent<StartWithEquipmentType>().panelToOpen = panelToOpen;
        itemSlotToFill.GetComponent<StartWithEquipmentType>().panelToPause = panelToPause;
        itemSlots.Add(itemSlotToFill);


        itemSlotToFill = BattlePooler.ProduceObject(itemSlotPrefab);
        itemSlotToFill.transform.Find("Type").GetComponent<TextMeshProUGUI>().text = "Hemet";
        itemSlotToFill.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = pcToEquip.equipment.helmet?.listName;
        itemSlotToFill.GetComponent<StartWithEquipmentType>().slotType = typeof(Helmet);
        itemSlotToFill.GetComponent<StartWithEquipmentType>().panelToOpen = panelToOpen;
        itemSlotToFill.GetComponent<StartWithEquipmentType>().panelToPause = panelToPause;
        itemSlots.Add(itemSlotToFill);



    }

}
