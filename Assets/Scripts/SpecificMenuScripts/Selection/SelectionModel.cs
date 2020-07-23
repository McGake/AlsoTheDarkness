using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionModel : GeneralSelectionModel
{


    public override List<GameObject> GetSelections()
    {
        return selections;
    }

}
