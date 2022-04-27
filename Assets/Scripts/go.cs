using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class go : MonoBehaviour
{
    [Range(0,100)] public int speed;
    public Vector3 direction;
    private void Start()
    {
        direction *= speed;
    }
    void LateUpdate()
    {
        
        transform.position += new Vector3(direction.x * Time.deltaTime, direction.y * Time.deltaTime, direction.z * Time.deltaTime);
    }
}
