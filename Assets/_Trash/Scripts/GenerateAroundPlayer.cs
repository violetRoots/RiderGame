using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAroundPlayer : MonoBehaviour
{

    public int overlapBoxsize;
    public LayerMask m_LayerMask;
    public GameObject plane;

    Vector3 SPos1;
    Vector3 SPos2;
    Vector3 SPos3;
    Vector3 SPos4;
    Vector3 SPos5;
    Vector3 SPos6;
    Vector3 SPos7;
    Vector3 SPos8;


    void Start()
    {
        //MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();
        //mesh.material.color = new Vector4(0.6f, 0.6f, 0.6f);

        SPos1 = new Vector3(transform.position.x + 50, transform.position.y, transform.position.z);
        SPos2 = new Vector3(transform.position.x - 50, transform.position.y, transform.position.z);
        SPos3 = new Vector3(transform.position.x + 50, transform.position.y, transform.position.z + 50);
        SPos4 = new Vector3(transform.position.x - 50, transform.position.y, transform.position.z - 50);
        SPos5 = new Vector3(transform.position.x + 50, transform.position.y, transform.position.z - 50);
        SPos6 = new Vector3(transform.position.x - 50, transform.position.y, transform.position.z + 50);
        SPos7 = new Vector3(transform.position.x, transform.position.y, transform.position.z + 50);
        SPos8 = new Vector3(transform.position.x, transform.position.y, transform.position.z - 50);

    }
    void CheckAndSpawn(Vector3 SPos)
    {
        
        for (int i = 0; i < 1; i++)
        {

            //SpawnCheck
            Collider[] hitColliders = Physics.OverlapBox(SPos, new Vector3(overlapBoxsize, overlapBoxsize, overlapBoxsize) / 2, Quaternion.identity, m_LayerMask);

            if (0 == hitColliders.Length)
            {
                Instantiate(plane, SPos, Quaternion.identity);
            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            CheckAndSpawn(SPos1);
            CheckAndSpawn(SPos2);
            CheckAndSpawn(SPos3);
            CheckAndSpawn(SPos4);
            CheckAndSpawn(SPos5);
            CheckAndSpawn(SPos6);
            CheckAndSpawn(SPos7);
            CheckAndSpawn(SPos8);
            
        }
    }
}

   

