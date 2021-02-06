using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatsScript : MonoBehaviour
{
    public float flyingSpeed;
    public float maxDistance;

    private bool flyingRight = true;
    private bool flyingLeft = false;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (flyingRight)
        {
            FlyingRight();
        }
        if (flyingLeft)
        {
            FlyingLeft();
        }

        if (this.transform.position.x <= -maxDistance)
        {
            flyingLeft = false;
            flyingRight = true;
        }
        if (this.transform.position.x >= maxDistance)
        {
            flyingRight = false;
            flyingLeft = true;
        }
    }
        void FlyingRight()
        {
            spriteRenderer.flipX = true;
            transform.Translate(Time.deltaTime * flyingSpeed, 0, 0);
        }
        void FlyingLeft()
        {
            spriteRenderer.flipX = false;
            transform.Translate(-Time.deltaTime * flyingSpeed, 0, 0);
        }
}
