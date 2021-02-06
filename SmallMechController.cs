using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMechController : MonoBehaviour
{
    public GameObject mech;
    public GameObject player;
    public Animator enemyAnimatorController;
    public EnemyAI enemyAI;
    public EnemyHealth enemyHealth;
    public PlayerController playerController;

    public GameObject mechExplosion;

    public bool hasAnimation;

    public bool countsAsAlphaMech;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAI.playerToLeft)
        {
            if (enemyAI.canAttackWithRanged)
            {
                enemyAI.canMoveRight = false;
                Vector3 tempY = transform.rotation.eulerAngles;
                tempY.y = 0.0f;
                transform.rotation = Quaternion.Euler(tempY);
            }
        }
        if (enemyAI.playerToRight)
        {
            enemyAI.canMoveLeft = false;
            Vector3 tempY = transform.rotation.eulerAngles;
            tempY.y = 180.0f;
            transform.rotation = Quaternion.Euler(tempY);
        }
        if (enemyHealth.currentEnemyHealth <= 0)
        {
            if (countsAsAlphaMech == false)
            {
                playerController.killPointsMech = playerController.killPointsMech + 1;
                Instantiate(mechExplosion, this.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0.0f, 360f)));
                Destroy(this.gameObject);
            }
            if (countsAsAlphaMech)
            {
                playerController.killPointsAlphaMech = playerController.killPointsAlphaMech + 1;
                Instantiate(mechExplosion, this.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0.0f, 360f)));
                Destroy(this.gameObject);
            }
        }

        if (enemyAI.isMoving)
        {
            if (hasAnimation)
            {
                enemyAnimatorController.SetBool("isIdle", false);
                enemyAnimatorController.SetBool("isRunning", true);
            }
        }
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

                if (hasAnimation)
                {
                    enemyAnimatorController.SetBool("isRunning", false);
                    enemyAnimatorController.SetBool("isIdle", true);
                }
            }
            else
            {
                enemyAI.isIdle = true;
                enemyAI.isMoving = true;
                enemyAI.canMoveRight = true;
                enemyAI.canMoveLeft = true;
                if (player.activeSelf)
                {
                    enemyAI.canMoveToward = true;
                }
            }
        }
    }
}
