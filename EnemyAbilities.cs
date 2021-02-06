using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilities : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public float basicAttackDamage;
    public float specialAttackDamage;
    public float rangedAttackDamage;

    public void BasicAttack()
    {
        playerHealth.currentHealth = playerHealth.currentHealth - basicAttackDamage;
        basicAttackDamage = 0f;
    }

    public void SpecialAttack()
    {
        playerHealth.currentHealth = playerHealth.currentHealth - specialAttackDamage;
        specialAttackDamage = 0f;
    }

    public void RangedAttack()
    {
        playerHealth.currentHealth = playerHealth.currentHealth - rangedAttackDamage;
        rangedAttackDamage = 0f;
    }
}
