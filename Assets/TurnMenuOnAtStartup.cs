using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMenuOnAtStartup : MonoBehaviour
{
    public GameObject menu;
    void Awake()
    {
        menu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
