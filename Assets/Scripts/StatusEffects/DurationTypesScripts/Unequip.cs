using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(fileName = "Unequip", menuName = "DurationTypes/Unequip", order = 1)]
public class Unequip : DurationType
{
    public Equipable item;
}
