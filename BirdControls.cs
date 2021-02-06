using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControls : MonoBehaviour
{
    public EnemyAI enemyAI;
    public EnemyHealth enemyHealth;
    public Animator enemyAnimatorController;
    public PlayerController playerController;

    public GameObject deathObjectToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimatorController = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.currentEnemyHealth <= 0)
        {
            playerController.killPointsBird = playerController.killPointsBird + 1;
            Instantiate(deathObjectToSpawn, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
        if (enemyAI.isAttackingMelee == true)
        {
            enemyAnimatorController.SetTrigger("isBiting");
        }
        if (enemyAI.isMoving || enemyAI.isAttackingMelee == false)
        {
            enemyAnimatorController.SetTrigger("isRunning");
        }
    }
}
