using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInBounds : MonoBehaviour
{

    private Vector2 boundsMax;
    private Vector2 boundsMin;
    private float halfHeight;
    private float halfWidth;
    public Collider2D mainCollider;
    private bool stayInBounds = false; 
    // Start is called before the first frame update
    void Start()
    {
        halfHeight = mainCollider.bounds.extents.y;
        halfWidth = mainCollider.bounds.extents.x;

    }

    public void SetUpBounds(Collider2D boundary)
    {
        boundsMax.x = boundary.bounds.center.x + boundary.bounds.extents.x - halfWidth;
        boundsMin.x = boundary.bounds.center.x - boundary.bounds.extents.x + halfWidth;
        boundsMax.y = boundary.bounds.center.y + boundary.bounds.extents.y - halfHeight;
        boundsMin.y = boundary.bounds.center.y - boundary.bounds.extents.y + halfHeight;
        stayInBounds = true;
    }

    public void SetStayInBounds(bool newSetting)
    {
        stayInBounds = newSetting;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (stayInBounds)
        {
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(transform.position.x, boundsMin.x, boundsMax.x);
            clampedPosition.y = Mathf.Clamp(transform.position.y, boundsMin.y, boundsMax.y);
            transform.position = clampedPosition;
        }
    }
}
