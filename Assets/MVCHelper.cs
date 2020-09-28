using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public enum UIEvents
{
    none,
    start,
    end,
    setup,
    dataChanged,
    advance,
    display,
}
public class MVCHelper : MonoBehaviour
{
    private Dictionary<UIEvents, List<Action<object>>> subscribers = new Dictionary<UIEvents, List<Action<object>>>();

    public void StartUI(object obj)
    {
        Debug.Log("UIStart Called");
        CallEvent(UIEvents.setup, obj);
        CallEvent(UIEvents.start, obj);
        Pauser.PauseGame();

    }



    public void CallEvent(UIEvents uiEvent, object obj)
    {
        Debug.Log(uiEvent + " event called$$$ " + " totalcount" + subscribers[uiEvent].Count);
        for (int i = 0; i < subscribers[uiEvent].Count; i++)
        {
            Debug.Log(subscribers[uiEvent][i].Target.GetType().Name + " " + uiEvent + " cont " + i );
            subscribers[uiEvent][i](obj);
            Debug.Log("total count now " + subscribers[uiEvent].Count);
        }
    }

    public void Subscribe(UIEvents eventType, Action<object> callback)
    {
        List<Action<object>> subs;

        //Debug.Log("subscriber to " + eventType + " is " + callback.Target.GetType());

        if(subscribers.TryGetValue(eventType, out subs ))
        {
            subs.Add(callback);
        }
        else
        {
            List<Action<object>> newSubscribers = new List<Action<object>>();
            newSubscribers.Add(callback);
            subscribers.Add(eventType, newSubscribers);
        }
    }

    public void Unsubscribe(UIEvents eventType, Action<object> callback)
    {
        List<Action<object>> subs;

        if(subscribers.ContainsKey(eventType) == false)
        {
            Debug.LogError("we tried to unsubscribe from a dictionary non entry");
            return;
        }
        if(subscribers[eventType] == null)
        {
            Debug.LogError("we tried to unsubscribe from a null list");
        }

        if (subscribers.TryGetValue(eventType, out subs))
        {
            for(int i = subscribers[eventType].Count-1; i >= 0; i--)
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

    private void Start() //Danger!!! in the current setup, these two methods never get unsubscribed. This is by design, but the design might be a bad one.
    {
        mVCHelper.Subscribe(UIEvents.setup, MVCSetup);
        mVCHelper.Subscribe(UIEvents.start, MVCStart);
        gameObject.SetActive(false);
    }

    public virtual void MVCSetup(object obj)
    {

    }

    public virtual void MVCStart(object obj)
    {
        mVCHelper.Subscribe(UIEvents.end, MVCEnd);
        gameObject.SetActive(true);
    }

    public virtual void MVCEnd(object obj)
    {
        gameObject.SetActive(false);
    }
}
