using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public Transform target;
    public float thisSpeed;
    public bool isLockedOn;

    enum FacingDirection
    {
        UP = 90,
        DOWN = 270,
        LEFT = 0,
        RIGHT = 180
    }

    void Update()
    {
                                          //LookAt2D(target);
        if (isLockedOn)
        {
            LookAt2D(target, 17f, FacingDirection.RIGHT);
        }
    }

    void LookAt2D(Transform theTarget, float theSpeed, FacingDirection facing)
    {
        Vector3 vectorToTarget = theTarget.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        angle -= (float)facing;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * thisSpeed);
    }
}
