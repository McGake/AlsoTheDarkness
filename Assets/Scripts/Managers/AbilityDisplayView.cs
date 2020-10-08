using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDisplayView
{
    public List<AbilityButton> allAbilityButtons = new List<AbilityButton>();
    public List<AbilityCluster> abilityDisplayClusters = new List<AbilityCluster>();

    public AbilityCluster curAbilityCluster;
    public List<Ability> curCombatAbilities;

    public void ResetView()
    {
        allAbilityButtons.Clear();
        abilityDisplayClusters.Clear();
        //curCombatAbilities.Clear();
    }

    public void PopulateHeroAbilityMenu(GameObject curHero)
    {
        GetBattleAbilities(curHero);
        SetDisplayToNewAbilities();
    }

    public void SetDisplayToNewAbilities()
    {
        for (int i = 0; i < curCombatAbilities.Count; i++)
        {
            allAbilityButtons[i].uIButton.SetActive(true);
            allAbilityButtons[i].abilityView = allAbilityButtons[i].uIButton.GetComponentInChildren<AbilityView>();
            allAbilityButtons[i].abilityView.SetButtonLabel(curCombatAbilities[i].DisplayName);
            allAbilityButtons[i].abilityView.SetUsesLeft((curCombatAbilities[i].maxUses - curCombatAbilities[i].uses).ToString());
            allAbilityButtons[i].ability = curCombatAbilities[i];
            //put other image changes here when you have them
        }

        for (int i = curCombatAbilities.Count; i < allAbilityButtons.Count; i++)
        {
            allAbilityButtons[i].uIButton.SetActive(false);
        }
    }

    public void GetBattleAbilities(GameObject curHero)
    {
        List<Ability> allAbilitiesOnPC = curHero.GetComponent<BattlePC>().abilities;
        curCombatAbilities = Ability.NewRetrunOnlyAbilitiesOfContext(UsableContexts.battleAbilityMenu, allAbilitiesOnPC);
    }

    public void UpdateAbilityMenu()//ToDo: move setting the actual circle and such to the AbilityView. Time till cooldown end should probably still be calculated here or perhaps on the ability itself
    {
        AbilityButton curButton;
        for (int i = 0; i < abilityDisplayClusters.Count; i++)
        {
            for (int j = 0; j < abilityDisplayClusters[i].abilityButtons.Count; j++)
            {
                curButton = abilityDisplayClusters[i].abilityButtons[j];
                if (curButton.uIButton.activeSelf)
                {
                    curButton.abilityView.UpdateAbility(curButton.ability);
                }
            }
        }
    }
}
