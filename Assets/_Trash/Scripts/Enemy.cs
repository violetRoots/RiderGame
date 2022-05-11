using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Sprite[] enemySpriteArray;
    public SpriteRenderer enemySprite;
    float maxAngleTurn = 90;
    float angleRot; 

    private void Update()
    {
        angleRot =  transform.rotation.y; 
        EnemySpriteAnim(angleRot);
        Debug.Log(angleRot);
    }

    public void EnemySpriteAnim(float Angle)
    {
        float checkAngle = maxAngleTurn / enemySpriteArray.Length;
        // rigth turn
        if (Angle >= 0) enemySprite.flipX = false;
        if (Angle >= 0 && Angle <= checkAngle) enemySprite.sprite = enemySpriteArray[0];
        if (Angle >= checkAngle && Angle <= 2 * checkAngle) enemySprite.sprite = enemySpriteArray[1];
        if (Angle >= 2 * checkAngle && Angle <= 3 * checkAngle) enemySprite.sprite = enemySpriteArray[2];
        //left turn
        if (Angle < 0) enemySprite.flipX = true;
        if (Angle <= 0 && Angle >= -checkAngle) enemySprite.sprite = enemySpriteArray[0];
        if (Angle <= -checkAngle && Angle >= -2 * checkAngle) enemySprite.sprite = enemySpriteArray[1];
        if (Angle <= -2 * checkAngle && Angle >= -3 * checkAngle) enemySprite.sprite = enemySpriteArray[2];
    }

}
