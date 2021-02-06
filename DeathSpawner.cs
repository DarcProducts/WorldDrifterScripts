using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpawner : MonoBehaviour
{
    public GameObject deathObjectToSpawn;
    private EnemyHealth enemyHealth;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }
    private void Update()
    {
        if (enemyHealth.currentEnemyHealth <= 0)
        {
            Instantiate(deathObjectToSpawn, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
