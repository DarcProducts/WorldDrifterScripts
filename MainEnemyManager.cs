using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemyManager : MonoBehaviour
{
    public List<GameObject> myEnemyObjects;
    public int numberToSpawn;
    private int maxNumberToSpawn;
    public float timeBetweenSpawns = 10f;

    private void Start()
    {
        maxNumberToSpawn = myEnemyObjects.Count;
        StartCoroutine(StartSpawnTimer());
    }

    private IEnumerator StartSpawnTimer()
    {
        yield return new WaitForSecondsRealtime(timeBetweenSpawns);
        if (myEnemyObjects[numberToSpawn].activeSelf == false)
        {
            myEnemyObjects[numberToSpawn].SetActive(true);
        }
        ResetSpawnTimer(); 
    }

    private void ResetSpawnTimer()
    {
        StartCoroutine(StartSpawnTimer());
    }

    private void FixedUpdate()
    {
        numberToSpawn = Random.Range(0, maxNumberToSpawn);
    }
}
