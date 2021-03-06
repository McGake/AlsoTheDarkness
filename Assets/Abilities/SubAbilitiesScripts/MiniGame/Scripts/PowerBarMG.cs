﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PowerBarMG", menuName = "SubProjectileAbility/MiniGames/PowerBarMG", order = 1)]
public class PowerBarMG : SubProjectileAbility
{
    public float maxPower;

    public float powerIncreaseSpeed;

    private float curPower;

    private float curPowerPercentage;

    public Vector3 mgDisplayOffset;

    public GameObject powerContainer;

    private GameObject powerContainerInstance;

    private Image powerHeat;

    private Transform ownerTransform;

    public void OnEnable()
    {
        powerContainerInstance = null;
    }


    private bool skip = true;

    public override void DoInitialProjectileSubAbility(ProjectileAbility pa)
    {
        ownerTransform = pa.ability.Owner.transform;

        Image[] images;
        if(powerContainerInstance == null)
        {
            powerContainerInstance = pa.ability.InstantiateInWorldSpaceCanvas(powerContainer, ownerTransform.position);
            images = powerContainerInstance.GetComponentsInChildren<Image>();
            foreach(Image im in images)
            {
                if(im.fillMethod == Image.FillMethod.Vertical)
                {
                    powerHeat = im;
                }
            }
        }
        skip = true;

        powerContainerInstance.SetActive(true);
        curPower = 0f;
    }


    public override void DoProjectileSubAbility(ProjectileAbility pa)
    {
        if(skip == true)
        {
            skip = false;
            return;
        }
        curPowerPercentage = curPower / maxPower;
        powerHeat.fillAmount = curPowerPercentage;

        curPower += powerIncreaseSpeed * Time.deltaTime;

        if(curPowerPercentage>=1f)
        {
            curPower = 0f;
        }

        powerContainerInstance.transform.position = ownerTransform.position + mgDisplayOffset;

        if(MultiInput.GetAButtonDown() && pa.ability.IsCurrentSelectedHero(pa.ability.Owner))
        {
            powerContainerInstance.SetActive(false);
            pa.power = curPower;
            EndProjectileSubAbility();            
        }
    }
}
 
