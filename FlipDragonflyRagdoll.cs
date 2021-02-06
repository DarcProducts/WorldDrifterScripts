using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipDragonflyRagdoll : MonoBehaviour
{
    private void Start()
    {
        Vector3 tempZ = transform.rotation.eulerAngles;
        tempZ.z = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(tempZ);
    }
}
