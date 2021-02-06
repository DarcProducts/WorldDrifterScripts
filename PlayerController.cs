using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerAbilities script_pa;
    public PlayerHealth script_ph;
    public Text healthText;
    public Text shiftEnergyText;
    public Text weaponEnergyText;
    public Text airText;
    public Slider healthBar;
    public Slider shiftEnergyBar;
    public Slider weaponEnergyBar;
    public Slider airBar;
    public GameObject airBarSlider;
    public float speed = 10f;
    private Rigidbody2D rigidbody2d;
    public bool isGroundedA;
    public bool isGroundedB;
    public bool isSubmergedWater;
    public bool isSubmergedLava;
    public Transform submerged;
    public Transform grounder;
    public float distanceToCheck = 0f;
    public LayerMask groundA;
    public LayerMask groundB;
    public LayerMask water;
    public float waterSpeed = 0.8f;
    public LayerMask lava;
    public float lavaSpeed = 0.8f;
    public GameObject player;
    public SpriteRenderer myRenderer;
    public SpriteRenderer myWeapon;
    public Animator playerAnimController;
    public bool isCharged = false;
    public bool pauseGame = true;
    public bool continueGame = false;
    public GameObject pauseScreen;
    public WorldShift worldShift;
    public float healthRegenRate = 1f;
    public float shiftEnergyRegenRate = 1f;
    public float weaponEnergyRegenRate = 1f;
    public float weaponFireRate;
    public float degradeRate = 1f;
    public bool isOnWall;
    public LayerMask wall;
    public float onWallSpeed = 0.5f;
    public bool isOnEnemyA;
    public bool isOnEnemyB;
    public float onEnemySpeed = 0.5f;
    public LayerMask enemyA;
    public LayerMask enemyB;
    public bool isOnObject = false;
    public LayerMask inanimateObject;
    public float onObjectSpeed = 0.5f;
    public bool isMoving = false;

    public float orbPoints;
    public Text orbPointsText;

    public Text kills;
    public int killPointsRat = 0;
    public int killPointsBird = 0;
    public int killPointsDrone = 0;
    public int killPointsMech = 0;
    public int killPointsAlphaMech = 0;
    public int killPointsScorpion = 0;
    public int killPointsDragonfly = 0;
    public int killPointsMosasaur = 0;

    private float weaponFireMax = 1f;
    private float weaponFireCurrent = 0f;

    private float oldBulletSpeed;
    private float newBulletSpeed;

    public GameObject worldBShiftView;
    public GameObject worldAShiftView;
    private Vector3 shiftWorldBPosition;
    private Vector3 shiftWorldAPosition;
    public GameObject cameraWorldB;
    public GameObject cameraWorldA;

    public bool isInWorldA = true;
    public bool isInWorldB = false;

    public GameObject worldABackground;
    public GameObject worldBBackground;

    public bool isJumping;
    public bool isSwimming;

    private float shiftMaxWait = 1.1f;
    private float currentShiftWait = 0f;
    [Range(0,1)]
    [Tooltip("Set this to the wait time between ablility to shift")]
    public float shiftWaitRate;

    private float maxJumpWait = 1f;
    private float currentJumpWait = 0f;

    private Animator myAnimator;

    public GameObject footSteps;

    [Range(0,1)]
    public float inAirSpeed;

    public SoundController soundController;

    public byte maxOrbPoints;

    void Start()
    {
        myAnimator = GetComponent<Animator>();

        currentJumpWait = maxJumpWait;

        currentShiftWait = shiftMaxWait;

        worldBBackground.SetActive(false);
        worldBShiftView.SetActive(false);
        worldAShiftView.SetActive(false);
        worldABackground.SetActive(true);

        oldBulletSpeed = script_pa.bulletSpeed;

        newBulletSpeed = oldBulletSpeed / 2;

        weaponFireCurrent = weaponFireMax;
        airBar.value = script_ph.maxAir;
        rigidbody2d = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        pauseScreen.SetActive(true);
        script_pa.PauseGame();
    }

    public void SetMaxHealth(float health)     //setting up health energy bars
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void SetHealth(float health)
    {
        healthBar.value = health;
    }

    public void SetMaxAir(float air)     //setting up air bars
    {
        airBar.maxValue = air;
        airBar.value = air;
    }

    public void SetAir(float air)
    {
        airBar.value = air;
    }

    public void SetMaxShiftEnergy(float shiftEnergy)
    {
        shiftEnergyBar.maxValue = shiftEnergy;
        shiftEnergyBar.value = shiftEnergy;
    }

    public void SetShiftEnergy(float shiftEnergy)
    {
        shiftEnergyBar.value = shiftEnergy;
    }

    public void SetMaxWeaponEnergy(float weaponEnergy)
    {
        weaponEnergyBar.maxValue = weaponEnergy;
        weaponEnergyBar.value = weaponEnergy;
    }

    public void SetWeaponEnergy(float weaponEnergy)
    {
        weaponEnergyBar.value = weaponEnergy;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Orb"))
        {
            ++orbPoints;
            Destroy(other.gameObject);
            Debug.Log("orb collected!");
        }
    }

    public void ShiftToWorldB()
    {
        worldBShiftView.SetActive(false);
        worldAShiftView.SetActive(false);
        worldABackground.SetActive(false);
        worldBBackground.SetActive(true);
        this.transform.position = shiftWorldBPosition;
        isInWorldA = false;
        isInWorldB = true;
        currentShiftWait = shiftMaxWait;
    }
    

    public void ShiftToWorldA()
    {
        worldAShiftView.SetActive(false);
        worldBShiftView.SetActive(false);
        worldBBackground.SetActive(false);
        worldABackground.SetActive(true);
        this.transform.position = shiftWorldAPosition;
        isInWorldB = false;
        isInWorldA = true;
        currentShiftWait = shiftMaxWait;
    }

    public void ActivateWorldAViewer()
    {
        worldBShiftView.SetActive(false);
        worldAShiftView.SetActive(true);
    }

    public void ActivateWorldBViewer()
    {
        worldAShiftView.SetActive(false);
        worldBShiftView.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (isMoving && isGroundedA == true || isMoving && isOnEnemyB == true)
        {
            if (footSteps != null)
            {
                footSteps.SetActive(true);
                footSteps.GetComponent<AudioSource>().volume = Random.Range(.01f, .2f);
            }
        }
        else
        {
            footSteps.SetActive(false);
        }

        if (currentShiftWait >= 0)
        {
            currentShiftWait = currentShiftWait - shiftWaitRate * Time.deltaTime;
        }

        //detectors
        isGroundedA = Physics2D.Raycast(grounder.transform.position, Vector2.down, distanceToCheck, groundA);
        isGroundedB = Physics2D.Raycast(grounder.transform.position, Vector2.down, distanceToCheck, groundB);
        isSubmergedWater = Physics2D.Raycast(submerged.transform.position, Vector2.up, distanceToCheck, water);
        isSubmergedLava = Physics2D.Raycast(submerged.transform.position, Vector2.up, distanceToCheck, lava);
        isOnWall = Physics2D.Raycast(grounder.transform.position, Vector2.down, distanceToCheck, wall);
        isOnEnemyA = Physics2D.Raycast(this.transform.position, Vector2.down, distanceToCheck, enemyA);
        isOnEnemyB = Physics2D.Raycast(this.transform.position, Vector2.down, distanceToCheck, enemyB);
        isOnObject = Physics2D.Raycast(grounder.transform.position, Vector2.down, distanceToCheck, inanimateObject);

        weaponFireCurrent = weaponFireCurrent - weaponFireRate;
    }
   
    void Update()
    {
        if (pauseScreen.activeSelf == true)
        {
            if (Input.GetButton("E"))
            {
                Application.Quit();
            }
        }

        if (currentShiftWait <= 0)
        {
            if (worldShift.currentShiftEnergy >= worldShift.shiftCost)
            {
                if (Input.GetButtonDown("L Shift"))
                {
                    if (isInWorldB)
                    {
                        ActivateWorldAViewer();
                    }
                    else
                        ActivateWorldBViewer();
                }
            }
        }
        

        if (Input.GetButtonUp("L Shift"))
        {
            if (worldAShiftView.activeSelf == true)
            {
                if (isInWorldB)
                {
                    if (worldShift.currentShiftEnergy >= worldShift.shiftCost)
                    {
                        worldShift.Shift();
                        ShiftToWorldA();
                    }
                }
            }
            if (worldBShiftView.activeSelf == true)
            {
                if (isInWorldA)
                {
                    if (worldShift.currentShiftEnergy >= worldShift.shiftCost)
                    {
                        worldShift.Shift();
                        ShiftToWorldB();
                    }
                }
            }
        }

        shiftWorldBPosition.x = this.transform.position.x - 5000f;
        shiftWorldBPosition.y = this.transform.position.y + 1;
        shiftWorldBPosition.z = this.transform.position.z;

        shiftWorldAPosition.x = this.transform.position.x + 5000f;
        shiftWorldAPosition.y = this.transform.position.y + 1;
        shiftWorldAPosition.z = this.transform.position.z;

        orbPointsText.text = "Orbs Collected: " + orbPoints.ToString() + " / " + maxOrbPoints.ToString();

        healthText.text = "Health: " + Mathf.Floor(script_ph.currentHealth) + " / " + Mathf.Floor(script_ph.maxHealth);
        shiftEnergyText.text = "Shift Energy: " + Mathf.Floor(worldShift.currentShiftEnergy) + " / " + Mathf.Floor(worldShift.maxShiftEnergy);
        weaponEnergyText.text = "Weapon Energy: " + Mathf.Floor(script_pa.currentWeaponEnergy) + " / " + Mathf.Floor(script_pa.maxWeaponEnergy);
        airText.text = "Air Left";

        kills.text = "Rats killed: " + killPointsRat.ToString() + "\nScorpions killed: " + killPointsScorpion.ToString() + "\nDragonflys killed: " + killPointsDragonfly.ToString() + "\nBirds killed: " + killPointsBird.ToString() + "\nDrones killed: " + killPointsDrone.ToString() +
            "\nMechs killed: " + killPointsMech.ToString() + "\nMosasaurs killed: " + killPointsMosasaur.ToString() + "\nAlpha Mechs killed: " + killPointsAlphaMech.ToString();

        isCharged = false;

        //health and energy regen

        HealthRegen();
        EnergyRegen();
        WeaponRegen();

        //detect if grounded or airborne
        if (isSubmergedWater)
        {
            airBar.value = script_ph.currentAir;
            script_pa.bulletSpeed = newBulletSpeed;
            Debug.Log("you are underwater!");
        }
        if (isSubmergedLava)
        {
            script_pa.bulletSpeed = newBulletSpeed;
            Debug.Log("you are in lava!");
        }     
             
        if (isSubmergedLava == false && isSubmergedWater == false)
        {
            script_pa.bulletSpeed = oldBulletSpeed;
        }

        if (Input.GetButtonDown("Escape")) //start button press //pause game or press escape
        {
                script_pa.PauseGame();
                pauseScreen.SetActive(true);
                pauseGame = true;
                continueGame = false;
                Debug.Log("escape button pressed!");
        }

            if (Input.GetButtonDown("Space")) //back button press //unpause game
            {
                script_pa.ContinueGame();
                pauseScreen.SetActive(false);
                pauseGame = false;
                continueGame = true;
                Debug.Log("space button pressed!");
            }

        float _h = Input.GetAxisRaw("Horizontal"); //get horizontal axis

        myAnimator.SetFloat("Speed", Mathf.Abs(_h));

        //controls if grounded

        if (continueGame)
        {
            if (_h == 1 && isGroundedA == true)
            {
                isMoving = true;
                myRenderer.flipX = false;
                myWeapon.flipX = false;
                rigidbody2d.velocity = new Vector2(speed, rigidbody2d.velocity.y);
            }
            if (_h == -1 && isGroundedA == true)
            {
                isMoving = true;
                myRenderer.flipX = true;
                myWeapon.flipX = true;
                rigidbody2d.velocity = new Vector2(speed * -1, rigidbody2d.velocity.y);
            }

            if (_h == 0 && isGroundedA == true)
            {
                isMoving = false;
                rigidbody2d.velocity = new Vector2(speed * 0f, rigidbody2d.velocity.y);
            }

            //other crap

            if (_h == 1 && isGroundedB == true)
            {
                isMoving = true;
                myRenderer.flipX = false;
                myWeapon.flipX = false;
                rigidbody2d.velocity = new Vector2(speed, rigidbody2d.velocity.y);
            }
            if (_h == -1 && isGroundedB == true)
            {
                isMoving = true;
                myRenderer.flipX = true;
                myWeapon.flipX = true;
                rigidbody2d.velocity = new Vector2(speed * -1, rigidbody2d.velocity.y);
            }

            if (_h == 0 && isGroundedB == true)
            {
                isMoving = false;
                rigidbody2d.velocity = new Vector2(speed * 0f, rigidbody2d.velocity.y);
            }

            //controls if swimming in water

            if (_h == 1 && isSubmergedWater == true)
            {
                isMoving = true;
                myRenderer.flipX = false;
                myWeapon.flipX = false;
                rigidbody2d.velocity = new Vector2(speed * waterSpeed, rigidbody2d.velocity.y);
            }
            if (_h == -1 && isSubmergedWater == true)
            {
                isMoving = true;
                myRenderer.flipX = true;
                myWeapon.flipX = true;
                rigidbody2d.velocity = new Vector2(speed * (waterSpeed * -1), rigidbody2d.velocity.y);
            }
            if (_h == 0 && isSubmergedWater == true)
            {
                isMoving = false;
                rigidbody2d.velocity = new Vector2(speed * (float)0.001f, rigidbody2d.velocity.y);
            }

            //controls if swimming in lava

            if (_h == 1 && isSubmergedLava == true)
            {
                isMoving = true;
                myRenderer.flipX = false;
                myWeapon.flipX = false;
                rigidbody2d.velocity = new Vector2(speed * lavaSpeed, rigidbody2d.velocity.y);
            }
            if (_h == -1 && isSubmergedLava == true)
            {
                isMoving = true;
                myRenderer.flipX = true;
                myWeapon.flipX = true;
                rigidbody2d.velocity = new Vector2(speed * (lavaSpeed * -1), rigidbody2d.velocity.y);
            }
            if (_h == 0 && isSubmergedLava == true)
            {
                isMoving = false;
                rigidbody2d.velocity = new Vector2(speed * (float)0.001f, rigidbody2d.velocity.y);
            }

            //controls if on enemy A

            if (_h == 1 && isOnEnemyA == true)
            {
                isMoving = true;
                myRenderer.flipX = false;
                myWeapon.flipX = false;
                rigidbody2d.velocity = new Vector2(speed * onEnemySpeed, rigidbody2d.velocity.y);
            }
            if (_h == -1 && isOnEnemyA == true)
            {
                isMoving = true;
                myRenderer.flipX = true;
                myWeapon.flipX = true;
                rigidbody2d.velocity = new Vector2(speed * (onEnemySpeed * -1), rigidbody2d.velocity.y);
            }

            if (_h == 0 && isOnEnemyA == true)
            {
                isMoving = false;
                rigidbody2d.velocity = new Vector2(speed * (float)0.001f, rigidbody2d.velocity.y);
            }

            //controls if on enemy B

            if (_h == 1 && isOnEnemyB == true)
            {
                isMoving = true;
                myRenderer.flipX = false;
                myWeapon.flipX = false;
                rigidbody2d.velocity = new Vector2(speed * onEnemySpeed, rigidbody2d.velocity.y);
            }
            if (_h == -1 && isOnEnemyB == true)
            {
                isMoving = true;
                myRenderer.flipX = true;
                myWeapon.flipX = true;
                rigidbody2d.velocity = new Vector2(speed * (onEnemySpeed * -1), rigidbody2d.velocity.y);
            }

            if (_h == 0 && isOnEnemyB == true)
            {
                isMoving = false;
                rigidbody2d.velocity = new Vector2(speed * (float)0.001f, rigidbody2d.velocity.y);
            }

            //controls if on wall

            if (_h == 1 && isOnWall == true)
            {
                isMoving = true;
                myRenderer.flipX = false;
                myWeapon.flipX = false;
                rigidbody2d.velocity = new Vector2(speed * onWallSpeed, rigidbody2d.velocity.y);
            }
            if (_h == -1 && isOnWall == true)
            {
                isMoving = true;
                myRenderer.flipX = true;
                myWeapon.flipX = true;
                rigidbody2d.velocity = new Vector2(speed * (onWallSpeed * -1), rigidbody2d.velocity.y);
            }

            if (_h == 0 && isOnWall == true)
            {
                isMoving = false;
                rigidbody2d.velocity = new Vector2(speed * (float)0.001f, rigidbody2d.velocity.y);
            }

            //controlls if on object

            if (_h == 1 && isOnObject == true)
            {
                isMoving = true;
                myRenderer.flipX = false;
                myWeapon.flipX = false;
                rigidbody2d.velocity = new Vector2(onObjectSpeed, rigidbody2d.velocity.y);
            }
            if (_h == -1 && isOnObject == true)
            {
                isMoving = true;
                myRenderer.flipX = true;
                myWeapon.flipX = true;
                rigidbody2d.velocity = new Vector2(onObjectSpeed * -1, rigidbody2d.velocity.y);
            }

            if (_h == 0 && isOnObject == true)
            {
                isMoving = false;
                rigidbody2d.velocity = new Vector2(speed * (float)0.001f, rigidbody2d.velocity.y);
            }

            //if in air

            if (_h == 1 && isGroundedA == false && isGroundedB == false)
            {
                isMoving = true;
                myRenderer.flipX = false;
                myWeapon.flipX = false;
                rigidbody2d.velocity = new Vector2(speed, rigidbody2d.velocity.y);
            }
            if (_h == -1 && isGroundedA == false && isGroundedB == false)
            {
                isMoving = true;
                myRenderer.flipX = true;
                myWeapon.flipX = true;
                rigidbody2d.velocity = new Vector2(speed * -1, rigidbody2d.velocity.y);
            }

            if (_h == 0 && isGroundedA == false && isGroundedB == false)
            {
                isMoving = false;
                rigidbody2d.velocity = new Vector2(speed * 0f, rigidbody2d.velocity.y);
            }

            //jumping things

            if (Input.GetButtonDown("Space"))
            {
                if (isGroundedA)
                {
                    soundController.JumpSound();
                }
                if (isGroundedB)
                {
                     soundController.JumpSound();
                }

                if (isGroundedA || isGroundedB)
                {
                    myAnimator.SetBool("isJumping", true);
                    isJumping = true;
                    script_pa.JumpGround();
                }
                if (isSubmergedWater)
                {
                    myAnimator.SetBool("isJumping", true);
                    isJumping = true;
                    script_pa.SwimWater();
                }
                if (isSubmergedLava)
                {
                    myAnimator.SetBool("isJumping", true);
                    isJumping = true;
                    script_pa.SwimLava();
                }
            }

            //fire weapon

            if (Input.GetButton("Fire1"))
            {
                if (weaponFireCurrent <= 0)
                {
                    script_pa.FireTempBullet();
                    weaponFireCurrent = weaponFireMax;
                    Debug.Log("weapon fired");
                }
            }

            //charged weapon fire

            if (Input.GetButton("Fire2"))
            {
                if (weaponFireCurrent <= 0)
                {
                    script_pa.FireChargedBullet();
                    weaponFireCurrent = weaponFireMax;
                    Debug.Log("charged weapon fire");
                }
            }
        }
    }

    public void OnLanding()
    {
        myAnimator.SetBool("isJumping", false);
        
    }

    public void WeaponRegen()
    {
        script_pa.WeaponRegen();
    }

    public void HealthRegen()
    {
        script_ph.HealthRegen();
    }
    public void EnergyRegen()
    {
        worldShift.EnergyRegen();
    }
}
