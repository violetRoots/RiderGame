using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour
{
    float sens = 40;
    float mouseX;
    bool isRotate;
    float direction;


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isRotate = true;
        }
        else isRotate = false;
    }

    private void FixedUpdate()
    {
        if (isRotate)
        {
            mouseX += Input.GetAxis("Mouse X") * sens;
            mouseX = Mathf.Clamp(mouseX, -45, 45);
            transform.rotation = Quaternion.Euler(0, 0, mouseX);
            
        }

        print(mouseX);
        direction = -1f;
        transform.position += transform.up * direction * Time.deltaTime;
    }

}
