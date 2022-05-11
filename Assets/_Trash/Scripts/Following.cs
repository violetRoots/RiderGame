using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{

    public Transform target;
    public int smoothFactor;
    public Vector3 offset;
    

    void Update()
    {
        follow();
    }

    void follow()
    {
        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothFactor * Time.deltaTime);
       // if (smoothFactor == 0) { transform.position = transform.position + targetPos; }
    }
}
