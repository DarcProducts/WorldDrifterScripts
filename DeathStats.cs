using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStats : MonoBehaviour
{
    public GameObject statsPanel;

    private void Update()
    {
        if(Input.GetButton("Tab"))
        {
            statsPanel.SetActive(true);
        }
        if (Input.GetButtonUp("Tab"))
        {
            statsPanel.SetActive(false);
        }
    }
}
