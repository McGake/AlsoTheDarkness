using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingDirectionInput : WalkingDirectionInput
{
    public override Vector2 DirectionalInput()
    {
        return MultiInput.GetPrimaryDirection();
    }

    public override bool IsThereDirectionalInput(float threshold)
    {
        if (Mathf.Abs(MultiInput.GetPrimaryDirection().x) > threshold || Mathf.Abs(MultiInput.GetPrimaryDirection().y) > threshold)
        {
            return true;
        }
        return false;
    }
}
