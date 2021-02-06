using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflyController : MonoBehaviour
{
    public PlayerController playerController;
    public EnemyHealth enemyHealth;
    public GameObject deathObjectToSpawn;

    private void Update()
    {
        if (this.gameObject != null)
        {
            if (enemyHealth.currentEnemyHealth <= 0)
            {
                playerController.killPointsDragonfly = playerController.killPointsDragonfly + 1;
                Instantiate(deathObjectToSpawn, this.transform.position, this.transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }
}