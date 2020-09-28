using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSelectionView : MonoBehaviour
{
    public GameObject selectionPrefab;

    selectable test = new selectable();
    ShopItem testitem = new ShopItem();

    private void Start()
    {
        test.objectReference = testitem;
    }

}


public class selectable
{
    public object objectReference;
    public Action actionOnSelect;
}
