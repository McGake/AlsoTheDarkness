using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSelectionController : UIMVC
{
    int curSelection = 0;

    List<GameObject> selectionList;

    public override void MVCStart(object obj)
    {
        base.MVCStart(obj);

    }
}
