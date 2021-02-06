using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public PlayerAbilities playerAbilities;
    public EnemyHealth enemyHealth;
    public EnemyAI enemyAI;
    public float maxSpawnWait;
    private float spawnTimeWait;
    public float currentSpawnTime;
    public float spawnTimeRate = 0f;
    public Vector2 newPosition;
    public bool relativeToThisObject;
    public float spawningRange;

    public float currentDestructTime;
    public float destroyWaitRate;

    public bool canBeDestroyed;

    public void SpawnEnemyOne()
    {
        Vector2 finalPosition = newPosition;

        newPosition.x = Random.Range(-spawningRange, spawningRange);
        newPosition.y = Random.Range(-spawningRange, spawningRange);

        if (relativeToThisObject)
        {
            finalPosition = (Vector2)transform.position + newPosition;
        }

        GameObject enemyOneCopy = Instantiate<GameObject>(enemyToSpawn);

        if (enemyOneCopy != null)
        {
            enemyOneCopy.transform.position = finalPosition;
            enemyOneCopy.SetActive(true);
        }
    }

    void Start()
    {
        currentSpawnTime = maxSpawnWait;
        spawnTimeWait = maxSpawnWait;
    }

    void FixedUpdate()
    {
        currentDestructTime = currentDestructTime - destroyWaitRate;

        currentSpawnTime = spawnTimeWait;

        if (spawnTimeWait >= 0)
        {
            spawnTimeWait = spawnTimeWait - spawnTimeRate;
        }
        if (spawnTimeWait <= 0)
        {
            SpawnEnemyOne();
            currentSpawnTime = maxSpawnWait;
        }

        if (canBeDestroyed)
        {
            if (currentDestructTime <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Update()
    {
        if (spawnTimeWait <= 0)
        {
            if (currentSpawnTime >= 0)
            {
                SpawnEnemyOne();
            }
            if (spawnTimeWait <= 0)
            {
                spawnTimeWait = maxSpawnWait;
            }
        }
    }

}