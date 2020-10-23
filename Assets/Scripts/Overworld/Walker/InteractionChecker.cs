using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChecker : MonoBehaviour
{
    public GeneralGridMovement gGM;

    void Update()
    {
        if(MultiInput.GetAButtonDown())
        {
            CheckForInteraction();
        }
    }

    public LayerMask mask;

    void CheckForInteraction()
    {
        RaycastHit2D rh2d = Physics2D.Raycast(((Vector2)transform.position + gGM.moveUtil.raycastOffset), gGM.lastFacingDirection, gGM.moveUtil.tileSize, mask);

        if(rh2d.transform != null)
        {
           Iinteractable interacterface = rh2d.transform.gameObject.GetComponent<Iinteractable>();
            if(interacterface != null)
            {
                interacterface.Interact();
            }
        }
    }
}
