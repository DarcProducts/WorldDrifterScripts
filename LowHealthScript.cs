using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowHealthScript : MonoBehaviour
{
    public CanvasRenderer canvasRenderer;
    public Graphic graphic;
    private Color oldColor;
    public Color colorWhenLowHealth;
    public PlayerHealth playerHealth;

    private void Start()
    {
        oldColor = graphic.color;
    }

    private void FixedUpdate()
    {
        if (playerHealth.currentHealth < playerHealth.maxHealth / 4)
        {
            graphic.color = colorWhenLowHealth;
        }
        if (playerHealth.currentHealth > playerHealth.maxHealth / 10)
        {
            ResetColor();
        }
    }

    private void ResetColor()
    {
        graphic.color = oldColor;
    }
}
