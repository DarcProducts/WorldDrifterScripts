using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldBFollow : MonoBehaviour
{
    public GameObject player;
    public float offsetFromPlayer;
    public Vector3 newPosition;


    void FixedUpdate()
    {
        newPosition.z = 0f;
        newPosition.x = player.transform.position.x + offsetFromPlayer;
        newPosition.y = player.transform.position.y;

        this.transform.position = newPosition;
    }
}
