using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")]
    public float maxHealth = 100f;
    float currentHealth;

    public float healthDecayMultiplier = 1.5f;

    [Header("Health Pick Up")]
    public RectMask2D mask;
    public float healthPickUpAmount = 10f;
    public float healthEffectivenessDecrese = 4f;
    public float PanelMaskSize = 130f;
    float currentPanelMaskSize = 130f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth -= healthDecayMultiplier;
        if (currentHealth >= maxHealth) currentHealth = maxHealth;

        currentPanelMaskSize = currentHealth * (PanelMaskSize / maxHealth);
        mask.padding = new Vector4(0, 0, 0, currentPanelMaskSize);
    }

    public void pickUpHealth()
    {
        currentHealth += healthPickUpAmount;
        healthPickUpAmount -= healthEffectivenessDecrese;
    }
}
