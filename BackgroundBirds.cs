using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBirds : MonoBehaviour
{

    public float birdSpeed;

    public bool _flyLeft;

    public bool _flyRight;

    public float distanceToTravel;

    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {

        if (this.transform.position.x >= distanceToTravel)
        {
            _flyRight = false;
            _flyLeft = true;
        }
        if (this.transform.position.x <= -distanceToTravel)
        {
            _flyLeft = false;
            _flyRight = true;
        }

        if (_flyLeft)
        {
            spriteRenderer.flipX = false;
            FlyLeft();
        }
        if (_flyRight)
        {
            spriteRenderer.flipX = true;
            FlyRight();
        }
    }

    void FlyRight()
    {
        transform.Translate(Time.deltaTime * birdSpeed, 0, 0); //move right
    }

    void FlyLeft()
    {
        transform.Translate(-Time.deltaTime * birdSpeed, 0, 0); //move left
    }
}
