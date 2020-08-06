using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum AssociatedButton
{
    open = 0,
    rightButton = 1,
    leftButton = 2,
    bothButtons = 3,
}

public class AbilityCluster : MonoBehaviour
{
    public AssociatedButton clusterButton;
    public List<AbilityButton> abilityButtons;
}
[System.Serializable]
public class AbilityButton
{
    public GameObject uIButton;
    public AbilityView abilityView;
    public Image radialProgress;
    public GameObject greyMask;
    public Ability ability;
}
