﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemUseModel : GeneralSelectionModel
{
    [HideInInspector]
    public Item itemToUse;

    private List<PC> pcs;

    public override List<GameObject> GetSelections()
    {
        CreateButtonsForItems();
        return selections;
    }

    public void CreateButtonsForItems()
    {
        pcs = PartyManager.curParty.partyMembers;
        for (int i = 0; i < pcs.Count; i++)
        {
            if (selections.Count <= i)
            {
                selections.Add(GameObject.Instantiate(buttonTemplate));
            }

            PC pc = pcs[i];
            selections[i].GetComponentInChildren<Image>().sprite = pc.portrait;

            selections[i].GetComponentInChildren<TextMeshProUGUI>().text = pc.name + "/n HP:" + pc.battlePC.stats.hP + "/" + pc.battlePC.stats.maxHP + "/n MP:" + pc.battlePC.stats.mana + "/" + pc.battlePC.stats.maxMana; 

          //  selections[i].GetComponent<SelectPartyAssignMenu>().item = ;

          //  selections[i].GetComponentInChildren<TextMeshProUGUI>().text = item.listName;
        }

        //for (int j = itmes.Count - 1; j < selections.Count - 1; j++)
        //{
        //    Destroy(selections[j]);
        //    selections.RemoveAt(j);


        //}
    }


    public void Update()
    {
        //if (selections.Count != PartyManager.curParty.items.Count)
        //{

        //}
    }


}


