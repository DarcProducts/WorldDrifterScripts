using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosasaurController : MonoBehaviour
{
    public EnemyAI enemyAI;
    
    public LayerMask waterArea;
    public bool isInWater = true;
    private float distanceToCheck = .1f;

    public float enemySpeedRun;
    public float distanceToSwimAway;

    public float maxDepthToSwim;

    private Animator myAnimator;

    private bool isSwimmingAway = false;

    public float maxDistanceToSwimHorizontal;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public IEnumerator Swim()
    {
        yield return new WaitForSecondsRealtime(distanceToSwimAway);
        isSwimmingAway = false;
    }
    public void SwimAwayFromPlayer()
    {
        transform.Translate(0, -Time.deltaTime * enemySpeedRun, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            isSwimmingAway = true;
            StartCoroutine(Swim());
        }
    }

    public void FixedUpdate()
    {
        if (this.transform.position.x >= maxDistanceToSwimHorizontal)
        {
            this.transform.Translate(Time.deltaTime * enemySpeedRun, 0, 0);
        }
        if (this.transform.position.x <= -maxDistanceToSwimHorizontal)
        {
            this.transform.Translate(-Time.deltaTime * enemySpeedRun, 0, 0);
        }

        if (this.transform.position.y >= 0)
        {
            this.transform.Translate(0, -Time.deltaTime * enemySpeedRun, 0);
        }
        if (this.transform.position.x <= -maxDepthToSwim)
        {
            this.transform.Translate(0, Time.deltaTime * enemySpeedRun, 0);
        }

        if (isSwimmingAway)
        {
            SwimAwayFromPlayer();
        }

        isInWater = Physics2D.Raycast(this.transform.position, Vector2.up, distanceToCheck, waterArea);

        if (isInWater == false)
        {
            enemyAI.MoveDownY();
        }

        if (enemyAI.isTargetingMeleeRadius)
        {
            myAnimator.SetBool("isBiting", true);
        }
        else
            myAnimator.SetBool("isBiting", false);
    }
}
