using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxEnemyHealth = 100f;
    public float currentEnemyHealth = 0f;
    public float enemyHealthRegenRate = 0.1f;
    public PlayerAbilities playerAbilities;
    public PlayerController playerController;
    public GameObject bulletImpact;
    public float bloodSplatterRange;
    private Vector2 enemyCenter;
    private Vector2 enemyCenter2;
    public Transform enemy;
    public float impactDuration;

    // Start is called before the first frame update

    public void Start()
    {
        currentEnemyHealth = maxEnemyHealth;
    }

    void Update()
    {
        enemyCenter = enemy.transform.position + Random.insideUnitSphere * bloodSplatterRange;
        enemyCenter2 = enemy.transform.position + Random.insideUnitSphere * bloodSplatterRange * 2;
    }

    void FixedUpdate()
    {
        if (currentEnemyHealth <= maxEnemyHealth && currentEnemyHealth >= 0)
        {
            currentEnemyHealth = currentEnemyHealth + enemyHealthRegenRate;
        }
    }

    public void Impact()
    {
        GameObject Temp_Impact;
        Temp_Impact = Instantiate(bulletImpact, enemyCenter, transform.rotation) as GameObject;
        Destroy(Temp_Impact, impactDuration);
    }
    public void LargeImpact()
    {
        GameObject Temp_Impact;
        Temp_Impact = Instantiate(bulletImpact, enemyCenter2, transform.rotation) as GameObject;
        Temp_Impact.transform.localScale = new Vector3(2,2,0);
        Destroy(Temp_Impact, impactDuration);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Impact();
            currentEnemyHealth = currentEnemyHealth - playerAbilities.bulletDamage;
        }
        if (other.gameObject.CompareTag("Charged Bullet"))
        {
            Destroy(other.gameObject);
            LargeImpact();
            currentEnemyHealth = currentEnemyHealth - playerAbilities.chargedBulletDamage;
        }
    }
    void LateUpdate()
    {
        if (this.gameObject == null)
        {
            if (currentEnemyHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
