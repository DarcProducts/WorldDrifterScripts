using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Tooltip("player gameobject")]
    public GameObject player;
    [Tooltip("enemy abilities script")]
    public EnemyAbilities enemyAbilities;
    [Tooltip("enemy health script")]
    public EnemyHealth enemyHealth;
    [Tooltip("enemy controller script")]
    public EnemyController enemyController;
    [Tooltip("player health script")]
    public PlayerHealth playerHealth;
    [Tooltip("set to whatever you would like the basic damage to be")]
    public float basicDamage = 0f;
    [Tooltip("set to whatever you would like the special damage to be")]
    public float specialDamage = 0f;
    [Tooltip("set to whatever you would like the ranged damage to be")]
    public float rangedDamage = 0f;
    [Tooltip("time to wait between firing ranged projectiles")]
    public float timeBtwShots;
    [Tooltip("time to wait between attacking with melee")]
    public float timeBtwBites;
    private float startTimeBtwShots;
    private float startTimeBtwBites;
    [Tooltip("rate change till next ranged shot")]
    public float rateChangeBtwShots;
    [Tooltip("rate change till next melee strike")]
    public float rateChangeBtwBites;
    [Tooltip("duration of the ranged projectile before it disapears")]
    public float projectileLife;
    [Tooltip("ranged projectiles to be fired")]
    public GameObject rangedProjectile;
    [Tooltip("speed of ranged projectiles")]
    public float projectileSpeed;
    private Vector2 bulletMoveDirection;
    [Tooltip("DO NOT CHANGE. if enemy is moving, this will be true")]
    public bool isMoving = false;
    [Tooltip("DO NOT CHANGE. if enemy is attacking, this will be true")]
    public bool isAttacking = false;
    [Tooltip("DO NOT CHANGE. if enemy is attacking with melee, this will be true")]
    public bool isAttackingMelee = false;
    [Tooltip("DO NOT CHANGE. if enemy is standing still, this will be true")]
    public bool isIdle = true;
    [Tooltip("DO NOT CHANGE. this is the activate right movement for enemy A.I.")]
    public bool moveRightX;
    [Tooltip("DO NOT CHANGE. this is the activate left movement for enemy A.I.")]
    public bool moveLeftX;
    [Tooltip("DO NOT CHANGE. this is the activate upwards movement direction for enemy A.I.")]
    public bool moveUpY;
    [Tooltip("DO NOT CHANGE. this is the activate down movement direction for enemy A.I.")]
    public bool moveDownY;
    public bool canFlipSprite;
    [Tooltip("this is the renderer for this gameobject sprite")]
    public SpriteRenderer enemyRenderer;
    [Tooltip("the speed at which the A.I will move")]
    public float enemySpeed;
    [Tooltip("the speed at which the A.I will move in the upwards direction")]
    public float enemySpeedUp;
    [Tooltip("the speed at which the A.I will move in the downward direction")]
    public float enemySpeedDown;
    [Tooltip("the speed at which the A.I will move towards the player when following")]
    public float followSpeed;
    [Tooltip("set this to true if you want the A.I. to be able to move toward the player")]
    public bool canMoveToward;

    private int number;
    private int numberTwo;
    [Tooltip("gameobject from which the ranged projectile ejects from")]
    public GameObject bulletEmitter;
    [Tooltip("set this the this gameobject's transform")]
    public Transform enemy;
    private int numberMin = 0;
    [Tooltip("set this to determine the rate of change on A.I. decisions, CURRENTLY MUST BE SET OVER A VALUE OF 10, a higher value will mean the A.I. stays on the current task longer (or has a longer time to decide what to do next)")]
    public int decisionRate = 0;
    [Tooltip("set this to the transform of the detector that will be detecting close objects")]
    public Transform detector;
    [Tooltip("DO NOT CHANGE. this will show the enemy is targeting the player to the left side")]
    public bool isTargetingLeft;
    [Tooltip("DO NOT CHANGE. this will show the enemy is targeting the player to the right side")]
    public bool isTargetingRight;
    [Tooltip("set this to the distance you want the enemy A.I. to detect or search for the player")]
    public float distanceToCheck = 10f;
    [Tooltip("must create a layer called 'Player Target' and set the player to that layer")]
    public LayerMask playerTarget;
    [Tooltip("MUST BE SET IN ORDER TO BE ABLE TO ATTACK THE PLAYER BEFORE BEING ATTACKED")]
    public bool canAttack;
    [Tooltip("MUST BE SET IN ORDER TO BE ABLE TO ATTACK THE PLAYER WITH RANGED PROJECTILE")]
    public bool canAttackWithRanged;
    [Tooltip("MUST BE SET IN ORDER TO BE ABLE TO MOVE DOWN")]
    public bool canMoveDown;
    [Tooltip("MUST BE SET IN ORDER TO BE ABLE TO MOVE UP")]
    public bool canMoveUp;
    [Tooltip("MUST BE SET IN ORDER TO BE ABLE TO MOVE RIGHT")]
    public bool canMoveRight;
    [Tooltip("MUST BE SET IN ORDER TO BE ABLE TO MOVE LEFT")]
    public bool canMoveLeft;
    [Tooltip("MUST BE SET IN ORDER TO BE ABLE TO TARGET PLAYER WITHIN RADIUS")]
    public bool canTargetRadius;
    public bool canTargetRight;
    public bool canTargetLeft;
    [Tooltip("DO NOT CHANGE. this let's you know the enemy A.I. is tageting the player within the radius")]
    public bool isTargetingRadius;
    [Tooltip("MUST BE SET FOR THE RADIUS OF ENEMY'S TARGETING RANGE")]
    public float circleCastRadius;
    [Tooltip("DO NOT CHANGE. this will let you know the A.I. is targeting a player within melee range")]
    public bool isTargetingMeleeRadius;
    [Tooltip("MUST BE SET FOR THE RADIUS OF ENEMY'S MELEE ATTACK RANGE")]
    public float meleeAttackRadius;
    [Tooltip("must set the player gameobject transform")]
    public Transform playerTransform;
    [Tooltip("DO NOT CHANGE. this will let you know the A.I. is tracking the player after enemy has been hit")]
    public bool isTrackingPlayer;

    private Color regularColor;
    [Tooltip("set this to a color for this enemy to change to when hit by the players bullets")]
    public Color colorWhenHit;
    [Tooltip("must set to a duration for how long the impact from a players bullet will last (change depending on length of sprite animation)")]
    public float impactDuration;
    [Tooltip("this is the variation of aim the enemy A.I. has to hit the player, a higher number will result in more inaccurate shots")]
    public float bulletVariationRange;
    private Vector3 variationOfBullets;

    public bool willStopOnceInRange;
    private Vector3 distanceFromTarget;

    public float playerWithinThisRange;
    public bool isPlayerWithinThisRange;

    [Tooltip("Draws a ray along bullet path")]
    public bool drawTargetRay;
    public GameObject rayCast;

    public bool playerToLeft;
    public bool playerToRight;
    public bool playerAbove;
    public bool playerBelow;

    void Start()
    {
        regularColor = enemyRenderer.color;
        isMoving = false;
        isAttacking = false;
        isIdle = true;
    }


    //move toward

    public void MoveToward()
    {
        if (canMoveToward)
        {
            if (player.activeSelf == true)
            {

                enemy.transform.position = Vector2.MoveTowards(transform.position, playerTransform.transform.position, followSpeed * Time.deltaTime);

            }
        }
    }

    //move enemy right

    public void MoveRightX()
    {
        if (canMoveRight)
        {
            if (canFlipSprite)
            {
                enemyRenderer.flipX = true;
            }
            isIdle = false;
            isMoving = true;
            transform.Translate(Time.deltaTime * enemySpeed, 0, 0);
        }
    }

    //move enemy left

    public void MoveLeftX()
    {
        if (canMoveLeft)
        {
            if (canFlipSprite)
            {
                enemyRenderer.flipX = false;
            }

            isIdle = false;
            isMoving = true;
            transform.Translate(-Time.deltaTime * enemySpeed, 0, 0);
        }
    }

    //move enemy up

    public void MoveUpY()
    {
        if (canMoveUp == true)
        {
            isIdle = false;
            isMoving = true;
            transform.Translate(0, Time.deltaTime * enemySpeedUp, 0);
        }
    }

    //move enemy down

    public void MoveDownY()
    {
        if (canMoveDown == true)
        {
            isIdle = false;
            isMoving = true;
            transform.Translate(0, -Time.deltaTime * enemySpeedDown, 0);
        }
    }


    void ShootAtTarget()
    {
        if (canAttack)
        {
            if (canTargetRadius)
            {
                if (player.activeSelf == true)
                {
                    enemyAbilities.rangedAttackDamage = rangedDamage;

                    isAttacking = true;

                    GameObject Temporary_Projectile;
                    Temporary_Projectile = Instantiate(rangedProjectile, transform.position, transform.rotation) as GameObject;
                    Temporary_Projectile.GetComponent<Rigidbody2D>();
                    Destroy(Temporary_Projectile, projectileLife);   //destroy temp bullet

                    Temporary_Projectile.GetComponent<Rigidbody2D>().transform.position = bulletEmitter.transform.position;
                    Temporary_Projectile.GetComponent<Rigidbody2D>().AddForce(bulletMoveDirection.normalized * projectileSpeed);

                    startTimeBtwShots = timeBtwShots;
                }
                else
                    isAttacking = false;
            }
        }
    }

    //calling random function from answer of random number

    void CallAFunction()
    {
        if (number == 1)
        {
            if (canMoveRight)
            {
                moveRightX = true;
            }
        }
        if (number == 2)
        {
            if (canMoveUp)
            {
                moveUpY = true;
            }
        }
        if (number == 3)
        {
            if (canMoveLeft)
            {
                moveLeftX = true;
            }
        }
        if (number == 4)
        {
            if (canMoveDown)
            {
                moveDownY = true;
            }
        }
        if (number == 5)
        {
            if (canMoveRight)
            {
                moveRightX = false;
            }
        }
        if (number == 6)
        {
            if (canMoveLeft)
            {
                moveLeftX = false;
            }
        }
        if (number == 7)
        {
            if (canMoveUp)
            {
                moveUpY = false;
            }
        }
        if (number == 8)
        {
            if (canMoveDown)
            {
                moveDownY = false;
            }
        }
        if (number == 9 & numberTwo == 9 || number == 9 && numberTwo == 8 || number == 9 && numberTwo == 7)
        {
            moveDownY = false;
            moveUpY = false;
            moveLeftX = false;
            moveRightX = false;
            isIdle = true;
        }
    }


    public void MoveAwayFromPlayer()
    {
        if (playerToLeft)
        {
            MoveRightX();
        }
        if (playerToRight)
        {
            MoveLeftX();
        }
        if (playerAbove)
        {
            MoveDownY();
        }
        if (playerBelow)
        {
            MoveUpY();
        }
    }
    //Update function

    void Update()
    {
        if (isTargetingRight)
        {
            if (moveRightX)
            {
                if (canFlipSprite)
                {
                    enemyRenderer.flipX = true;
                }
            }
        }
        if (isTargetingLeft)
        {
            if (moveLeftX)
            {
                if (canFlipSprite)
                {
                    enemyRenderer.flipX = false;
                }
            }

        }
        
        distanceFromTarget = this.transform.position - playerTransform.transform.position;

        if (distanceFromTarget.x >= 1)
        {
            playerToRight = false;
            playerToLeft = true;
            if (canFlipSprite)
            {
                enemyRenderer.flipX = false;
            }
        }
        if (distanceFromTarget.x <= -1)
        {
            playerToLeft = false;
            playerToRight = true;
            if (canFlipSprite)
            {
                enemyRenderer.flipX = true;
            }
        }
        if (distanceFromTarget.y >= 1)
        {
            playerAbove = false;
            playerBelow = true;
        }
        if (distanceFromTarget.y <= -1)
        {
            playerBelow = false;
            playerAbove = true;
        }

        if (drawTargetRay)
        {
            Debug.DrawRay(rayCast.transform.position, bulletMoveDirection, Color.red, 0.01f);
        }

        variationOfBullets = playerTransform.transform.position + Random.insideUnitSphere * bulletVariationRange;

        bulletMoveDirection = variationOfBullets - transform.position;

        startTimeBtwBites = startTimeBtwBites - rateChangeBtwBites;
        startTimeBtwShots = startTimeBtwShots - rateChangeBtwShots;

        if (isAttacking == false && isAttackingMelee == false && isMoving == false && isTargetingRadius == false && isTargetingLeft == false && isTargetingRight == false)
        {
            isIdle = true;
        }

        if (moveRightX == true)
        {
            MoveRightX();
        }
        if (moveLeftX == true)
        {
            MoveLeftX();
        }
        if (moveUpY == true)
        {
            MoveUpY();
        }
        if (moveDownY == true)
        {
            MoveDownY();
        }

        if (canAttack)
        {
            if (isTargetingRight)
            {
                if (player.activeSelf == true)
                {
                    MoveRightX();
                }
            }
        }

        if (canAttack)
        {
            if (isTargetingLeft)
            {
                if (player.activeSelf == true)
                {
                    MoveLeftX();
                }
            }
        }

        if (canAttackWithRanged == true)
        {
            canAttackWithRanged = true;
        }
        if (canAttackWithRanged == false)
        {
            canAttackWithRanged = false;
        }

        if (canAttack)
        {
            if (startTimeBtwShots <= 0)
            {
                if (isTargetingRadius)
                {
                    if (canTargetRadius)
                    {
                        if (player.activeSelf == true)
                        {
                            ShootAtTarget();
                        }
                    }
                }
            }
        }
    }

    //Fixed Update function

    void FixedUpdate()
    {

        isIdle = false;
        if (canTargetLeft)
        {
            isTargetingLeft = Physics2D.Raycast(detector.transform.position, Vector2.left, distanceToCheck, playerTarget);
        }
        if (canTargetRight)
        {
            isTargetingRight = Physics2D.Raycast(detector.transform.position, Vector2.right, distanceToCheck, playerTarget);
        }
        isTargetingRadius = Physics2D.OverlapCircle(detector.transform.position, circleCastRadius, playerTarget);
        isTargetingMeleeRadius = Physics2D.OverlapCircle(detector.transform.position, meleeAttackRadius, playerTarget);

        number = Random.Range(numberMin, decisionRate);
        numberTwo = Random.Range(numberMin, decisionRate / 2);

        CallAFunction();

        if (canAttack)
        {
            if (isTargetingRadius == true)
            {
                if (player.activeSelf == true)
                {
                    if (canMoveToward)
                    {
                        if (isPlayerWithinThisRange == false)
                        {
                            MoveToward();
                        }
                    }
                }
            }
        }

        if (willStopOnceInRange && isPlayerWithinThisRange)
        {
            MoveAwayFromPlayer();
        }

        //for targeting in a melee range
        if (isTargetingMeleeRadius)
        {
            if (player.activeSelf == true)
            {
                if (canAttack)
                {
                    isAttackingMelee = true;
                    enemyAbilities.basicAttackDamage = basicDamage;
                    enemyAbilities.specialAttackDamage = specialDamage;
                    if (startTimeBtwBites <= 0)
                    {
                        enemyAbilities.BasicAttack();
                        playerHealth.BloodSplat();
                        playerHealth.ShakeTheCamera();
                        startTimeBtwBites = timeBtwBites;
                    }
                    else
                        isAttackingMelee = false;
                }
            }
        }
    }

    //for attacking the player after been hit

    void OnTriggerEnter2D(Collider2D other)
    {
        if (player.activeSelf == true)
        {
            if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Charged Bullet"))
            {
                isTrackingPlayer = true;
                canAttack = true;
            }
        }
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Charged Bullet"))
        {
            Impact();
        }
    }

    public IEnumerator ImpactTimer()
    {
        yield return new WaitForSecondsRealtime(impactDuration);
        Recover();
    }

    void LateUpdate()
    {
        if (isTrackingPlayer)
        {
            if (player.activeSelf == true)
            {
                if (willStopOnceInRange)
                {
                    if (isPlayerWithinThisRange == false)
                    {
                        enemy.transform.position = Vector2.MoveTowards(transform.position, playerTransform.transform.position, followSpeed * Time.deltaTime);
                    }
                    if (isPlayerWithinThisRange == true)
                    {
                        MoveAwayFromPlayer();
                    }
                }
                if (willStopOnceInRange == false)
                {
                    enemy.transform.position = Vector2.MoveTowards(transform.position, playerTransform.transform.position, followSpeed * Time.deltaTime);
                }
            }
        }

        if (canFlipSprite)
        {
            if (moveRightX)
            {
                enemyRenderer.flipX = true;
            }
            else
                enemyRenderer.flipX = false;

            if (isTargetingRight)
            {
                enemyRenderer.flipX = true;
            }
            else
                enemyRenderer.flipX = false;

            if (isTargetingRadius)
            {
                if (playerToRight)
                {
                    enemyRenderer.flipX = true;
                }
                else
                    enemyRenderer.flipX = false;
            }
        }

        if (isIdle)
        {
            moveDownY = false;
            moveLeftX = false;
            moveUpY = false;
            moveRightX = false;
        }
    }

    public void Impact()
    {
        enemyRenderer.color = colorWhenHit;
        StartCoroutine(ImpactTimer());
    }

    public void Recover()
    {
        enemyRenderer.color = regularColor;
    }
}
