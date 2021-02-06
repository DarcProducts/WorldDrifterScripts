using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private EnemyAI enemyAI;
    private EnemyHealth enemyHealth;
    public PlayerController playerController;

    public GameObject deathObjectToSpawn;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        _animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
    }

    public void Update()
    {
        if (enemyHealth.currentEnemyHealth <= 0)
        {
            playerController.killPointsScorpion = playerController.killPointsScorpion + 1;
            Instantiate(deathObjectToSpawn, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }        
    }

    private void FixedUpdate()
    {
        if (enemyAI.playerTransform.position.x <= 2f && enemyAI.playerTransform.position.x >= 0 && enemyAI.playerTransform.position.y <= 2f && enemyAI.playerTransform.position.y >= 0)
        {
            _animator.SetBool("isWalking", false);

            int number = Random.Range(0, 1);

            if (number == 0)
            {
                _animator.SetBool("isStinging", true);
            }
            if (number == 1)
            {
                _animator.SetBool("isClawing", true);
            }
        }
        else
            _animator.SetBool("isWalking", true);
    }
}
