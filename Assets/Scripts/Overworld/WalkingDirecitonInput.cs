using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WalkingDirectionInput : MonoBehaviour
{
    public abstract bool IsThereDirectionalInput(float threshold);
    public abstract Vector2 DirectionalInput();
}
