using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAndSpwan : MonoBehaviour
//Attach this script to your GameObject. This GameObject doesn’t need to have a Collider component
//Set the Layer Mask field in the Inspector to the layer you would like to see collisions in (set to Everything if you are unsure).
//Create a second Gameobject for testing collisions. Make sure your GameObject has a Collider component (if it doesn’t, click on the Add Component button in the GameObject’s Inspector, and go to Physics>Box Collider).
//Place it so it is overlapping your other GameObject.
//Press Play to see the console output the name of your second GameObject

//This script uses the OverlapBox that creates an invisible Box Collider that detects multiple collisions with other colliders. The OverlapBox in this case is the same size and position as the GameObject you attach it to (acting as a replacement for the BoxCollider component).


{
   

    public LayerMask m_LayerMask;
    
    public GameObject large;
    public GameObject middle;
    public GameObject small;
    public GameObject stars;
    public GameObject cubes;
    public GameObject reshetka;

    [Range(0, 100)] public int large_num;
    public float large_Range = 20;
    public int large_Overlap = 4;

    [Range(0, 100)] public int middle_num;
    public float middle_range = 20;
    public int middle_overlap= 4;

    [Range(0, 100)] public int small_num;
    public float small_range = 20;
    public int small_overlap = 4;

    [Range(0, 100)] public int stars_num;
    public float stars_range = 20;
    public int stars_overlap = 4;

    [Range(0, 100)] public int reshetka_num;
    public float reshetka_range = 20;
    public int reshetka_overlap = 4;

    void Start()
    {
        GenerateObject(large_num, large, large_Range, large_Overlap);
        GenerateObject(middle_num, middle, middle_range, middle_overlap);
        GenerateObject(small_num, small, small_range, small_overlap);
        GenerateObject(stars_num, stars, stars_range, stars_overlap);
       // GenerateObject(small_num, cubes, small_range, small_overlap);
        GenerateObject(reshetka_num, reshetka, reshetka_range, reshetka_overlap);
    }
   
    void GenerateObject(int A, GameObject B, float C, int D)
    {
        
        for (int i = 0; i < A; i++)
        {
            Vector3 SpawnPos;
            SpawnPos = new Vector3(transform.position.x + Random.Range(-C, C), transform.position.y, transform.position.z + Random.Range(-C, C));

            Collider[] hitColliders = Physics.OverlapBox(SpawnPos, new Vector3(D, D, D) / 2, Quaternion.identity, m_LayerMask);

            if (0 == hitColliders.Length) Instantiate(B, SpawnPos, Quaternion.Euler(0, 90, 0));


        }



    }
}

