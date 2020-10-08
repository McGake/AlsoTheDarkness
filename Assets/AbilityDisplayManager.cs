using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDisplayManager 
{
    private AbilityDisplayView view = new AbilityDisplayView();

    private GameObject curHero;
    private GameObject heroAbilitiesDisplay;

    public void SetupAbilityDisplayManager(GameObject curHero, GameObject heroAbilitiesDisplay)
    {
        this.heroAbilitiesDisplay = heroAbilitiesDisplay;
        view.ResetView();
        AddAbilityClustersToDictionary();
        AddAllAbilitybuttonsToListInProperOrder();
        this.curHero = curHero;
        view.PopulateHeroAbilityMenu(curHero);
    }

    void AddAbilityClustersToDictionary()
    {
        //this will eventually add all the ability button clusters open, righ trigger down, left trigger down, and both down
        view.abilityDisplayClusters.Add(heroAbilitiesDisplay.GetComponentInChildren<AbilityCluster>());
        view.curAbilityCluster = view.abilityDisplayClusters[0]; //temp code          
    }

    void AddAllAbilitybuttonsToListInProperOrder()
    {
        //this will eventually go through all the clusters to get all of the abilities
        AbilityCluster tempCluster = view.abilityDisplayClusters[0];
        foreach (AbilityButton aB in tempCluster.abilityButtons)
        {
            view.allAbilityButtons.Add(aB);
        }
    }



    private void Update()
    {
        view.UpdateAbilityMenu();
    }

    public void OnSwitchHero(GameObject curHero)
    {
        this.curHero = curHero;
        view.PopulateHeroAbilityMenu(curHero);
    }




    public void StartAbilityAtIndx(int indx)
    {
        Ability abilityToPass = null;
        foreach (Ability ab in view.curCombatAbilities)
        {
            if (ab == view.curAbilityCluster.abilityButtons[indx].ability)
            {
                abilityToPass = ab;
                break;
            }
        }
        if (abilityToPass == null)
        {
            Debug.LogError("We just called a null ability from the menu. did the hero get switched or the ability list change?");
        }
        BaseBattleActor curHeroBattleActor = curHero.GetComponent<BaseBattleActor>();
        if (AbilityManager.abManager.IsCharacterCurrentlyDoingAbility(curHero) == false)
        {
            curHeroBattleActor.DoAbility(abilityToPass);
        }
        //else just do nothing and continue
    }
}
