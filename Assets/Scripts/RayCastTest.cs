using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTest : MonoBehaviour
{
    Vector3 startpos;
    public float speedCube;
    public Transform direction;

    private void Start()
    {
        startpos.y = transform.rotation.y;
    }


    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 100, Color.blue);
       
        transform.position += transform.forward * speedCube * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(direction.position);
        //transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(finih.position - transform.position), Time.deltaTime * 2f);

        

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        bool ishit = Physics.Raycast(ray, out hit);

        if (ishit)
        {
            if (hit.distance <= 6f)
            {
                Quaternion.LookRotation(direction.position);
                //transform.Rotate(Vector3.Lerp(new Vector3(0, 0, 0), Vector3.up * 10f, 1f));
                //startpos.y = transform.rotation.y;

            }
        }
        else transform.rotation = Quaternion.Euler(0, transform.rotation.y, 1);
        print(startpos.y);
    }

    private void FixedUpdate()
    {
        
        

    }
}
