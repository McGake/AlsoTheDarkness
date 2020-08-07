using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

//this currently does not work
public class OnlyCollideWithOneThing : MonoBehaviour
{
    private Collider2D localCollider; 

    public void Awake()
    {
        localCollider = GetComponent<Collider2D>();       

    }

    public void SetThingToCollideWith(Collider2D thingToCollideWith)
    {
        Debug.Log(thingToCollideWith);
        Debug.Log(localCollider);
        Physics2D.IgnoreCollision(thingToCollideWith, localCollider, false);
        ObjectsInBattle.objectsInBattle
        Debug.Log ("will you ignore collision between " + thingToCollideWith.name + " " + localCollider.name + " ? " + Physics2D.GetIgnoreCollision(thingToCollideWith, localCollider));
    }
}
