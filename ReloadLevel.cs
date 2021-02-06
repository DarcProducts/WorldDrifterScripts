using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
    public DeathManager deathManager;

    void Update()
    {
        if (deathManager.deathActivated == true)
        {
            if (Input.GetButtonDown("L"))
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
