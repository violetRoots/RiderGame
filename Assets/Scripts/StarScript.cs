using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarScript : MonoBehaviour
{
    
    GameController GC;
    public AudioClip starSoundClip;
    public string NameCount;


    private void Start()
    {
        GC = GameObject.Find("Canvas").GetComponent<GameController>();
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (NameCount == "1") GC.CubeCount++;
            else GC.StarsCount++;
            float volume = 1f;
            AudioSource.PlayClipAtPoint(starSoundClip, transform.position, volume );
            Destroy(gameObject);

        }
       

    }


}
