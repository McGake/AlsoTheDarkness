using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionGizmo : MonoBehaviour
{

    public Texture gizmoTexture;
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "bob");
        Gizmos.DrawIcon(transform.position, "transitionMarker");
    }
}
