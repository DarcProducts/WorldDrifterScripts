using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public GameObject objectToFollow;
    public float xOffset;
    public float yOffset;
    public float zOffset;
    public Vector3 finalPosition;

    void FixedUpdate()
    {
        finalPosition.x = objectToFollow.transform.position.x + xOffset;
        finalPosition.y = objectToFollow.transform.position.y + yOffset;
        finalPosition.z = objectToFollow.transform.position.z + zOffset;

        this.transform.position = finalPosition;
    }
}
