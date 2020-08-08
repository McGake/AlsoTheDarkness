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

    public void SetThingToCollideWith(Collider2D thingToCollideWith) //ToDo:This is a breakable stop gap. replace this with a pre battle start setup once we add our pooling system
    {
        Debug.Log(thingToCollideWith);
        Debug.Log(localCollider);

        List<GameObject> pcsInBattle = ObjectsInBattle.objectsInBattle.pcsInBattle;
        List<GameObject> enemiesInBattle = ObjectsInBattle.objectsInBattle.enemiesInBattle;


        for (int i = 0; i  < pcsInBattle.Count; i++)
        {
            Physics2D.IgnoreCollision(pcsInBattle[i].transform.GetChild(0).GetComponent<Collider2D>(), localCollider, true); //this is very breakable (getting child by number)
        }

        for (int i = 0; i < enemiesInBattle.Count; i++)
        {
            Physics2D.IgnoreCollision(pcsInBattle[i].transform.GetChild(0).GetComponent<Collider2D>(), localCollider, true);
        }
        Physics2D.IgnoreCollision(thingToCollideWith, localCollider, false);
    }
}
