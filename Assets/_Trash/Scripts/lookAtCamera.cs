using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtCamera : MonoBehaviour
{
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Target");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(target.transform);
    }
    
}

