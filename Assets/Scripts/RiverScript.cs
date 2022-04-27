using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverScript : MonoBehaviour
{
    //PlayerController PC;

    //private void Start()
    //{
    //    PC = GameObject.Find("Player").GetComponent<PlayerController>();
    //}

    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
        }
        
    }
}



