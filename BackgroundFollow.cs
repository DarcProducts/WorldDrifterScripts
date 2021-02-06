using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public GameObject target;
    public float offsetFromTarget;
    public Vector3 newPosition;


    void FixedUpdate()
    {
        newPosition.z = offsetFromTarget;
        newPosition.x = target.transform.position.x;
        newPosition.y = target.transform.position.y;

        this.transform.position = newPosition;
    }
}
