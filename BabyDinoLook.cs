using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyDinoLook : MonoBehaviour
{
    public LayerMask playerToLookAt;

    private bool playerToLeft = true;
    private bool playerToRight = false;

    public GameObject detector;

    private Animator myAnimator;

    private float distanceToCheck = 40f;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        playerToRight = Physics2D.Raycast(detector.transform.position, Vector2.right, distanceToCheck, playerToLookAt);
        playerToLeft = Physics2D.Raycast(detector.transform.position, Vector2.left, distanceToCheck, playerToLookAt);

        if (playerToRight == true)
        {
            myAnimator.SetBool("lookingLeft", false);
            myAnimator.SetBool("lookingRight", true);
        }
        if (playerToLeft == true)
        {
            myAnimator.SetBool("lookingRight", false);
            myAnimator.SetBool("lookingLeft", true);
        }
    }
}
