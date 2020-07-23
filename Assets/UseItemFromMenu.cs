using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemFromMenu : MonoBehaviour, ISelectionBehavior
{
    // Start is called before the first frame update

    public Item itemToUse;
    public PC referencedPC;
    public void DoSelectionBehavior()
    {
        itemToUse.useScript.UseItem(referencedPC);
    }
}
