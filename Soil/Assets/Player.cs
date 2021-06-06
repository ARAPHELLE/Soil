using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 100, stamina = 100, hunger = 100;
    private int maxHealth = 100, maxStamina = 100, maxHunger = 100;

    private Image healthBar, hungerBar, staminaBar;
    private Text healthAmountText;

    private void Awake()
    {
        healthBar = GameObject.Find("Canvas/Statbars/BG/HP").GetComponent<Image>();
        hungerBar = GameObject.Find("Canvas/Statbars/BG/HUNGER").GetComponent<Image>();
        staminaBar = GameObject.Find("Canvas/Statbars/BG/STAMINA").GetComponent<Image>();

        healthAmountText = GameObject.Find("Canvas/Statbars/BG/HP/HP Amount").GetComponent<Text>();
    }

    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        hunger = Mathf.Clamp(hunger, 0, maxHunger);
        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        UpdateHealthAmountText();

        float healthFillPercent = (float)health / (float)maxHealth;

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, healthFillPercent, Time.deltaTime * 8.0f);
    }

    void UpdateHealthAmountText()
    {
        healthAmountText.text = $"Health: {health}/{maxHealth}";
    }
}
