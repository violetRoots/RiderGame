using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    public float speed;
    SwipeControl SWC;
    public  int lifetime = 3;
    public float displacement;

    private void Start()
    {
        SWC = GameObject.Find("Player").GetComponent<SwipeControl>();
        displacement = SWC.displacement;


    }


    private void Awake()
    {
        Destroy(gameObject, lifetime); 
    }


    private void FixedUpdate()
    {
        
        print(displacement);
        transform.Translate(displacement, 0, -1 * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            
            collision.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }
        
    }

}
