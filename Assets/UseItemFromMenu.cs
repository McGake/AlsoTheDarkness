using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemFromMenu : MonoBehaviour, ISelectionBehavior
{
    // Start is called before the first frame update

    public Item itemToUse;
    public PC referencedPC;
    SelectHeroToApplyModel sHAM;
    public void DoSelectionBehavior()
    {
        if (PartyManager.curParty.items.Contains(itemToUse))
        {
            itemToUse.useScript.UseItem(referencedPC);
            PartyManager.curParty.items.Remove(itemToUse);
            GetComponentInParent<SelectHeroToApplyModel>().RefreshHeros();
            itemToUse = null;
        }
    }
}
