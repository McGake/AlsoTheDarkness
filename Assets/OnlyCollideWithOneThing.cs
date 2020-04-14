using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this currently does not work
public class OnlyCollideWithOneThing : MonoBehaviour
{
    public Collider2D thingToCollideWith;

    private Collider2D localCollider; 

    public void Awake()
    {

    }
    public void Start()
    {
        localCollider = gameObject.GetComponent<Collider2D>();  

   }
    public void OnCollisionEnter2D(Collision2D col)
    {
        
    }
}
