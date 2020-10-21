using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusView : UIMVC
{
    PC pc;
    public Image portrait;
    public TextMeshProUGUI name;
    public TextMeshProUGUI lvLHealthMP;
    public TextMeshProUGUI statsNames;
    public TextMeshProUGUI statsModNumbers;
    public TextMeshProUGUI statsBaseNumbers;

    BattleStats pcStats;
    public override void MVCSetup(object obj)
    {
        mVCHelper.Subscribe(UIEvents.dataChanged, DisplayStats);
    }

    private void DisplayStats(object obj)
    {
        pc = (PC)obj;
        portrait.sprite = pc.portrait;
        name.text = pc.displayName;
        lvLHealthMP.text = pc.displayName + "\nHP:" + pc.battler.GetComponent<BattlePC>().stats.modified.hP + "/" + pc.battler.GetComponent<BattlePC>().stats.modified.maxHP + "\nMP:" + pc.battler.GetComponent<BattlePC>().stats.modified.mana + "/" + pc.battler.GetComponent<BattlePC>().stats.modified.maxMana;

        pcStats = pc.battler.GetComponent<BattlePC>().stats;

        statsNames.text = "Speed:\n" + "Quickness:\n" + "Mg Power:\n" + "Mg Skill:\n" + "Phys Power:\n" + "Phys Skill:\n" + "Armor:\n" + "Exp:\n" + "NextLvl:\n";
        statsModNumbers.text = pcStats.modified.speed + "\n" + pcStats.modified.quickness + "\n" + pcStats.modified.magicalPower + "\n" + pcStats.modified.magicalSkill + "\n" + pcStats.modified.physicalPower + "\n" + pcStats.modified.physicalSkill + "\n" + pcStats.modified.armor + "\n" + pcStats.modified.exp + "\n" + pcStats.modified.nextLevel;
        statsBaseNumbers.text = pcStats.basic.speed + "\n" + pcStats.basic.quickness + "\n" + pcStats.basic.magicalPower + "\n" + pcStats.basic.magicalSkill + "\n" + pcStats.basic.physicalPower + "\n" + pcStats.basic.physicalSkill + "\n" + pcStats.basic.armor + "\n" + pcStats.basic.exp + "\n" + pcStats.basic.nextLevel;

    }


    private void Update()
    {
        if (MultiInput.GetAButtonDown())
        {
            mVCHelper.CallEvent(UIEvents.execute, null);
        }
        if (MultiInput.GetBButtonDown())
        {
            mVCHelper.CallEvent(UIEvents.backout, null);
        }
    }

    public override void MVCEnd(object obj)
    {
        base.MVCEnd(obj);
        mVCHelper.Unsubscribe(UIEvents.dataChanged, DisplayStats);
    }
}
