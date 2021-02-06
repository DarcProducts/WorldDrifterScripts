using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffMesh : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public float distanceToTurnOff;

    private Vector3 distanceFromTarget;

    public GameObject objectOrigin;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    void FixedUpdate()
    {

        distanceFromTarget = this.transform.position - objectOrigin.transform.position;

        if (distanceFromTarget.x >= -distanceToTurnOff && distanceFromTarget.x <= distanceToTurnOff && distanceFromTarget.y >= -distanceToTurnOff && distanceFromTarget.y <= distanceToTurnOff)
        {
            meshRenderer.enabled = true;
        }
        else
            meshRenderer.enabled = false;
    }
}
