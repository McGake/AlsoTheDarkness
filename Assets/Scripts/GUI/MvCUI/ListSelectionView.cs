using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    public ScrollRect scrollRect;

    private float scrollZeroPos;

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

        if(scrollRect != null)
        {
            scrollZeroPos = scrollRect.content.anchoredPosition.y;
        }
    }
    public override void MVCStart(object obj)
    {
        cursor.SetActive(true);
        base.MVCStart(obj);
        
    }

    private void MoveCursorByIndx()
    {
        if (selectionList.Count > 0)
        {
            cursor.transform.position = (Vector2)selectionList[curSelection].transform.position + cursorOffset;
        }
    }

    protected void SetButtons(object obj)
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
                ScrollScrollRect();
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

    private void ScrollScrollRect()
    {
        if (scrollRect != null)
        {
            RectTransform curSelectionRect = selectionList[curSelection].GetComponent<RectTransform>();
            RectTransform contentRect = scrollRect.content;
            RectTransform scrollRectRect = scrollRect.gameObject.GetComponent<RectTransform>();

            float selectedBottom = curSelectionRect.anchoredPosition.y - curSelectionRect.rect.height;// - curSelectionRect.rect.height;
            float selectedTop = curSelectionRect.anchoredPosition.y + (curSelectionRect.rect.height/2);

           float heightAbove = -(scrollZeroPos - (contentRect.anchoredPosition.y));
            float heightBelow = -(scrollZeroPos - (contentRect.anchoredPosition.y));
            Debug.Log("height below " + heightBelow);

            Debug.Log("selectedBottom " + selectedBottom);




                if (heightAbove + selectedTop > 0)
                {
                    contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, scrollZeroPos -selectedTop);
                }
            


                if (-scrollRectRect.rect.height > selectedBottom + heightAbove)
                {
                    contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, scrollZeroPos + (-selectedBottom - scrollRectRect.rect.height));
                }
            
            //if(selectedTop > scrollZeroPos)
            //{
            //    contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, scrollZeroPos + (-selectedTop - scrollRectRect.rect.height));
            //}

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
    }

}



