using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIEvents
{
    none,
    start,
    end,
    dataChanged,
    advance,
    display,
}
public class MVCHelper : MonoBehaviour
{
    private Dictionary<UIEvents, List<Action<object>>> subscribers = new Dictionary<UIEvents, List<Action<object>>>();

    public void CallEvent(UIEvents uiEvent, object obj)
    {
        for (int i = 0; i < subscribers[uiEvent].Count; i++)
        {
            subscribers[uiEvent][i](obj);
        }
    }

    public void Subscribe(UIEvents eventType, Action<object> callback)
    {
        List<Action<object>> subs;

        if(subscribers.TryGetValue(eventType, out subs ))
        {
            subs.Add(callback);
        }
        else
        {
            subscribers[eventType] = subs;
        }
    }

    public void Unsubscribe(UIEvents eventType, Action<object> callback)
    {
        List<Action<object>> subs;

        if (subscribers.TryGetValue(eventType, out subs))
        {
            for(int i = subscribers[eventType].Count; i >= 0; i--)
            {
                if(subscribers[eventType][i] == callback)
                {
                    subscribers[eventType].RemoveAt(i);
                }
            }
        }
    }
}

public abstract class UIMVC:MonoBehaviour
{
    public MVCHelper mVCHelper;
    private void Start()
    {
        gameObject.SetActive(false);
        mVCHelper.Subscribe(UIEvents.start, MVCStart);
        mVCHelper.Subscribe(UIEvents.end, MVCEnd);
    }

    public virtual void MVCStart(object obj)
    {
        gameObject.SetActive(true);
    }

    public virtual void MVCEnd(object obj)
    {
        gameObject.SetActive(false);
    }

    public virtual void OnDisable()
    {
        mVCHelper.Unsubscribe(UIEvents.start, MVCStart);
    }
}
