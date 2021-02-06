using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffSprite : MonoBehaviour
{
    private SpriteRenderer meshRenderer;

    public float distanceToTurnOff;

    private Vector3 distanceFromTarget;

    public GameObject objectOrigin;

    public bool isPlayerNear = false;

    void Awake()
    {
        meshRenderer = GetComponent<SpriteRenderer>();
        meshRenderer.enabled = false;
    }

    void FixedUpdate()
    {

        distanceFromTarget = this.transform.position - objectOrigin.transform.position;

        if (distanceFromTarget.x >= -distanceToTurnOff && distanceFromTarget.x <= distanceToTurnOff && distanceFromTarget.y >= -distanceToTurnOff && distanceFromTarget.y <= distanceToTurnOff)
        {
            isPlayerNear = true;
            meshRenderer.enabled = true;
        }
        else
        {
            isPlayerNear = false;
            meshRenderer.enabled = false;
        }
    }
    
}
