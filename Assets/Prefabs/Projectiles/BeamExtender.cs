using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamExtender : MonoBehaviour
{

    public float shootTime;

    public float shootLength;

    private float endTime;

    public SpriteRenderer sR;
    public void OnEnable()
    {

        endTime = Time.time + shootTime;
        //sR = GetComponent<SpriteRenderer>();
        sR.size = new Vector2(shootLength, sR.size.y);


    }

    public void Update()
    {
        
        if (Time.time > endTime)
        {
            gameObject.SetActive(false);
        }
    }
}
