using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speedGo;
    public float speedRotate;
    public float mouseX;
    public float maxAngleTurn = 60;
    public bool isGround;
    public bool isDead;
    public bool fallen;
    public bool fireball;
    public float distance;
    public bool immortal;
    public Vector3 PlayerPosition;
    private Rigidbody rb;
    GameController GC;
    [SerializeField] GameObject _snowball;
    SwipeControlRotation SCR;

    private void Start()
    {
        isGround = true;
        isDead = false;
        rb = gameObject.GetComponent<Rigidbody>();
        GC = GameObject.Find("Canvas").GetComponent<GameController>();
        SCR = GameObject.Find("Player").GetComponent<SwipeControlRotation>();
        
    }

    private void Update()
    {
        distance = Mathf.RoundToInt(Vector3.Distance(new Vector3(0, 0, 0), transform.position));
        PlayerPosition = transform.position;
        
    }
   
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Tree" || collision.gameObject.tag == "Enemy")
        {
            if(immortal==false) Fallen();
        }
       
    }

    private void IsDead()
    {

        isDead = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GC.Replay();
        SCR.speedGo = 0;

        GetComponent<SFX>().PlaySound(0);
        GetComponent<CapsuleCollider>().enabled = false;
    }
    private void Fallen()
    {

        fallen = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        SCR.speedGo = 0;
       
        GetComponent<SFX>().PlaySound(0);


    }

    public void GetUp ()
    {
        
        fallen = false;
        immortal = true;
        print("no");
        StartCoroutine(immortalTime());
        if (immortal==false) StopCoroutine(immortalTime());
        SCR.speedGo = 10;
    }

    public void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, 80, 0), ForceMode.Impulse);
            isGround = false;
        }
        

    }

    public void Fire()
    {

        Instantiate(_snowball, transform.position + new Vector3(0, 0.5f, -0.5f), Quaternion.Euler(45, 0, 45));
    }


    public IEnumerator immortalTime()
    {
        
        yield return new WaitForSeconds(3f);
        immortal = false;
    }




}
