
using UnityEngine;
using UnityEngine.Events;

public class JumpController : MonoBehaviour
{ 
    [SerializeField] public LayerMask m_WhatIsGround;
    [SerializeField] public Transform m_GroundCheck;                         // A position marking where to check if the player is grounded.


    const float k_GroundedRadius = 1f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }


    private void Awake()
    { 
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }
}