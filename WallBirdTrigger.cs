using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBirdTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Background Birds")
        {
            Debug.Log("Birds Entered!");
        }
    }
}
