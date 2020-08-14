using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithObjectOnStay : MonoBehaviour
{

    class Pushey
    {
        public float xDist;
        public Transform pushey;
    }

    private List<Pushey> pushies = new List<Pushey>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pushey tempPushey = new Pushey();
        tempPushey.xDist = collision.transform.position.x - transform.position.x;
        tempPushey.pushey = collision.transform;
        pushies.Add(tempPushey);
    }

    private void Update()
    {
        for(int i = 0; i < pushies.Count; i++)
        {
            Vector3 curPos = pushies[i].pushey.position;
            pushies[i].pushey.position = new Vector3(transform.position.x + pushies[i].xDist, curPos.y, curPos.z);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < pushies.Count; i++)
        {
            if (pushies[i].pushey = collision.transform)
            {
                pushies.RemoveAt(i);
                break;
            }
        }
    }
}
