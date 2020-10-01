using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListSelectionView : UIMVC
{
    int curSelection = 0;

    private List<GameObject> selectionList;

    public GameObject cursor;

    public Vector2 cursorOffset;

    public float inputRefractoryPeriod;

    private float inputRefractoryEnd =0;

    public float inputThreshold;

    bool isHorizontal = false;

    private Action GetDirInput;

    public override void MVCSetup(object obj)
    {
        base.MVCSetup(obj);
        mVCHelper.Subscribe(UIEvents.dataChanged, SetButtons);

        
        curSelection = 0;
        if (GetComponent<LayoutGroup>() is VerticalLayoutGroup)
        {
            GetDirInput = GetVertDirInput;
        }
        else if (GetComponent<LayoutGroup>() is HorizontalLayoutGroup)
        {
            GetDirInput = GetHorDirInput;
        }
    }
    public override void MVCStart(object obj)
    {
        cursor.SetActive(true);
        base.MVCStart(obj);
        
    }

    private void MoveCursorByIndx()
    {
        cursor.transform.position = (Vector2)selectionList[curSelection].transform.position + cursorOffset;
    }

    private void SetButtons(object obj)
    {
        selectionList = (List<GameObject>)obj;
        for (int i = 0; i < selectionList.Count; i++)
        {
            selectionList[i].transform.SetParent(transform);
            selectionList[i].transform.localScale = Vector3.one;
            selectionList[i].transform.SetAsLastSibling();
        }
        Invoke("MoveCursorByIndx", .04f);
    }

    private void Update()
    {
        GetDirInput();
        if(Mathf.Abs(dirInput) > inputThreshold)
        {
            if(Time.time > inputRefractoryEnd)
            {
                if (dirInput > 0)
                {
                    
                    IncrementCurSelection((int)Mathf.Sign(dirInput));

                }
                else
                {
                    IncrementCurSelection((int)Mathf.Sign(dirInput));
                }
                MoveCursorByIndx();
                inputRefractoryEnd = Time.time + inputRefractoryPeriod;
            }
        }

        if(MultiInput.GetAButtonDown())
        {
            mVCHelper.CallEvent(UIEvents.execute, selectionList[curSelection]);
        }
        if(MultiInput.GetBButtonDown())
        {
            Debug.Log("backout");
            mVCHelper.CallEvent(UIEvents.backout, null);
        }
    }

    private float dirInput;
    private void GetHorDirInput()
    {
         dirInput = MultiInput.GetPrimaryDirection().x;
    }

    private void GetVertDirInput()
    {
        dirInput = MultiInput.GetPrimaryDirection().y *-1;
    }

    private void IncrementCurSelection(int incrementAmount)
    {
        curSelection += incrementAmount;

        if(curSelection >= selectionList.Count)
        {
            curSelection = 0;
        }
        else if(curSelection < 0)
        {
            curSelection = selectionList.Count - 1;
        }
    }

    public override void MVCEnd(object obj)
    {
        base.MVCEnd(obj);
        cursor.SetActive(false);
        mVCHelper.Unsubscribe(UIEvents.dataChanged, SetButtons);
        Debug.Log("unsubscribe called54");
    }

}



