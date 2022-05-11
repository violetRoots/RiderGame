using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-200)]
public class PlaneSpawn : MonoBehaviour
{
    public GameObject plane;
    Vector3 planeScale;
    int x = 2;

    private void Start()
    {
        planeScale = plane.gameObject.transform.localScale;
        Instantiate(plane, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(plane, new Vector3(0, 0, 40), Quaternion.identity);
        Instantiate(plane, new Vector3(40, 0, 0), Quaternion.identity);
        Instantiate(plane, new Vector3(40, 0, 40), Quaternion.identity);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Instantiate(plane, new Vector3(0, 0, 10 * x), Quaternion.identity);
            x +=2;
        }
    }
}