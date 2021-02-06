using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
    public GameObject player;
    public float jumpForce = 800f;
    public float jumpForceWall = 400f;
    public float jumpForceWater = 400f;
    public float jumpForceLava = 400f;
    public GameObject bulletEmitter;
    private Vector3 crosshairs;
    public GameObject crosshairsGameObject;
    private Vector3 bulletMoveDirection;
    public GameObject bullet;
    public GameObject bulletCharged;

    public float bulletCost = 20f;
    public float bulletCostCharged = 40f;
    public float bulletSpeed;
    public float bulletLife;
    public float bulletDamage = 10f;
    public float chargedBulletDamage = 20f;
    public PlayerController playerController;
    public PlayerHealth playerHealth;
    public WorldShift worldShift;

    public float maxWeaponEnergy = 200f;
    public float currentWeaponEnergy;
    public PlayerController weaponEnergyBar;
    public SoundController sounds;

    public void Start()
    {
        currentWeaponEnergy = maxWeaponEnergy;
        playerController.SetMaxWeaponEnergy(maxWeaponEnergy);
        playerController.SetWeaponEnergy(maxWeaponEnergy);
    }

    public void Update()
    {
        playerController.SetWeaponEnergy(currentWeaponEnergy);

        crosshairs = crosshairsGameObject.transform.position;
        bulletMoveDirection = crosshairs - transform.position;
        bulletMoveDirection.z = 0;
        bulletMoveDirection.Normalize();

        if (currentWeaponEnergy <= 0)
        {
            worldShift.DoubleDegradeEnergy();
        }
    }

    public void WeaponRegen()
    {
        if (currentWeaponEnergy <= maxWeaponEnergy && playerController.pauseGame == false)
        {
            currentWeaponEnergy = currentWeaponEnergy + playerController.weaponEnergyRegenRate;
        }
    }

    public void DegradeWeaponEnergy()
    {
        if (currentWeaponEnergy >= 0)
            currentWeaponEnergy = currentWeaponEnergy - playerController.degradeRate;
    }

    public void JumpGround()
    {
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        Debug.Log("player jumped");
    }
    public void JumpWall()
    {
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForceWall));
        Debug.Log("player jumped");
    }

    public void SwimWater()
    {
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForceWater));
        Debug.Log("you are swimming in water!");
    }
    public void SwimLava()
    {
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForceLava));
        Debug.Log("you are swimming in lava!");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        playerHealth.currentHealth = playerHealth.currentHealth + 0;
        playerHealth.PauseAir();
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

    public void FireTempBullet()
    {
        Debug.Log("Bullet ejected");

        if (currentWeaponEnergy >= bulletCost)
        {
            currentWeaponEnergy = currentWeaponEnergy - bulletCost;
            GameObject Temporary_Bullet;
            Temporary_Bullet = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;
            Temporary_Bullet.GetComponent<Rigidbody2D>();
            Destroy(Temporary_Bullet, bulletLife);   //destroy temp bullet

            Temporary_Bullet.GetComponent<Rigidbody2D>().transform.position = bulletEmitter.transform.position;
            Temporary_Bullet.GetComponent<Rigidbody2D>().AddForce(bulletMoveDirection * bulletSpeed);

            sounds.LaserShot();

        }        
    }

    public void FireChargedBullet()
    {
        Debug.Log("charged bullet ejected");

        if (currentWeaponEnergy >= bulletCostCharged)
        {

            currentWeaponEnergy = currentWeaponEnergy - bulletCostCharged;
            GameObject Temporary_Bullet;
            Temporary_Bullet = Instantiate(bulletCharged, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;
            Temporary_Bullet.GetComponent<Rigidbody2D>();
            Destroy(Temporary_Bullet, bulletLife);   //destroy temp bullet
            Temporary_Bullet.GetComponent<Rigidbody2D>().AddForce(bulletMoveDirection * bulletSpeed);

            sounds.ChargedLaserShot();
        }

        if (currentWeaponEnergy <= 0 && worldShift.currentShiftEnergy >= bulletCostCharged)
        {
            
            currentWeaponEnergy = currentWeaponEnergy - bulletCostCharged;
            GameObject Temporary_Bullet;
            Temporary_Bullet = Instantiate(bulletCharged, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;
            Temporary_Bullet.GetComponent<Rigidbody2D>();
            Destroy(Temporary_Bullet, bulletLife);   //destroy temp bullet
            Temporary_Bullet.GetComponent<Rigidbody2D>().AddForce(bulletMoveDirection * bulletSpeed);

            sounds.ChargedLaserShot();
        }
    }
}
