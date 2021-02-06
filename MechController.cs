using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechController : MonoBehaviour
{
    public GameObject mech;
    public GameObject player;
    public Animator enemyAnimatorController;
    public EnemyAI enemyAI;
    public EnemyHealth enemyHealth;
    public PlayerController playerController;
    public EyeTransition eyeTransitionDrone1;
    public EyeTransition eyeTransitionDrone2;
    public EyeTransition eyeTransitionDrone3;
    public bool hasDrones;
    private int drones = 3;
    public bool drone1Activated;
    public bool drone2Activated;
    public bool drone3Activated;
    public GameObject drone1NonPowered;
    public GameObject drone2NonPowered;
    public GameObject drone3NonPowered;
    public GameObject drone1Powered;
    public GameObject drone2Powered;
    public GameObject drone3Powered;

    public bool hasAnimation;

    public bool countsAsAlphaMech;

    public float launchSequenceDuration;

    public GameObject deathObjectToSpawn;

    public WinScreenScript winScreen;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAI.playerToLeft)
        {
            if (enemyAI.canAttackWithRanged)
            {
                enemyAI.canMoveRight = false;
                Vector3 tempY = transform.rotation.eulerAngles;
                tempY.y = 0.0f;
                transform.rotation = Quaternion.Euler(tempY);
            }
        }
        if (enemyAI.playerToRight)
        {
            if (enemyAI.canAttackWithRanged)
            {
                enemyAI.canMoveLeft = false;
                Vector3 tempY = transform.rotation.eulerAngles;
                tempY.y = 180.0f;
                transform.rotation = Quaternion.Euler(tempY);
            }
        }
        if (enemyHealth.currentEnemyHealth <= 0)
        {
            if (countsAsAlphaMech == false)
            {
                playerController.killPointsMech = playerController.killPointsMech + 1;
            }
            if (countsAsAlphaMech)
            {
                playerController.killPointsAlphaMech = playerController.killPointsAlphaMech + 1;
            }

            Instantiate(deathObjectToSpawn, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
            winScreen.alphaMechDead = true;
        }

        if (enemyAI.isMoving)
        {
            if (hasAnimation)
            {
                enemyAnimatorController.SetBool("isIdle", false);
                enemyAnimatorController.SetBool("isRunning", true);
            }
        }
    }
    
    void LateUpdate()
    {
        if (player.activeSelf == true)
        {
            if (enemyAI.isPlayerWithinThisRange)
            {
                enemyAI.canMoveRight = false;
                enemyAI.canMoveLeft = false;
                enemyAI.canMoveToward = false;
                enemyAI.isMoving = false;
                enemyAI.isIdle = true;

                if (hasAnimation)
                {
                    enemyAnimatorController.SetBool("isRunning", false);
                    enemyAnimatorController.SetBool("isIdle", true);
                }
            }
            else
            {
                enemyAI.isIdle = true;
                enemyAI.isMoving = true;
                enemyAI.canMoveRight = true;
                enemyAI.canMoveLeft = true;
                if (player.activeSelf)
                {
                    enemyAI.canMoveToward = true;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Charged Bullet"))
        {
            eyeTransitionDrone3.isActivated = true;
            StartCoroutine(LaunchSequence());
        }
    }

    public IEnumerator LaunchSequence()
    {
        yield return new WaitForSecondsRealtime(launchSequenceDuration);
        LaunchDrone3();
        eyeTransitionDrone2.isActivated = true;
        StartCoroutine(LaunchSequence2());
    }
    public IEnumerator LaunchSequence2()
    {
        yield return new WaitForSecondsRealtime(launchSequenceDuration);
        LaunchDrone2();
        eyeTransitionDrone1.isActivated = true;
        StartCoroutine(LaunchSequence3());
    }
    public IEnumerator LaunchSequence3()
    {
        yield return new WaitForSecondsRealtime(launchSequenceDuration);
        LaunchDrone1();
    }

    void LaunchDrone3()
    {
        if (drone3NonPowered.activeSelf && drones >= 0)
        {
            eyeTransitionDrone3.isActivated = true;
            drone3Powered.transform.position = drone3NonPowered.transform.position;
            drone3Powered.SetActive(true);
            drone3NonPowered.SetActive(false);
            drones = drones - 1;
            drone3Activated = true;
        }
    }
    void LaunchDrone2()
    {
        if (drone2NonPowered.activeSelf && drones >= 0)
        {
            eyeTransitionDrone2.isActivated = true;
            drone2Powered.transform.position = drone2NonPowered.transform.position;
            drone2Powered.SetActive(true);
            drone2NonPowered.SetActive(false);
            drones = drones - 1;
            drone2Activated = true;
        }
    }
    void LaunchDrone1()
    {
        if (drone1NonPowered.activeSelf && drones >= 0)
        {
            eyeTransitionDrone1.isActivated = true;
            drone1Powered.transform.position = drone1NonPowered.transform.position;
            drone1Powered.SetActive(true);
            drone1NonPowered.SetActive(false);
            drones = drones - 1;
            drone1Activated = true;
        }
    }
}
