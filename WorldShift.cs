using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldShift : MonoBehaviour
{
    public float shiftCost = 100f;
    public float shiftCostCharged = 200f;

    public float maxShiftEnergy = 500f;
    public float currentShiftEnergy;

    public PlayerController playerController;
    public PlayerAbilities playerAbilities;
    public PlayerAimWeapon playerAimWeapon;
    public SoundController sounds;

    public float slowTimeRate = 0.5f;
    public bool timeShift = false;

    private float newRotationSpeed;
    private float oldRotationSpeed;

    private float oldWeaponFireRate;
    private float newWeaponFireRate;

    public void Awake()
    {
        oldRotationSpeed = playerAimWeapon.rotationSpeed;
        newRotationSpeed = playerAimWeapon.rotationSpeed * 5;
        oldWeaponFireRate = playerController.weaponFireRate;
        newWeaponFireRate = playerController.weaponFireRate * 5;
    }

    public void Start()
    {
        currentShiftEnergy = maxShiftEnergy - 1;
        playerController.SetMaxShiftEnergy(maxShiftEnergy);
        playerController.SetShiftEnergy(maxShiftEnergy);
    }

    public void Update()
    {
        playerController.isCharged = false;

        playerController.SetShiftEnergy(currentShiftEnergy);
    }

    public void EnergyRegen()
    {
        if (currentShiftEnergy <= maxShiftEnergy && playerController.pauseGame == false)
        {
            currentShiftEnergy = currentShiftEnergy + playerController.shiftEnergyRegenRate;
        }
    }

    public void DegradeEnergy()
    {
        if (currentShiftEnergy >= 0)
        {
            currentShiftEnergy = currentShiftEnergy - playerController.degradeRate;
        }
    }

    public void DoubleDegradeEnergy()
    {
        if (currentShiftEnergy >= 0)
        {
            currentShiftEnergy = currentShiftEnergy - (playerController.degradeRate * 2);
        }
    }

    public void Shift()
    {
        currentShiftEnergy = currentShiftEnergy - shiftCost;
    }
}