using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    NavMeshAgent agent;
    public Vector3 target;
    public Animator Animator;
    public GameObject parent;
    public GameObject enemysprite;
    public GameObject item;
    public float targetXpos;
    public float EnemyRotY;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // направления движения и скорость
        targetXpos = Random.Range(-5f, 5f);
        
    }
    

    private void Update()
    {
        
        target = new Vector3(targetXpos, 0, 3);
        agent.SetDestination(transform.position - target);
        EnemyRotY = transform.localEulerAngles.y;
        print(EnemyRotY);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetXpos = Random.Range(-5f, 5f);
            print("random targetXpos");
        }
    }

    public void Death()
    {
        agent.isStopped = true;
        enemysprite.GetComponent<EnemySpriteAnim>().dead = true;
        gameObject.GetComponent<Collider>().enabled = false;
        Instantiate(item, transform.position, Quaternion.Euler(0,90,0));
        Destroy(parent, 1f);
    }


}
