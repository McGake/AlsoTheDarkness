using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMagicAim : MonoBehaviour
{

    public float angle = 45f;

    private float impulse = 1f;

    public GameObject testGameObject1;

    public GameObject testGameObject2;

    public Vector3 facingDirection
    {
        get
        {
            Vector3 result =transform.rotation * Vector3.right;
            
            return result.sqrMagnitude == 0f ? Vector3.forward : result.normalized;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ShootTestProjectiles();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("A"))
        {
            
        }
    }

    void ShootTestProjectiles()
    {
        
        Vector3 direction = facingDirection;
        Vector3 minDirection = facingDirection;

        //direction = Quaternion.AngleAxis(angle, Vector3.Cross(direction, Vector3.up)) * direction;
        Debug.Log(direction + " direction");
        testGameObject1= GameObject.Instantiate(testGameObject1, transform.position, Quaternion.identity );
        testGameObject1.GetComponent<Rigidbody>().AddForce(direction * impulse, ForceMode.Impulse);
    }
}
