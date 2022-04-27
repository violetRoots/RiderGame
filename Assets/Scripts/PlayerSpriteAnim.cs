using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteAnim : MonoBehaviour
{
    PlayerController PC;
    SwipeControl SC;
    SwipeControlRotation SwipeControlRotation;
    public Sprite[] playerSpritesArray;
    public Sprite deadSprite;
    public Sprite fireSprite;
    public Sprite jumpSprite;
    public float mouseX;
    public float Angle;
    public Animator animator;
    Vector2 delta;
    public float maxAngleTurn;
    public bool isGround=true;
    SpriteRenderer PlayerSprite;
    bool isDead;
    bool fallen;
    bool isFire;

    public void Start()
    {
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
        PlayerSprite = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        
    }


    private void Update()
    {
        //maxAngleTurn = PC.maxAngleTurn;
        //mouseX = PC.mouseX;

        SwipeControlRotation = GameObject.Find("Player").GetComponent<SwipeControlRotation>();
        fallen = PC.fallen;
        isDead = PC.isDead;
        //SpriteAnim(SwipeControlRotation.angleRot);

        // animation by animator
        Angle = SwipeControlRotation.angleRot;
        animator.SetFloat("Angle", Mathf.Abs(Angle));
        if (Angle < 0) PlayerSprite.flipX = true;
        if (Angle > 0) PlayerSprite.flipX = false;
    }

    public void SpriteAnim(float Angle)
    {
        // автоматическое делением угла поворота на количество спрайтов
        float curAngle = 60 / 4;
        if (!isDead && !fallen)
        {
            // rigth turn
            if (Angle >= 0) PlayerSprite.flipX = true;
            if (Angle >= 0 && Angle <= curAngle) 
            if (Angle >= curAngle && Angle <= 2 * curAngle) 
            if (Angle >= 2 * curAngle && Angle <= 3 * curAngle) 
            //left turn
            if (Angle < 0) PlayerSprite.flipX = false;
            if (Angle <= 0 && Angle >= -curAngle) PlayerSprite.sprite = playerSpritesArray[0];
            if (Angle <= -curAngle && Angle >= -2 * curAngle) PlayerSprite.sprite = playerSpritesArray[1];
            if (Angle <= -2 * curAngle && Angle >= -3 * curAngle) PlayerSprite.sprite = playerSpritesArray[2];
        }

        //if (!isDead)
        //{

        //    // rigth turn
        //    if (Angle == 0) PlayerSprite.sprite = playerSpritesArray[0];
        //    if (Angle < 0) PlayerSprite.flipX = false;
        //    if (Angle < -1 && Angle > -8) PlayerSprite.sprite = playerSpritesArray[1];
        //    if (Angle < -8 ) PlayerSprite.sprite = playerSpritesArray[2];

        //    // left turn
        //    if (Angle == 0) PlayerSprite.sprite = playerSpritesArray[0];
        //    if (Angle > 0) PlayerSprite.flipX = true;
        //    if (Angle > 1 && Angle < 8) PlayerSprite.sprite = playerSpritesArray[1];
        //    if (Angle > 8 ) PlayerSprite.sprite = playerSpritesArray[2];

        //}


        if (PC.immortal)
        {
            PlayerSprite.color = Color.grey;
        }
        else PlayerSprite.color = Color.white;

        if (isDead)
        {
            PlayerSprite.sprite = deadSprite;
            PlayerSprite.sortingOrder = 2;
        }

        if (fallen)
        {
            PlayerSprite.sprite = deadSprite;
            PlayerSprite.sortingOrder = 2;
        }
        else
        {
            PlayerSprite.sortingOrder = 1;
        }

    }
}
