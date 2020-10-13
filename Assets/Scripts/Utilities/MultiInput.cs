using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public static class MultiInput
{
    private static bool isXbox = false;
    private static bool isPS4 = false;
    private static bool isKeybaord = false;
    public static void Setup()
    {
        string[] controllernames;
        controllernames = Input.GetJoystickNames();
        isKeybaord = true;
        foreach(string name in controllernames)
        {
            Debug.Log(name);
            if (name == "Controller (XBOX 360 For Windows)" || name == "Controller (GAME FOR WINDOWS >)")
            {
                Debug.Log("xbox set to true");
                isXbox = true;
                isPS4 = false;
                isKeybaord = false;
                break;
            }
            else if(name.Length == 19)
            {
                isXbox = false;
                isPS4 = true;
                isKeybaord = false;
                break;
            }
        }
    }

    public static Vector2 GetPrimaryDirection()
    {
        
        Vector2 dir;
        if (isPS4 || isXbox)
        {
            dir.x = Input.GetAxis("Horizontal");
            dir.y = Input.GetAxis("Vertical");
        }
        else
        {
            dir.x = Input.GetAxis("KBHorizontal");
            dir.y = Input.GetAxis("KBVertical");
        }

        return dir;
    }


    public static Vector2 GetSecondaryDirection()
    {
        Vector2 dir = new Vector2(0f,0f);
        if (isXbox)
        {
            dir.x = Input.GetAxis("RXboxHorizontal");
            dir.y = Input.GetAxis("RXboxVertical");
            return dir;
        }
        else if (isPS4)
        {
            dir.x = Input.GetAxis("RHorizontal");
            dir.y = Input.GetAxis("RVertical");
        }
        else if(isKeybaord)
        {
            dir.x = Input.GetAxis("KBRVertical");
            dir.y = Input.GetAxis("KBRHorizontal");
        }

        return dir;
    }

    public static bool GetAButtonDown()
    {
        if (isXbox )
        {
            return Input.GetButtonDown("AXbox");
        }
        else if (isPS4)
        {
            return Input.GetButtonDown("A");
        }
        else if (isKeybaord)
        {
            return Input.GetButtonDown("KBA");
        }
        return false;
    }
    public static bool GetBButtonDown()
    {
        if (isXbox)
        {
            return Input.GetButtonDown("BXbox");
        }
        else if (isPS4)
        {
            return Input.GetButtonDown("B");
        }
        else if (isKeybaord)
        {
            return Input.GetButtonDown("KBB");
        }
        return false;
    }
    public static bool GetXButtonDown()
    {
        if (isXbox)
        {
            return Input.GetButtonDown("XXbox");
        }
        else if (isPS4)
        {
            return Input.GetButtonDown("X");
        }
        else if (isKeybaord)
        {
            return Input.GetButtonDown("KBX");
        }
        return false;
    }
    public static bool GetYButtonDown()
    {
        if (isXbox)
        {
            return Input.GetButtonDown("YXbox");
        }
        else if (isPS4)
        {
            return Input.GetButtonDown("Y");
        }
        else if (isKeybaord)
        {
            return Input.GetButtonDown("KBY");
        }
        return false;
    }

    public static float GetRightTriggerDown()
    {
        if (isXbox)
        {
            return Input.GetAxis("RTrigXbox");
        }
        else if (isPS4)
        {
            //return Input.GetButtonDown("Y");
        }
        else if (isKeybaord)
        {
            //return Input.GetButtonDown("KBY");
        }
        return 0f;
    }

    public static float GetLeftTriggerDown()
    {
        if (isXbox)
        {
            return Input.GetAxis("LTrigXbox");
        }
        else if (isPS4)
        {
            //return Input.GetButtonDown("Y");
        }
        else if (isKeybaord)
        {
            //return Input.GetButtonDown("KBY");
        }
        return 0f;
    }
}
