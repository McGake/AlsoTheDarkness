using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Equipable/Weapon", order = 1)]
public class Weapon : Equipable
{
    public WeaponType weaponType;
}
