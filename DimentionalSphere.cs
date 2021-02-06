using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimentionalSphere : MonoBehaviour
{
    public GameObject player;
    public float distanceFromPlayer = 4f;
    private Vector3 newPosition;

    void Start()
    {
        newPosition.z = distanceFromPlayer;
        newPosition.x = player.transform.position.x;
        newPosition.y = player.transform.position.y;
        this.transform.position = newPosition;
    }

    void FixedUpdate()
    {
        newPosition.z = distanceFromPlayer;
        newPosition.x = player.transform.position.x;
        newPosition.y = player.transform.position.y;

        this.transform.position = newPosition;
    }
}
