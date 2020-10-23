using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMainMenuOnPressStart : MonoBehaviour
{
    public GameObject topMenu;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Start"))
        {
            topMenu.GetComponent<SelectionController>().StartSelection();
            Pauser.PauseGame();
        }
    }


}
