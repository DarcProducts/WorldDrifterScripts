using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenScript : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject winScreen;

    public bool tMexDead = false;
    public bool alphaMechDead = false;

    public void Update()
    {
        if (playerController.orbPoints == playerController.maxOrbPoints)
        {
            winScreen.SetActive(true);
        }
        else
            winScreen.SetActive(false);
    }
}
