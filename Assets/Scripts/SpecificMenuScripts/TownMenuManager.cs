using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class TownMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
  
    public GameObject dialoguePanel;
    public GameObject storePanel;

    public GameObject itemUIButton;
    public GameObject initialPanel;
    public Button initialPanelFirstButton;
    public GameObject buyPanel;
    public GameObject sellPanel;
    public GameObject confirmationPanel;

    private GameObject curPanel;
    
    EventSystem es;    

    public List<GameObject> itemUIButtons;

    GameSegment gameSegment;

 

    public void DisplayDialoguePanel(string txt)
    {
        Debug.Log("dialgue panel set to ture");
        dialoguePanel.SetActive(true);
        dialoguePanel.GetComponentInChildren<TextMeshProUGUI>().text = txt;
    }

    public bool AdvanceDialguePanel()//eventually this will advance through multi part texts or close if at end of text.
    {
        CloseDialoguePanel();
        return false;
    }

    public void CloseDialoguePanel()
    {
        dialoguePanel.SetActive(false);
    }

    public void OpenStore(GameSegment inter)
    {
        storePanel.SetActive(true);
        initialPanel.SetActive(true);
        curPanel = initialPanel;
        gameSegment = inter;
        initialPanelFirstButton.Select();


    }

    public void GenerateBuyingList(List<ShopItem> itemsToGenerate)
    {
        int uiButtonIndx = 0;
        foreach(ShopItem sI in itemsToGenerate)
        {
            if(uiButtonIndx >= itemUIButtons.Count)
            {
                Debug.LogError("more items than store slots!");
                break;
            }

            itemUIButtons[uiButtonIndx].transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = sI.itemDef.name;
            itemUIButtons[uiButtonIndx].transform.Find("PriceText").GetComponent<TextMeshProUGUI>().text = sI.price.ToString();
            itemUIButtons[uiButtonIndx].SetActive(true);
            uiButtonIndx++;
        }

        itemUIButtons[0].GetComponent<Button>().Select();
    }

    public void CloseStore()
    {
        storePanel.SetActive(false);
        gameSegment.EndGameSegment();
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        curPanel = panel;
    }

    public void CloseInitialPanel()
    {
        initialPanel.SetActive(false);

    }
    public void OpenBuyPanel()
    {
        buyPanel.SetActive(true);
        curPanel = buyPanel;
    }

    public void HideAllPanels()
    {
        storePanel.SetActive(false);
        buyPanel.SetActive(false);
        initialPanel.SetActive(false);
        sellPanel.SetActive(false);
        confirmationPanel.SetActive(false);
    }

    public void BackOutOfCurrentTownMenu()//this will need to be changed to account for more levels of panels.
    {
        if(curPanel == initialPanel)
        {
            CloseStore();
        }
        else
        {
            curPanel.SetActive(false);
            initialPanel.SetActive(true);
            curPanel = initialPanel;
            initialPanelFirstButton.Select();
        }
        
    }

}
