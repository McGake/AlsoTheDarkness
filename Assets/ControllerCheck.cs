using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("A"))
        {
            Debug.Log("A button is working!!!");
        }
        //if(Input.GetAxis("Vertical")>0f)
        //{
        //    Debug.Log("up is working");
        //}
        //if(Input.GetAxis("Horizontal") > 0f)
        //{
        //    Debug.Log("right working");
        //}

    }
}
