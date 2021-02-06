using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerController playerController;
    private Animator myAnimator;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        myAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
 
    }
}
