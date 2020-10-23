using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStarter : MonoBehaviour
{
    public MVCHelper mainPanel;

    public MVCHelper secondaryPanel;

    public MVCHelper tertiaryPanel;

    public MVCHelper quadrenaryPanel;
    public void StartMenu(object startInfo, string name)
    {
        secondaryPanel?.StartUI(null);
        mainPanel?.StartUI(startInfo);
        
        tertiaryPanel?.StartUI(name);
        quadrenaryPanel?.StartUI(PartyManager.curParty.gp);
    }

}
