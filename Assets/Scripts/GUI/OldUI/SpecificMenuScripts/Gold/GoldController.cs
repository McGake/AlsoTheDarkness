using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldController : MonoBehaviour, IGoldController
{
    int previousValue = 0;

    int curValue;

    IGoldModel goldModel;
    IGoldView goldView;
    public void SetGoldText(int gold)
    {
        goldView.SetGoldText(gold);
    }



    void StartGoldDisplay()
    {
        goldModel = GetComponent<GoldModel>();
        goldView = GetComponent<GoldView>();
        SetGoldText(goldModel.GetCurrentGold());
    }

    void OnEnable()
    {
         StartGoldDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        curValue = goldModel.GetCurrentGold();
        if(curValue != previousValue)
        {
            previousValue = curValue;
            SetGoldText(curValue);
        }
    }
}
