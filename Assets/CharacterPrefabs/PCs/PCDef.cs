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
                weapon = (Weapon)SetNew(weapon, equipable);
                break;
            case Ring notUsed:
                ring = (Ring)SetNew(ring, equipable);
                break;
            case Helmet notUsed:
                helmet = (Helmet)SetNew(helmet, equipable);
                break;
        }

        SetNew(slotToSet, equipable);
    }

    private Equipable SetNew( Equipable slot, Equipable itemToSet)
    {
        if(slot != null)
        {
            RemoveEquipmentEffects(slot);
            PartyManager.AddItemToCurrentParty(slot);
        }
        AddEquipmentEffects(itemToSet);
        PartyManager.RemoveItemFromCurrentParty(itemToSet);

        return itemToSet;
    }

    private void AddEquipmentEffects(Equipable itemToSet)
    {
        foreach(Status status in itemToSet.statusesOnEquip)
        {
            Status statusInstance = status.CreateStatusInstance(battlePcToEquip.stats);
            itemToSet.statusesToUnequip.Add(statusInstance);
            battlePcToEquip.AddStatus(status);
        }
    }

    private void RemoveEquipmentEffects(Equipable itemToRemove)
    {
        foreach (Status status in itemToRemove.statusesToUnequip)
        {
            battlePcToEquip.FinishStatus(status);
        }
        itemToRemove.statusesToUnequip.Clear();
    }
}
