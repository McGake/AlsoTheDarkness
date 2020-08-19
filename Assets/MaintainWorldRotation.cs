using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainWorldRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.rotation.z * -1.0f); //TODO: make this an event to keep from having to do this every update
    }
}
