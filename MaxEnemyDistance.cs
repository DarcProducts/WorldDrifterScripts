using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxEnemyDistance : MonoBehaviour
{
    public float distanceToMovebackFrom;

    private Vector3 distanceFromPlayerObject;

    public GameObject playerObject;

    public Vector3 spawningLocation;
    public float distanceToSpawn;

    void FixedUpdate()
    {
        spawningLocation.x = playerObject.transform.position.x;
        spawningLocation.y = playerObject.transform.position.y + distanceToSpawn;
        spawningLocation.z = playerObject.transform.position.z + 8;

        
        distanceFromPlayerObject.x = this.transform.position.x - playerObject.transform.position.x;
        distanceFromPlayerObject.y = this.transform.position.y - playerObject.transform.position.y;
        distanceFromPlayerObject.z = this.transform.position.z - playerObject.transform.position.z;

        if (distanceFromPlayerObject.x <= -distanceToMovebackFrom && distanceFromPlayerObject.x >= distanceToMovebackFrom && distanceFromPlayerObject.y <= -distanceToMovebackFrom && distanceFromPlayerObject.y >= distanceToMovebackFrom)
        {
            ResetLocation();
        }
    }

    public void ResetLocation()
    {
        this.transform.position = spawningLocation;
    }
}
