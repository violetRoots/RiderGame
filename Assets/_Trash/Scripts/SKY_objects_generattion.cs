using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SKY_objects_generattion : MonoBehaviour
{
    [SerializeField] GameObject[] Objects;
    private Vector3 SpawnPos;
    GameObject player;
    public PlayerController PlayerController;
    float distance;
    int rnd;
    bool chance;
    bool cloudZones;
    

    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && cloudZones) GenerateObject(Objects[0]);


    }

    private void Update()
    {
        rnd = Random.Range(-100, 100);
        chance = Random.Range(0, 5) > 3;
        player = GameObject.Find("Player");
        distance = PlayerController.distance;
        if (distance > 0 && distance < 100 || distance > 200 && distance < 300 || distance > 400 && distance < 500 || distance > 600 && distance < 700 || distance > 800 && distance < 900) cloudZones = true;
        else cloudZones = false;
      
    }

    void GenerateObject(GameObject Object)
    {
        
        
        SpawnPos = new Vector3(transform.position.x + rnd, 10, transform.position.z - 200);
        Instantiate(Object, SpawnPos, Quaternion.Euler(0, 0, 0));
    }

}


