using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public GameObject smallSprite;
    SpriteRenderer sprite;
    public Sprite[] spriteList;

    void Start()
    {
        sprite = smallSprite.GetComponent<SpriteRenderer>();
        int rnd = Random.Range(0, spriteList.Length);
        sprite.sprite = spriteList[rnd];


        // random flip
        var myBool = (Random.value < 0.5);
        if (myBool) sprite.flipX = true;
        else sprite.flipX = false;

    }

}
