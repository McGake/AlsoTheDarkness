using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MultiInput.Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if(MultiInput.GetAButtonDown())
        {
            Debug.Log("A button is working!!!");
        }
        float x = MultiInput.GetSecondaryDirection().x;
        float y = MultiInput.GetSecondaryDirection().y;
    }
}
