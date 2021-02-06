using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffSpriteCamera : MonoBehaviour
{
    private SpriteRenderer meshRenderer;

    public float distanceToTurnOff;

    private Vector3 distanceFromTarget;
    private Vector3 distanceFromPlayer;

    public bool usesWorldBCamera = false;

    public GameObject player;
    public GameObject cameraTarget;

    void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (cameraTarget == null)
        {
            if (usesWorldBCamera)
            {
                cameraTarget = GameObject.FindWithTag("Second Camera");
            }
            else
            cameraTarget = GameObject.FindWithTag("First Camera");
        }

        meshRenderer = GetComponent<SpriteRenderer>();
        meshRenderer.enabled = false;
    }

    void FixedUpdate()
    {
        distanceFromPlayer = this.transform.position - player.transform.position;
        distanceFromTarget = this.transform.position - cameraTarget.transform.position;

        if (distanceFromTarget.x >= -distanceToTurnOff && distanceFromTarget.x <= distanceToTurnOff && distanceFromTarget.y >= -distanceToTurnOff && distanceFromTarget.y <= distanceToTurnOff)
        {
            EnableRenderer();
        }
        else if (distanceFromPlayer.x >= -distanceToTurnOff && distanceFromPlayer.x <= distanceToTurnOff && distanceFromPlayer.y >= -distanceToTurnOff && distanceFromPlayer.y <= distanceToTurnOff)
        {
            EnableRenderer();
        }
        else
            DisableRenderer();
    }

    void EnableRenderer()
    {
        meshRenderer.enabled = true;
    }

    void DisableRenderer()
    {
        meshRenderer.enabled = false;
    }
}
