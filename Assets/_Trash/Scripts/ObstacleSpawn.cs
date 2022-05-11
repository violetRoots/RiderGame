///using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] enemy;
    public PlayerController playerController;
    Vector3 instaPos;
    public float distance;
    int riverCount = 0;

    private void Start()
    {
        distance = playerController.distance;
       
    }


    private void FixedUpdate()
    {
        distance = playerController.distance;
        instaPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        RiverInsta();
    }

    private void RiverInsta()
    {
        int x = 50;

        if (distance == x/2 && riverCount < 1)
        {
            Instantiate(enemy[0], instaPos, Quaternion.Euler(0, 0, 0));
            riverCount++;
        }

        if (distance == x && riverCount < 2)
        {
            Instantiate(enemy[0], instaPos, Quaternion.Euler(0, 0, 0));
            riverCount++;
        }

        if (distance == 2 * x && riverCount < 3)
        {
            Instantiate(enemy[0], instaPos, Quaternion.Euler(0, 0, 0));
            riverCount++;
        }

        if (distance == 3 * x && riverCount < 4)
        {
            Instantiate(enemy[0], instaPos, Quaternion.Euler(0, 0, 0));
            riverCount++;
        }

        if (distance == 5 * x && riverCount < 5)
        {
            Instantiate(enemy[0], instaPos, Quaternion.Euler(0, 0, 0));
            riverCount++;
        }

        if (distance == 7 * x && riverCount < 6)
        {
            Instantiate(enemy[0], instaPos, Quaternion.Euler(0, 0, 0));
            riverCount++;
        }

        if (distance == 9 * x && riverCount < 7)
        {
            Instantiate(enemy[0], instaPos, Quaternion.Euler(0, 0, 0));
            riverCount++;
        }

        if (distance == 12 * x && riverCount < 8)
        {
            Instantiate(enemy[0], instaPos, Quaternion.Euler(0, 0, 0));
            riverCount++;
        }


    }
}