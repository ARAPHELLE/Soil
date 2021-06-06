using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health = 100;
    public float stamina = 100;
    public float hunger = 100;

    public int maxHealth = 100, maxStamina = 100, maxHunger = 100;

    public int jumpDrain = 10;

    public float jumpForce = 10.0f;

    public float speed = 30.0f;

    public float walkSpeed = 30.0f;

    public float runSpeed;

    public bool grounded;

    public bool sprinting;

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

        runSpeed = walkSpeed * 1.25f;

        UpdateHealthAmountText();

        float healthFillPercent = (float)health / (float)maxHealth;

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, healthFillPercent, Time.deltaTime * 8.0f);

        float staminaFillPercent = (float)stamina / (float)maxStamina;

        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, staminaFillPercent, Time.deltaTime * 8.0f);

        float hungerFillPercent = (float)hunger / (float)maxHunger;

        hungerBar.fillAmount = Mathf.Lerp(hungerBar.fillAmount, hungerFillPercent, Time.deltaTime * 8.0f);
    }

    private void FixedUpdate()
    {
        if (!sprinting && grounded && hunger > 0.0f)
        {
            stamina += 0.25f;
        }

        float num = 1.0f;

        if (sprinting)
        {
            num *= 5.0f;
        }

        hunger -= 0.15f * Time.deltaTime * num;
    }

    void UpdateHealthAmountText()
    {
        healthAmountText.text = $"Health: {health}/{maxHealth}";
    }

    public bool CanRun()
    {
        return stamina > 0;
    }

    public bool CanJump()
    {
        return stamina >= jumpDrain;
    }
}
