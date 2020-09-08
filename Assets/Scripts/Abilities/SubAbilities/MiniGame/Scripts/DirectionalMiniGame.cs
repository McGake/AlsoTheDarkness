using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DirectionalMG", menuName = "SubAbility/MiniGames/DirectionalMG", order = 1)]
public class DirectionalMiniGame : SubAbility
{

    public Vector3 mgDisplayOffset;

    public GameObject joysticIcon;

    private GameObject joystickIconInstance;

    private Transform ownerTransform;


    public void OnEnable()
    {
        joystickIconInstance = null;
    }


    private bool skip = true;

    public override void DoInitialSubAbility(Ability ab)
    {
        ownerTransform = ab.Owner.transform;
        if(joystickIconInstance == null)
        {
            joystickIconInstance = ab.InstantiateInWorldSpaceCanvas(joysticIcon, ownerTransform.position);
        }
        skip = true;

        joystickIconInstance.SetActive(true);
    }


    public override void DoSubAbility(Ability ab)
    {
        if(skip == true)
        {
            skip = false;
            return;
        }
        joystickIconInstance.transform.position = ownerTransform.position + mgDisplayOffset;

        if (Mathf.Abs(Input.GetAxisRaw("RHorizontal")) > .8f || Mathf.Abs(Input.GetAxisRaw("RVertical")) > .8f) //TODO: I cant figure out how to get more accurate controller input. there seems to be a big dead zone not just at the center, but in a cross out from the center. This is on an Xbox controller. The result is that the controll stick usually registers straght vertical or straight across, and only rarely an unsatisifyingly limited diagonal
        {
            
            joystickIconInstance.SetActive(false);        
            ab.positionTargets.Add( new Vector3(Input.GetAxisRaw("RHorizontal"), Input.GetAxisRaw("RVertical"), 0f).normalized);
            EndSubAbility();            
        }

    }


}
 
