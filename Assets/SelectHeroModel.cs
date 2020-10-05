using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectHeroModel : UIMVC
{
    public GameObject heroDisplayTemplate;

    private List<GameObject> heroDisplays = new List<GameObject>();

    public MVCHelper menuToOpen;

    public MVCHelper menuToClose;

    public override void MVCSetup(object obj)
    {
        base.MVCSetup(obj);
        foreach(GameObject hD in heroDisplays)
        {
            hD.SetActive(false);
        }
        heroDisplays.Clear();


        foreach(PC pc in PartyManager.curParty.partyMembers)
        {
            GameObject heroDisplayToCreate = BattlePooler.ProduceObject(heroDisplayTemplate);
            heroDisplayToCreate.GetComponentInChildren<Image>().sprite = pc.portrait;

            heroDisplayToCreate.GetComponentInChildren<TextMeshProUGUI>().text = pc.displayName + "\nHP:" + pc.battler.GetComponent<BattlePC>().stats.hP + "/" + pc.battler.GetComponent<BattlePC>().stats.maxHP + "\nMP:" + pc.battler.GetComponent<BattlePC>().stats.mana + "/" + pc.battler.GetComponent<BattlePC>().stats.maxMana;
            
            heroDisplayToCreate.GetComponent<SelectHeroForEquiping>().pc = pc;

            heroDisplayToCreate.GetComponent<SelectHeroForEquiping>().menuToClose = menuToClose;

            heroDisplayToCreate.GetComponent<SelectHeroForEquiping>().menuToOpen = menuToOpen;

            heroDisplays.Add(heroDisplayToCreate);
        }
    }

    public override void MVCStart(object obj)
    {
        base.MVCStart(obj);
        mVCHelper.CallEvent(UIEvents.dataChanged, heroDisplays);
    }
}
