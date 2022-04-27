using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    public int maxSpeed = 10;
    [SerializeField, Range(0f, 100f)]
    int maxAcceleration = 20;
    [SerializeField, Range(0f, 1f)]
    public float velocity;
    public Vector2 delta;
    public PlayerController playerController;
    public Snowball snowball;
    bool oneSnowBall = false;
    public float displacement;



    void Start()
    {
      
    }

    void Update()
    {
        
        transform.Translate(0, 0, -1 * maxSpeed * Time.deltaTime, Space.Self);
        print(delta);
       
        // controll by touch
        if (Input.touchCount > 0)
        {
            

            // get touch
            Touch touch = Input.GetTouch(0);
            delta = touch.deltaPosition;

            if (touch.phase == TouchPhase.Began)
            {
                oneSnowBall = false;
            }

            if (touch.phase == TouchPhase.Moved)
            {
               
            }


            // ограничение длины свайпа
            delta = Vector2.ClampMagnitude(delta, 10f);
            float absX = Mathf.Abs(delta.x);
            float absY = Mathf.Abs(delta.y);

            //horisontal vs vertical check
            if (absY > absX && oneSnowBall == false && delta.y < 0)
            {
                if (touch.deltaTime > 0.002f) Fire();
            }
            
            if (absY < absX)
            {
                
                
               oneSnowBall = true; // No fire while turn
            }
            
        }
        else
        {
            velocity = Mathf.MoveTowards(velocity, 0, maxAcceleration * Time.deltaTime);
            delta = new Vector2(0,0);
        }

        SwipeTurn(delta); // Находится в апдейте для плавного возвращения в исходную позицию
        ;

    }


    void SwipeTurn(Vector2 delta)
    {
        // move object by X
        float desiredVelocity = delta.x * maxSpeed;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity = Mathf.MoveTowards(velocity, desiredVelocity, maxSpeedChange);
        //velocity = Mathf.Clamp(velocity, -6, 6);
        displacement = velocity * Time.deltaTime;
        displacement = Mathf.Clamp(displacement, -1, 1);
        Vector3 newPosition = transform.localPosition + new Vector3(displacement, 0, 0);
        transform.localPosition = newPosition;

    }

    void Fire()
    {
        //smooth back on track
        velocity = Mathf.MoveTowards(velocity, 0, maxAcceleration * Time.deltaTime);
        delta = new Vector2(0, delta.y);
        playerController.Fire();

        oneSnowBall = true;
    }
    
}
