using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    GameObject player;
    int _DeathDistance;
    
    void Start()
    {
        player = GameObject.Find("Player");
        _DeathDistance = 300;
    }

    void Update()
    {
       int distance = Mathf.RoundToInt(Vector3.Distance(player.transform.position, transform.position));
       if (distance > _DeathDistance) Destroy(gameObject);
    }
}
