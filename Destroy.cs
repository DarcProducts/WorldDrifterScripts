using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject thisObject;
    public float timeTillDestruction;
    public bool canDestruct;

    void Update()
    {
        if (canDestruct == true)
        {
            Destroy(thisObject, timeTillDestruction);
        }
    }
}
