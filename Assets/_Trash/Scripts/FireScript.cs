using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    [SerializeField] GameObject _snowball;

    void Start()
    {
        
    }

    void Update()
    {
        Fire();
    }


    private void Fire()
    {
        if (Input.GetButtonDown("Space"))
        {
            Invoke("CreateSnowball", 0.1f);
        }
    }

    private void CreateSnowball()
    {
        Instantiate(_snowball, transform.position + new Vector3(0,0.5f,-3), transform.rotation);
    }
}
