using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControlRotation: MonoBehaviour
{
    
    public float speedGo = 10;
    [Range(0, 2)]
    public float speedRotate;
    public float angleRot;
    public int StarsCount;
    public Vector2 delta;
    public float velocity;
    public PlayerController PC;
    public Snowball snowball;
    bool oneSnowBall = false;
    public float displacement;
    Vector3 dir = Vector3.zero;


    private void FixedUpdate()
    {
        Go();
        SwipeCheck();
        //TurnAccelerometer(80);
       

    }
    private void Start()
    {
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Go()
    {
        //transform.position += transform.forward * -speedGo * Time.deltaTime * delta.x;
        transform.Translate(0, 0, -speedGo * Time.deltaTime, Space.Self);
    }
    void SwipeCheck()
    {
        if (Input.touchCount > 0)
        {

            if (PC.fallen) PC.GetUp();

            Touch touch = Input.GetTouch(0);
            delta = touch.deltaPosition;

            if (touch.phase == TouchPhase.Ended && oneSnowBall == false) Fire(delta.y);
            if (touch.phase == TouchPhase.Began)
            {
                oneSnowBall = false;
            }

            float delX = Mathf.Abs(delta.x);
            float delY = Mathf.Abs(delta.y);
            if (delY > delX) //vertical
            {
                
                if (delta.y < 0 && oneSnowBall == false)
                {
                    //print("DOWN");
                    oneSnowBall = true;
                    //speedGo = Mathf.Lerp(0, 10, 1);
                }
                if (delta.y > 0 && oneSnowBall == false)     
                {
                    //print("UP");       
                    oneSnowBall = true;
                   // speedGo = Mathf.Lerp(10, 0, 1);
                }
            }

            if (delY < delX) //horisontal
            {
                Turn(touch);

                oneSnowBall = true; // No fire while turn
            }


        }


    }
    private void TurnAccelerometer(int x)
    {
        dir.x = Input.acceleration.x;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();
        transform.Translate(dir * x * Time.deltaTime);
        
    }

    private void Turn(Touch touch)
    {
        //print(angleRot);
        angleRot += delta.x * speedRotate;
        angleRot = Mathf.Clamp(angleRot, -60, 60);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -angleRot, 0), 1.0f);
       

    }

    void Fire(float delta)
    {
        print("FIRE");
        oneSnowBall = true;
        Instantiate(snowball, transform.position + new Vector3(0, 0.6f, -0.6f), transform.rotation);
    }

}


