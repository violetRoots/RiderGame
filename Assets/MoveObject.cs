using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RiderGame
{
    
    public class MoveObject : MonoBehaviour
    {
        public float speed = 1.0f;  // Adjust the speed of movement

        private void Update()
        {
            // Calculate the new position based on the current position and speed
            Vector3 newPosition = transform.position + Vector3.right * speed * Time.deltaTime;

            // Apply the new position to the object
            transform.position = newPosition;
        }
    }
}
