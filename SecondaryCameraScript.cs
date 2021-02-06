using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryCameraScript : MonoBehaviour
{
    public GameObject targetToFollow;
    public float distanceZ;
    public float distanceX;
    public float distanceY;
    public Vector3 positionFinal;

    void FixedUpdate()
    {
        positionFinal.x = targetToFollow.transform.position.x + distanceX;
        positionFinal.y = targetToFollow.transform.position.y + distanceY;
        positionFinal.z = targetToFollow.transform.position.z + distanceZ;

        this.transform.position = positionFinal;
    }
}
