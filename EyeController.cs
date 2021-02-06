using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    public EnemyAI enemyAI;
    public EnemyHealth enemyHealth;
    public EnemyAbilities enemyAbilities;
    public PlayerHealth playerHealth;
    public PlayerController playerController;
    public PlayerAbilities playerAbilities;

    public Animator enemyAnimatorController;
    public ExplosionSoundEffectScript explosionSoundEffectScript;

    public GameObject droneExplosion;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimatorController = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAI.isAttacking)
        {
            enemyAnimatorController.SetTrigger("Eye_Shoot");
        }
        else
            enemyAnimatorController.SetTrigger("Eye_Idle");

        if (enemyHealth.currentEnemyHealth <= 0)
        {
            explosionSoundEffectScript.PlayExplosion();
            Instantiate(droneExplosion, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
            playerController.killPointsDrone = playerController.killPointsDrone + 1;
        }
    }
}
