using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRotation : MonoBehaviour
{

    public float angle;

    // Update is called once per frame
    void Update()
    {

        print(transform.localEulerAngles.y);
    }
}
