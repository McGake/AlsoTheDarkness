using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMask : MonoBehaviour
{

    public NavigationTypes nt;
    int ntInt;
    // Start is called before the first frame update
    void Start()
    {
        ntInt = (int)nt;
    }

    // Update is called once per frame
    void Update()
    {
        if(MultiInput.GetYButtonDown())
        {
            Debug.Log("y bootton down");
            if((nt & NavigationTypes.raft) != 0)
            {
                Debug.Log("containted");
            }
        }
    }
}
