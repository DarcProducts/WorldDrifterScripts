using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    public Animator ratController;
    public EnemyAI enemyAI;
    public EnemyHealth enemyHealth;
    public PlayerController playerController;
    public SpriteRenderer enemyRenderer;
    public GameObject player;

    public Animator enemyAnimatorController;

    public Color colorWhenAttacking;
    public Color oldColor;

    public GameObject deadRat;

    public bool hasAnimation;

    void Start()
    {
        oldColor = enemyRenderer.color;
        ratController = GetComponent<Animator>();
    }

    void Update()
    {
        if (enemyHealth.currentEnemyHealth <= 0)
        {
            Destroy(this.gameObject);
            playerController.killPointsRat = playerController.killPointsRat + 1;
        }

        if (enemyAI.moveRightX)
        {
            Vector3 tempY = transform.rotation.eulerAngles;
            tempY.y = 0.0f;
            transform.rotation = Quaternion.Euler(tempY);
        }
        else if (enemyAI.moveLeftX)
        {
            Vector3 tempY = transform.rotation.eulerAngles;
            tempY.y = 180.0f;
            transform.rotation = Quaternion.Euler(tempY);
        }
        else if (enemyAI.isTrackingPlayer || enemyAI.isAttacking)
        {
            if (enemyAI.playerToLeft)
            {
                Vector3 tempY = transform.rotation.eulerAngles;
                tempY.y = 0.0f;
                transform.rotation = Quaternion.Euler(tempY);
            }
            if (enemyAI.playerToRight)
            {
                Vector3 tempY = transform.rotation.eulerAngles;
                tempY.y = 180.0f;
                transform.rotation = Quaternion.Euler(tempY);
            }
        }

    }

    void FixedUpdate()
    {
        if (enemyAI.isTrackingPlayer)
        {
            enemyRenderer.color = colorWhenAttacking;
        }
        else
            enemyRenderer.color = oldColor;
    }

    void LateUpdate()
    {
        if (player.activeSelf == true)
        {
            if (enemyAI.isPlayerWithinThisRange)
            {
                enemyAI.canMoveRight = false;
                enemyAI.canMoveLeft = false;
                enemyAI.canMoveToward = false;
                enemyAI.isMoving = false;
                enemyAI.isIdle = true;
            }
            else
            {
                enemyAI.canMoveRight = true;
                enemyAI.canMoveLeft = true;

                if (player.activeSelf)
                {
                    if (enemyAI.isAttacking == true)
                    {
                        enemyAI.canMoveToward = true;
                    }
                }
            }
        }

        if (enemyAI.isIdle)
        {
            ratController.SetTrigger("isIdle");
        }

        if (enemyAI.isMoving)
        {
            ratController.SetTrigger("isRunning");
        }

        if (enemyAI.isAttackingMelee)
        {
            ratController.SetTrigger("isBiting");
        }
    }
}
