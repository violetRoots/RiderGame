using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSphere : MonoBehaviour
{

    [SerializeField, Range(0f, 100f)]
    int maxSpeed = 10;
    [SerializeField, Range(0f, 100f)]
    int maxAcceleration = 10;
    [SerializeField, Range(0f, 1f)]
    float bounciness = 0.5f;
    [SerializeField]
    Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);
    Vector2 playerInput;
    Vector3 velocity;

    void Start()
    {
        playerInput.x = 0f;
        playerInput.y = 0f;


    }

    
    void Update()
    {
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);


        Vector3 desiredVelocity = new Vector3(playerInput.x, playerInput.y, 0f) * maxSpeed;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.y =
            Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChange);
        Vector3 displacement = velocity * Time.deltaTime;

        Vector3 newPosition = transform.localPosition + displacement;
        if (!allowedArea.Contains(new Vector2(newPosition.x, newPosition.y)))
        {
            if (newPosition.x < allowedArea.xMin)
            {
                newPosition.x = allowedArea.xMin;
                velocity.x = -velocity.x * bounciness;
            }
            else if (newPosition.x > allowedArea.xMax)
            {
                newPosition.x = allowedArea.xMax;
                velocity.x = -velocity.x * bounciness;
            }
            if (newPosition.y < allowedArea.yMin)
            {
                newPosition.y = allowedArea.yMin;
                velocity.y = -velocity.y * bounciness;
            }
            else if (newPosition.y > allowedArea.yMax)
            {
                newPosition.y = allowedArea.yMax;
                velocity.y = -velocity.y * bounciness;
            }
        }

        transform.localPosition = newPosition;

        
        print(newPosition);
    }
}
