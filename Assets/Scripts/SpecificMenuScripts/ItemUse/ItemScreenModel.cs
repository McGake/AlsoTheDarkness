using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemScreenModel : GeneralSelectionModel
{
    List<Item> itmes;

    public SelectHeroToApplyModel heroApplyScreen;

    public GameObject itemSelectionScreen;

    public override List<GameObject> GetSelections()
    {
        CreateButtonsForItems();
        return selections;
    }

    public void CreateButtonsForItems()
    {
        
        itmes = PartyManager.curParty.items;
        for(int i =0; i < itmes.Count; i++)
        {
            if(selections.Count <= i)
            {
                selections.Add(GameObject.Instantiate(buttonTemplate));
            }

            Item item = itmes[i];

            selections[i].GetComponent<SelectPartyAssignMenu>().item = item;
            
            selections[i].GetComponentInChildren<TextMeshProUGUI>().text = item.listName;

            selections[i].GetComponentInChildren<SelectPartyAssignMenu>().item = item;

            selections[i].GetComponentInChildren<SelectPartyAssignMenu>().uiToOpen = heroApplyScreen;

            selections[i].GetComponentInChildren<SelectPartyAssignMenu>().uiToClose = itemSelectionScreen;
        }

        for(int j = itmes.Count; j < selections.Count; j++ )
        {
            GameObject tempObjectRef = selections[j];               
            selections.RemoveAt(j);
            Destroy(tempObjectRef);
        }
    }


    public void Update()
    {
        if (selections.Count != PartyManager.curParty.items.Count)
        {

        }
    }


}
