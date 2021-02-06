using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public PlayerController script_pc;
    public PlayerAbilities script_pa;
    public DeathManager deathManager;
    public CameraMainScript cameraMainScript;

    public GameObject player;
    public SpriteRenderer playerSprite;
    public GameObject airBarSlider;

    public float lavaDamage = 10f;
    public float waterDamage = 10f;

    public float maxHealth = 1000f;
    public float currentHealth;
    public float healthOrbStrength = 0f;
    public PlayerController healthBar;
    public PlayerController airBar;

    public float maxAir = 100f;
    public float currentAir;
    public float airLossRate = 0f;

    public EnemyController enemyController;

    public GameObject bulletImpact;

    public float bloodSplatterRange;
    public float bloodSplatterDuration;

    private Vector2 playerCenter;
    public float cameraShakeDuration;

    public bool isSuffocating;
    private float suffocatingDistanceCheck = 2f;
    public GameObject suffocatingPanel;

    public LayerMask underGround;

    public SoundController soundController;
    public GameObject heartBeat;

    private Color oldPlayerColor;
    public Color colorWhenHit;

    private float v;

    public float colorTime;
        
    // Start is called before the first frame update
    void Start()
    {
        oldPlayerColor = playerSprite.color;
        suffocatingPanel.SetActive(false);
        playerSprite = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        currentAir = maxAir;
        script_pc.SetHealth(maxHealth);
        script_pc.SetMaxHealth(maxHealth);
        script_pc.SetAir(maxAir);
        script_pc.SetMaxAir(maxAir);
    }

    public IEnumerator CameraShakeRecover()
    {
        if (this.gameObject.activeSelf == true)
        {
            yield return new WaitForSecondsRealtime(cameraShakeDuration);
            cameraMainScript.shakeCamera = false;
        }
    }

    public void ShakeTheCamera()
    {
        if (cameraMainScript.loweringCamera == false)
        {
            if (this.gameObject.activeSelf == true)
            {
                cameraMainScript.shakeCamera = true;
                StartCoroutine(CameraShakeRecover());
            }
        }
    }

    public void PauseAir()
    {
        currentAir = currentAir + 0;
    }
    
    public void HealthRegen()
    {
        if (currentHealth <= maxHealth && script_pc.pauseGame == false)
        {
            currentHealth = currentHealth + script_pc.healthRegenRate;
        }
    }

    void ElementalDamage()
    {
        if (script_pc.isSubmergedWater == true)
        {
            if (currentAir <= 0 && script_pc.pauseGame == false)
            {
                currentHealth = currentHealth - waterDamage;
                ShakeTheCamera();
                Debug.Log("you are drowning!");
            }
        }
        if (script_pc.isSubmergedLava == true)
        {
            ShakeTheCamera();
            currentHealth = currentHealth - lavaDamage;
            Debug.Log("you are taking damage!");
        }
        if (isSuffocating == true)
        {
            if (currentAir <= 0 && script_pc.pauseGame == false)
            {
                currentHealth = currentHealth - waterDamage;
            }
        }
    }

    public void BloodSplat()
    {
        GameObject Temp_Impact;
        Temp_Impact = Instantiate(bulletImpact, playerCenter, transform.rotation) as GameObject;
        Destroy(Temp_Impact, bloodSplatterDuration);
        if (Temp_Impact != null)
        {
            soundController.OnHit();
        }
        int number = Random.Range(0, 10);
        if (number == 1)
        {
            soundController.PlayerGrunt();
        }
    }

    //ranged attack from enemy projectile

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy Projectile"))
        {
            enemyController.enemyAbilities.RangedAttack();   
            Destroy(other.gameObject);
            BloodSplat();
            Hit();
            Debug.Log("hit by projectile!");
            soundController.OnHit();
            ShakeTheCamera();
        }
        if (other.gameObject.CompareTag("Health Orb"))
        {
            Debug.Log("health picked up!");
            currentHealth = currentHealth + healthOrbStrength;
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        isSuffocating = Physics2D.Raycast(this.transform.position, Vector2.down, suffocatingDistanceCheck, underGround);

        playerCenter = transform.position + Random.insideUnitSphere * bloodSplatterRange;

        if (script_pc.pauseGame == true)
        {
            PauseAir();
        }

        healthBar.SetHealth(currentHealth);
        airBar.SetAir(currentAir);

        airBarSlider.SetActive(false);

        if (currentHealth <= 0)
        {
            deathManager.Death();
        }
        
        if (script_pc.isSubmergedWater == true)
        {
            airBarSlider.SetActive(true);
            if (script_pc.pauseGame == false)
            {
                currentAir = currentAir - airLossRate;
                Debug.Log("you are about to drown!");
                ElementalDamage();
                Hit();
            }
        }

        if (isSuffocating == true)
        {
            airBarSlider.SetActive(true);
            if (script_pc.pauseGame == false)
            {
                suffocatingPanel.SetActive(true);
                currentAir = currentAir - airLossRate;
                Debug.Log("You are suffocating!");
                ElementalDamage();
                Hit();
            }
        }
        else
            suffocatingPanel.SetActive(false);

        if (script_pc.isSubmergedLava == true)
        {
            Debug.Log("you are burning!");
            ElementalDamage();
            Hit();
        }

        if (script_pc.isSubmergedWater == false && script_pc.isSubmergedLava == false && isSuffocating == false)
        {
            currentAir = maxAir;
        }

        if (currentHealth < maxHealth / 3)
        {
            heartBeat.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        ResetColor();
        
        if (currentHealth > maxHealth / 3)
        {
            heartBeat.SetActive(false);
        }
    }

    public void Hit()
    {
        playerSprite.color = colorWhenHit;
    }

    private void ResetColor()
    {
        playerSprite.color = oldPlayerColor;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}
