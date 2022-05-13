using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAnimator : MonoBehaviour
{

    public GameObject enemyBody;
    public EnemyController enemyController;
    public SpriteRenderer spriteRenderer;
    public bool dead = false;
    public Animator animator;
    public float Angle;

    public void Start()
    {
       
    }

    private void Update()
    {


        // animation by animator
        enemyController = enemyBody.GetComponent<EnemyController>();
        Angle = enemyController.EnemyRotY;
        //Angle = (Angle > 180) ? Angle - 360 : Angle;


        animator.SetFloat("Angle", Mathf.Abs(Angle));
        if (enemyController.targetXpos < 0) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;
        
    }

    
    }




