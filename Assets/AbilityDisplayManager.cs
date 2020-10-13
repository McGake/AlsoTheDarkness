using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AbilityDisplayManager 
{
    private AbilityDisplayView view = new AbilityDisplayView();

    private GameObject curHero;
    private GameObject heroAbilitiesDisplay;

    private int[] clusterFillOrder = new int[] { 1, 0, 2 };

    public void SetupAbilityDisplayManager(GameObject curHero, GameObject heroAbilitiesDisplay)
    {
        this.heroAbilitiesDisplay = heroAbilitiesDisplay;
        view.ResetView();
        AddAbilityClustersToDictionary();
        AddAllAbilityButtonsToListInProperOrder();
        this.curHero = curHero;
        view.PopulateHeroAbilityMenu(curHero);
    }

    void AddAbilityClustersToDictionary()
    {
        //this will eventually add all the ability button clusters open, righ trigger down, left trigger down, and both down
        view.abilityDisplayClusters.AddRange(heroAbilitiesDisplay.GetComponentsInChildren<AbilityCluster>());
        view.curAbilityCluster = view.abilityDisplayClusters[1]; //temp code          
    }

    void AddAllAbilityButtonsToListInProperOrder()
    {
        foreach (int indx in clusterFillOrder)
        {
            AbilityCluster tempCluster = view.abilityDisplayClusters[indx];

            foreach (AbilityButton aB in tempCluster.abilityButtons)
            {
                view.allAbilityButtons.Add(aB);
            }
        }
        
    }



    public void UpdateAbilityMenu()
    {
        view.UpdateAbilityMenu();
    }

    public void OnSwitchHero(GameObject curHero)
    {
        this.curHero = curHero;
        view.PopulateHeroAbilityMenu(curHero);
    }


    public void ChangeCurAbilityCluster(int clusterIndx)
    {
        view.curAbilityCluster = view.abilityDisplayClusters[clusterIndx];
        view.MakeNonIndxClustersTransparent(clusterIndx);
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
            //Debug.LogError("We just called a null ability from the menu. did the hero get switched or the ability list change?");
            return;
        }
        else if (AbilityManager.abManager.IsCharacterCurrentlyDoingAbility(curHero) == false)
        {
            BaseBattleActor curHeroBattleActor = curHero.GetComponent<BaseBattleActor>();
            Debug.Log("character was not doing other thing");
            curHeroBattleActor.DoAbility(abilityToPass);
        }
        //else just do nothing and continue
    }
}
