using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToSpawner : MonoBehaviour
{
    public Vector3 finalPosition;

    public List<GameObject> theseGameObjects;

    public float minRange;
    public float maxRange;

    public int spawnTimer;

    private void Awake()
    {
        finalPosition.z = 4f;
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < theseGameObjects.Count; ++i)
        {
            if (theseGameObjects[i].activeSelf == false)
            {
                finalPosition.x = Random.Range(minRange, maxRange);
                finalPosition.y = Random.Range(minRange, maxRange);
                theseGameObjects[i].transform.position = finalPosition;
            }
        }
    }
 }
