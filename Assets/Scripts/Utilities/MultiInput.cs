using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public static class MultiInput
{
    private static bool isXbox = true;
    public static void Setup()
    {
        string[] controllernames;
        controllernames = Input.GetJoystickNames();

        foreach(string name in controllernames)
        {
            Debug.Log(name);
            if (name == "Controller (XBOX 360 For Windows)")
            {
                isXbox = true;
                break;
            }
            else
            {
                isXbox = false;
            }
            Debug.Log(name);
        }
    }

    public static Vector2 GetPrimaryDirection()
    {
        
        Vector2 dir;

        dir.x = Input.GetAxis("Horizontal");
        dir.y = Input.GetAxis("Vertical");

        return dir;
    }


    public static Vector2 GetSecondaryDirection()
    {
        Vector2 dir;
        if(isXbox)
        {
            dir.x = Input.GetAxis("RXboxHorizontal");
            dir.y = Input.GetAxis("RXboxVertical");
            return dir;
        }
        dir.x = Input.GetAxis("RHorizontal");
        dir.y = Input.GetAxis("RVertical");

        return dir;
    }

    public static bool GetAButtonDown()
    {
        if(isXbox)
        {
            return Input.GetButtonDown("AXbox");
        }
        return Input.GetButtonDown("A");
    }
    public static bool GetBButtonDown()
    {
        if (isXbox)
        {
            return Input.GetButtonDown("BXbox");
        }
        return Input.GetButtonDown("B");
    }
    public static bool GetXButtonDown()
    {
        if (isXbox)
        {
            return Input.GetButtonDown("XXbox");
        }
        return Input.GetButtonDown("X");
    }
    public static bool GetYButtonDown()
    {
        if (isXbox)
        {
            return Input.GetButtonDown("YXbox");
        }
        return Input.GetButtonDown("Y");
    }
}
