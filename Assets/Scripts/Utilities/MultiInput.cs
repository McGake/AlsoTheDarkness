using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MultiInput
{
    // Start is called before the first frame update
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

        dir.x = Input.GetAxis("RHorizontal");
        dir.y = Input.GetAxis("RVertical");

        return dir;
    }
}
