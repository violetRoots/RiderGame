using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveTest : MonoBehaviour
{

    public float turnSpeed = 160f;
    public float _speed = 3;
    public Transform _direction;
    private Vector3 rotation;
    int x;
    Ray ray;

    void Update()
    {

        // _direction.x = Input.GetAxis("Horizontal");
        // _direction.z = Input.GetAxis("Vertical");

        //Vector3 dir = _direction.transform.position;
        //transform.Translate(dir * _speed * Time.deltaTime);
        
        

        if (Input.GetKey(KeyCode.A))
        {
            rotation = new Vector3(0, -1, 0);
            transform.Rotate(rotation * Time.deltaTime * turnSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotation = new Vector3(0, 1, 0);
            transform.Rotate(rotation * Time.deltaTime * turnSpeed);
        }


        //draw ray
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 100, Color.blue);


    }

    private void AvoidObstacle()
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            // left or right turn
            x = 0;
            while (x == 0)
            {
                x = Random.Range(-1, 2);
            }
            print(x);



            if (hit.distance <= 6f)
            {


                // rotation
                rotation = new Vector3(0, x, 0);
                transform.Rotate(rotation * Time.deltaTime * turnSpeed);


            }
        }
    }
}

