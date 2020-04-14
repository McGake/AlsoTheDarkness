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
        RunAbilities();
        RemoveFinishedAbilities();
    }

    private void RunAbilities()
    {
        //Debug.Log("pre count " + abilitiesInUse.Count);
        foreach (Ability aIU in abilitiesInUse)
        {
            aIU.AbilityStateMachine();
            if (aIU.IsAbilityOver())
            {
                Debug.Log("ability was over");
                Debug.Log("number of abilities just in use " + abilitiesInUse.Count);
                abilitiesToRemove.Add(aIU);//I actually dont know if this will mess up the for each loop
            }
        }
    }

    private void RemoveFinishedAbilities()
    {
        foreach (Ability aTR in abilitiesToRemove)
        {
            Debug.Log(aTR.DisplayName + " was removed");
            abilitiesInUse.Remove(aTR);
            Debug.Log("number of abilities in use after remove " + abilitiesInUse.Count);
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

            Debug.Log("ability " + ab.DisplayName + " added by " + ab.owner.name);
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
}
