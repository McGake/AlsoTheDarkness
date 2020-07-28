using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager abManager;//semi singlton for access

    public void Awake()
    {
        abManager = this;
    }

    public List<Ability> abilitiesInUse = new List<Ability>();
    private List<Ability> abilitiesToRemove = new List<Ability>();

    void Update()
    {
        RemoveFinishedAbilities();
        RunAbilities();
        
    }

    private void RunAbilities()
    {
        //Debug.Log("pre count " + abilitiesInUse.Count);
        foreach (Ability aIU in abilitiesInUse)
        {
            if (aIU.IsAbilityOver())
            {
                //Debug.Log(aIU.DisplayName + " ability was over on: " + aIU.owner.name);
                //Debug.Log("number of abilities just in use " + abilitiesInUse.Count);
                abilitiesToRemove.Add(aIU);//I actually dont know if this will mess up the for each loop
            }
            else
            {
                aIU.AbilityStateMachine();
            }

        }
    }

    private void RemoveFinishedAbilities()
    {
        foreach (Ability aTR in abilitiesToRemove)
        {
            //Debug.Log(aTR.DisplayName + " was removed");
            abilitiesInUse.Remove(aTR);
            //Debug.Log("number of abilities in use after remove " + abilitiesInUse.Count);
        }

        //Debug.Log("post count " + abilitiesInUse.Count);
        abilitiesToRemove.Clear();
    }


    public void TurnOnAbility(Ability ab) //need to change this name vis a vis StartAbility a person reading this code would not know to look for doability rather than startability in the code
    {
        if (ab.useable == true)
        {
            Ability abilityToAdd;
            abilityToAdd = ab;

            abilitiesInUse.Add(abilityToAdd);
            ab.StartAbility();
        }
    }

    public bool IsCharacterCurrentlyDoingAbility(GameObject character)
    {
        foreach (Ability aIU in abilitiesInUse)
        {
            if (aIU.owner == character)
            {
                return true;
            }
        }
        return false;
    }

    public void StopAllAbilitiesFromCharacter(GameObject character)
    {
        foreach (Ability aIU in abilitiesInUse)
        {
            if (aIU.owner == character)
            {
                abilitiesToRemove.Add(aIU);
            }
        }
    }
}
