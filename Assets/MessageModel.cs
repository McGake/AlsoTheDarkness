using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MessageModel : UIMVC
{
    public MessageForObject[] messagesForObjects;

    private void Awake()
    {
        for(int i = 0; i < messagesForObjects.Length; i++)
        {
            messagesForObjects[i].helper.Subscribe(UIEvents.start, messagesForObjects[i].UpdateMessage);
            messagesForObjects[i].updateMessage = UpdateMessage;
        }
    }

    private void StartSecondaryMenu(object obj)
    {
        mVCHelper.StartUI(null);
    }

    private void UpdateMessage(string message)
    {
        mVCHelper.CallEvent(UIEvents.display, message);
    }



}


[System.Serializable]
public class MessageForObject
{
    public string message;
    public MVCHelper helper;
    public Action<string> updateMessage;

    public void UpdateMessage(object obj)
    {
        updateMessage(message);
    }

}
