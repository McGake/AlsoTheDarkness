using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New PC", menuName = "character/PC")] 
public class PCDef : ScriptableObject
{
    public string displayName;
    public GameObject battler;

    public Sprite portrait;

    public AnimatorOverrideController battleAnimOverride;

    public AnimatorOverrideController overworldAnimOverride;

    public PCEquipment equipment;

}

[System.Serializable]
public class PC
{
    public string displayName;
    public GameObject battler;

    public BattlePC battlePC;

    public Sprite portrait;

    public AnimatorOverrideController battleAnimOverride;

    public AnimatorOverrideController overworldAnimOverride;

    public PCEquipment equipment = new PCEquipment();
}

public class PCEquipment
{
    public BattlePC battlePcToEquip = new BattlePC();

    public Armor armor = null;
    public Weapon weapon = null;
    public Ring ring = null;
    public Helmet helmet = null;

    public void Equip(Equipable equipable)
    {
        Equipable slotToSet = null;

        switch (equipable)
        {
            case Armor notUsed:
                armor = (Armor)SetNew(armor,equipable);
                break;
            case Weapon notUsed:
                weapon = (Weapon)SetNew(armor, equipable);
                break;
            case Ring notUsed:
                ring = (Ring)SetNew(armor, equipable);
                break;
            case Helmet notUsed:
                helmet = (Helmet)SetNew(armor, equipable);
                break;
        }

        SetNew(slotToSet, equipable);
    }

    private Equipable SetNew( Equipable slot, Equipable itemToSet)
    {
        if(slot != null)
        {
            PartyManager.AddItemToCurrentParty(slot);
        }
        PartyManager.RemoveItemFromCurrentParty(itemToSet);

        return itemToSet;
    }
}
