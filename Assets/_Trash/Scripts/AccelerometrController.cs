using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometrController : MonoBehaviour
{
    

    Vector3 dir = Vector3.zero;
    [SerializeField, Range(0f, 100f)]
    public int speed = 10;


    void Update()
    {
        dir.x = Input.acceleration.x;
        dir.z = Input.acceleration.y;

        if (dir.sqrMagnitude > 1)
           dir.Normalize();

        transform.Translate(dir * speed * Time.deltaTime);

        
    }
}
