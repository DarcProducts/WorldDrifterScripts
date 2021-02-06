using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeControllerMech : MonoBehaviour
{
    public GameObject player;
    public EnemyAI enemyAI;
    public EnemyHealth enemyHealth;
    public EnemyAbilities enemyAbilities;
    public PlayerHealth playerHealth;
    public PlayerController playerController;
    public PlayerAbilities playerAbilities;

    public Animator enemyAnimatorController;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth.currentEnemyHealth = enemyHealth.maxEnemyHealth;
        enemyAnimatorController = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAI.isAttacking)
        {
            if (player.activeSelf == true)
            {
                enemyAnimatorController.SetTrigger("Eye_Shoot");
            }
        }
        else
            enemyAnimatorController.SetTrigger("Eye_Idle");

        if (enemyHealth.currentEnemyHealth <= 0)
        {
            this.gameObject.SetActive(false);
            playerController.killPointsDrone = playerController.killPointsDrone + 1;
        }
    }
}

