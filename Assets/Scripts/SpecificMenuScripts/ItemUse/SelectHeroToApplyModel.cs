using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectHeroToApplyModel : GeneralSelectionModel
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
        Debug.Log("number of party members " + PartyManager.curParty.partyMembers.Count);
        for (int i = 0; i < pcs.Count; i++)
        {
            if (selections.Count <= i)
            {
                selections.Add(GameObject.Instantiate(buttonTemplate));
            }

            PC pc = pcs[i];
            Debug.Log(pc.portrait);
            selections[i].GetComponentInChildren<Image>().sprite = pc.portrait;

            selections[i].GetComponentInChildren<TextMeshProUGUI>().text = pc.displayName + "\nHP:" + pc.battler.GetComponent<BattlePC>().stats.hP + "/" + pc.battler.GetComponent<BattlePC>().stats.maxHP + "\nMP:" + pc.battler.GetComponent<BattlePC>().stats.mana + "/" + pc.battler.GetComponent<BattlePC>().stats.maxMana;

            selections[i].GetComponentInChildren<UseItemFromMenu>().itemToUse = itemToUse;

            selections[i].GetComponentInChildren<UseItemFromMenu>().referencedPC = pc;
            //  selections[i].GetComponent<SelectPartyAssignMenu>().item = ;

            //  selections[i].GetComponentInChildren<TextMeshProUGUI>().text = item.listName;
        }

        //for (int j = itmes.Count - 1; j < selections.Count - 1; j++)
        //{
        //    Destroy(selections[j]);
        //    selections.RemoveAt(j);


        //}
    }

    public void RefreshHeros()
    {
        for (int i = 0; i < selections.Count; i++)
        {

            PC pc = pcs[i];
            Debug.Log(pc.portrait);
            selections[i].GetComponentInChildren<Image>().sprite = pc.portrait;

            selections[i].GetComponentInChildren<TextMeshProUGUI>().text = pc.displayName + "\nHP:" + pc.battler.GetComponent<BattlePC>().stats.hP + "/" + pc.battler.GetComponent<BattlePC>().stats.maxHP + "\nMP:" + pc.battler.GetComponent<BattlePC>().stats.mana + "/" + pc.battler.GetComponent<BattlePC>().stats.maxMana;

            selections[i].GetComponentInChildren<UseItemFromMenu>().referencedPC = pc;
            //  selections[i].GetComponent<SelectPartyAssignMenu>().item = ;

            //  selections[i].GetComponentInChildren<TextMeshProUGUI>().text = item.listName;
        }
    }


}