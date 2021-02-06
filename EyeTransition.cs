using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTransition : MonoBehaviour
{
    public Animator thisAnimator;
    public MechController mechController;
    public bool isActivated;
    // Start is called before the first frame update
    void Start()
    {
        thisAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            thisAnimator.SetBool("isActivated", true);
        }
    }
}
