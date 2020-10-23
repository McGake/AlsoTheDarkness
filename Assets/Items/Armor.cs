using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "ScriptableObjects/Armor", order = 1)]
public class Armor : Equipable
{
    public ArmorType armorType;
}
