using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager abManager;
    public List<Ability> abilitiesInUse = new List<Ability>();
    private List<Ability> abilitiesToRemove = new List<Ability>();
    private List<Ability> abilitiesToCooldown = new List<Ability>();

    public void Awake()
    {
        abManager = this;
    }

    void Update()
    {
        RemoveFinishedAbilities();
        RunAbilities();
        UpdateCooldowns();
    }
    #region Internal

    private void RemoveFinishedAbilities()
    {
        foreach (Ability aTR in abilitiesToRemove)
        {
            abilitiesInUse.Remove(aTR);
        }
        abilitiesToRemove.Clear();
    }

    private void RunAbilities()
    {
        foreach (Ability ability in abilitiesInUse)
        {
            if (ability.IsAbilityOver())
            {
                abilitiesToRemove.Add(ability);
            }
            else
            {
                ability.RunSubAbility();
            }
        }
    }

    private void UpdateCooldowns()
    {
        for (int i = 0; i < abilitiesToCooldown.Count; i++)
        {
            abilitiesToCooldown[i].UpdateCooldown();
        }
    }
    #endregion Internal

    #region ExternalStartStopAbilities

    public void TurnOnAbility(Ability ab)
    {
        if (ab.IsUseable() == true)
        {
            ab.KickOffAbility();
            abilitiesInUse.Add(ab);
        }
    }

    public void RegisterAbilityForCooldown(Ability ab)
    {
        abilitiesToCooldown.Add(ab);
    }

    public void UnregisterAbilityForCooldown(Ability ab)
    {
        abilitiesToCooldown.Remove(ab);
    }

    public void StopAllAbilitiesFromCharacter(GameObject character)
    {
        foreach (Ability aIU in abilitiesInUse)
        {
            if (aIU.Owner == character)
            {
                abilitiesToRemove.Add(aIU);
            }
        }
    }
    #endregion ExternalStartStopAbilities

    #region Utilities
    public bool IsCharacterCurrentlyDoingAbility(GameObject character)
    {
        foreach (Ability aIU in abilitiesInUse)
        {
            if (aIU.Owner == character)
            {
                return true;
            }
        }
        return false;
    }
    #endregion Utilities
}
