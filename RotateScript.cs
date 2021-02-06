using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{ 
    public float rotateSpeed;
    public bool rotateOnXAxis;
    public bool rotateOnYAxis;
    public bool canRotate;

    void Update()
    {
        if (canRotate)
        {
            if (rotateOnXAxis)
            {
                transform.Rotate(Vector2.down * rotateSpeed * Time.deltaTime);
            }
            if (rotateOnYAxis)
            {
                transform.Rotate(Vector2.right * rotateSpeed * Time.deltaTime);
            }
        }

    }
}
