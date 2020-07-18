using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Animations;

public class SellModel : MonoBehaviour, ISelectionModel
{
    public List<GameObject> selections;

    public GameObject buttonTemplate;

    List<Item> sellItems;


    public List<GameObject> GetSelections()
    {
        CreateButtonsForItems();
        return selections;
    }

    public void CreateButtonsForItems()
    {
        sellItems = PartyManager.curParty.items;
        for(int i =0; i < sellItems.Count; i++)
        {
            if(selections.Count <= i)
            {
                selections.Add(GameObject.Instantiate(buttonTemplate));
            }

            Item curSellItem = sellItems[i];

            selections[i].GetComponent<Sell>().sellItem = curSellItem;
            
            selections[i].GetComponentInChildren<TextMeshProUGUI>().text = curSellItem.listName + " " + curSellItem.sellPrice;            
        }

        for(int j = sellItems.Count-1; j < selections.Count-1; j++ )
        {
            Destroy(selections[j]);
            selections.RemoveAt(j);
            
            
        }
    }


    public void Update()
    {
        if (selections.Count != PartyManager.curParty.items.Count)
        {

        }
    }


}
