using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Stats
{
    public float maxHP;
    public float hP;

    public float maxMana;
    public float mana;

    public float speed;
    public float quickness;

    public float magicalPower;
    public float magicalSkill;

    public float physicalSkill;
    public float physicalPower;

    public float armor;

    public float exp;
    public float nextLevel;

    public int level;

    public float expLevelModifier;


    public Stats Copy()
    {
        Stats tempStats = new Stats();

        tempStats.maxHP = maxHP;
        tempStats.hP = hP;

        tempStats.maxMana = maxMana;
        tempStats.mana = mana;

        tempStats.speed = speed;
        tempStats.quickness = quickness;

        tempStats.magicalPower = magicalPower;
        tempStats.magicalSkill = magicalSkill;

        tempStats.physicalSkill = physicalSkill;
        tempStats.physicalPower = physicalPower;

        tempStats.armor = armor;

        tempStats.exp = exp;
        tempStats.nextLevel = nextLevel;

        tempStats.level = level;

        return tempStats;
    }

    public void AddOnLevelUp(Stats statsToAdd)
    {
        maxHP += statsToAdd.maxHP;
        hP = maxHP;

        maxMana += statsToAdd.maxMana;
        mana = maxMana;

        speed += statsToAdd.speed;
        quickness += statsToAdd.quickness;

        magicalPower += statsToAdd.magicalPower;
        magicalSkill += statsToAdd.magicalSkill;

        physicalSkill += statsToAdd.physicalSkill;
        physicalPower += statsToAdd.physicalPower;

        armor += statsToAdd.armor;
        nextLevel = nextLevel * statsToAdd.expLevelModifier;
    }
}