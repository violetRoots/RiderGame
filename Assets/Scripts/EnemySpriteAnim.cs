using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteAnim : MonoBehaviour
{

    public GameObject enemyBody;
    float angleTurn;
    public Sprite[] enemySprites;
    public SpriteRenderer Enemysprite;
    public Sprite deadSprite;
    public bool isGround = true;
    public bool dead = false;

    public void Start()
    {
        Enemysprite.sprite = enemySprites[0];
    }

    private void Update()
    {

        float angle;
        Vector3 axis;
        enemyBody.transform.rotation.ToAngleAxis(out angle, out axis);
        angleTurn = angle;
        //SpriteAnim(angleTurn);

       
    }

    public void SpriteAnim(float angleTurn)
    {
        if (dead == false)
        {
            if (angleTurn < 181)
            {
                Enemysprite.flipX = false;
                if (angleTurn < 180 && angleTurn > 175) Enemysprite.sprite = enemySprites[1];
                if (angleTurn < 175 && angleTurn > 160) Enemysprite.sprite = enemySprites[2];
                if (angleTurn < 160) Enemysprite.sprite = enemySprites[3];
            }
            if (angleTurn > 181)
            {
                Enemysprite.flipX = true;
                if (angleTurn > 182 && angleTurn < 185) Enemysprite.sprite = enemySprites[1];
                if (angleTurn > 185 && angleTurn < 200) Enemysprite.sprite = enemySprites[2];
                if (angleTurn > 200) Enemysprite.sprite = enemySprites[3];
            }
            else
            {
                Enemysprite.sprite = enemySprites[0];
            }


        }

        if (dead)
        {
            Enemysprite.sprite = deadSprite;
        }


    }
}
    

