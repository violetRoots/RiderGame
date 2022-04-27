using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] enemy;
    private Vector3 SpawnPos;
    private Vector3 SpawnPos2;
    Vector3 SpawnPos3;
    float timeSpawn;
    public PlayerController playerController;
    float speedPlayer;
    float distance;

    public LayerMask m_LayerMask;
    public int rndRange = 20;
    public int overlapBoxsize = 2;
    int rnd;
    int rnd1;
    int rnd2;
    int rnd3;

    private void Start()
    {
        StartCoroutine(Spawn(timeSpawn));
    }


    private void Update()
    {
        speedPlayer = playerController.speedGo;
        distance = playerController.distance;
        timeSpawn = 6 / speedPlayer;
        rnd1 = Random.Range(-rndRange, rndRange);
        rnd = Random.Range(-rndRange, rndRange);
        rnd2 = Random.Range(-rndRange, rndRange);
        rnd3 = Random.Range(-rndRange, rndRange);

        SpawnPos2 = new Vector3(transform.position.x + Random.Range(-rndRange, rndRange), transform.position.y, transform.position.z + Random.Range(-rndRange/2, rndRange/2));
        SpawnPos3 = new Vector3(transform.position.x + Random.Range(-rndRange, -rndRange), transform.position.y + Random.Range(-2f, 2f), transform.position.x + Random.Range(-rndRange/3, rndRange/3));


    }
    

    IEnumerator Spawn(float timeSpawn)
    {
        yield return new WaitForSeconds(timeSpawn);
        int x = 300;

        if (distance >= 0 && distance < x)
        {
            CheckAndSpawn(0, rnd);
            CheckAndSpawn(1, rnd1);
            CheckAndSpawn(2, rnd2);
            CheckAndSpawn(3, rnd3);
        }
        if (distance >= x && distance < 2 * x)
        {
          
        }
        if (distance >= 2 * x )
        {
            
        }
        else
        {
            Instantiate(enemy[0], SpawnPos, Quaternion.Euler(0, 0, 0));
        }

        Repeat();
    }

    private void Repeat()
    {
        StartCoroutine(Spawn(timeSpawn));
    }



    void CheckAndSpawn(int objNum, int rnd)
    {
        SpawnPos = new Vector3(transform.position.x + rnd, transform.position.y, transform.position.z + rnd/2);

        Collider[] hitColliders = Physics.OverlapBox(SpawnPos, new Vector3(overlapBoxsize, overlapBoxsize, overlapBoxsize) , Quaternion.identity, m_LayerMask);
        if (0 == hitColliders.Length)
        {
            Instantiate(enemy[objNum], SpawnPos, Quaternion.identity);
        }
        


    }



}
