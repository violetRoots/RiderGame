using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    
    PlayerController PC;
    GameObject player;
    public GameObject[] enemy;
    public float timeSpawnEnemy_0 = 1;
    public float timeSpawnEnemy_1 = 1;

    void Start()
    {
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
        player = GameObject.Find("Player");
        StartCoroutine(SpawnFast(timeSpawnEnemy_0, 1));
        StartCoroutine(SpawnSlow(timeSpawnEnemy_1));
        StartCoroutine(SpawnFast(timeSpawnEnemy_1 ,2));
    }

    
    IEnumerator SpawnFast(float timeSpawnFastEnemy, int x)
    {
        yield return new WaitForSeconds(timeSpawnFastEnemy);

        // fast enemies spawn
        Instantiate(enemy[x], PointCalculate(15), Quaternion.Euler(0, 180, 0));
        Instantiate(enemy[x], PointCalculate(15), Quaternion.Euler(0, 180, 0));
        Instantiate(enemy[x], PointCalculate(15), Quaternion.Euler(0, 180, 0));
        RepeatFast(x);
    }

    private void RepeatFast(int x)
    {
        StartCoroutine(SpawnFast(timeSpawnEnemy_0, x));
    }


    IEnumerator SpawnSlow(float timeSpawnSlowEnemy)
    {
        yield return new WaitForSeconds(timeSpawnSlowEnemy);


        // slow enemies spwan
        int x = 0;
        Instantiate(enemy[x], PointCalculate(-20), Quaternion.Euler(0, 180, 0));
        Instantiate(enemy[x], PointCalculate(-20), Quaternion.Euler(0, 180, 0));
        Instantiate(enemy[x], PointCalculate(-20), Quaternion.Euler(0, 180, 0));
        RepeatSlow();
    }

    private void RepeatSlow()
    {
        StartCoroutine(SpawnSlow(timeSpawnEnemy_1));
    }


    // calculate random spawn point
    Vector3 PointCalculate(float z)
    {
        int rnd = Random.Range(-50, 50);
        Vector3 point = player.transform.position + new Vector3(rnd, 0, z);
        return point;
    }
}
