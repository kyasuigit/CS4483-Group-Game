using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float health;
    public float invincibilityTime;
    private float invincibilityTimer;
    [SerializeField] Slider rageSlider;
    public float animationDuration = 1f;
    private float targetValue = 0f;
    private float startValue;
    private float startTime;
    private bool reduceDamage = false;

    private void Start()
    {
        health = maxHealth;
        startValue = rageSlider.value;
        startTime = Time.time;
    }

    private void Update()
    {
        if (Mathf.Abs(rageSlider.value - targetValue) > 0.01f)
        {
            float t = (Time.time - startTime) / animationDuration;
            rageSlider.value = Mathf.Lerp(startValue, targetValue, t);
        }
        if (rageSlider.value == 0)
        {
            rageSlider.fillRect.gameObject.SetActive(false);
        }
        else
        {
            rageSlider.fillRect.gameObject.SetActive(true);
        }

        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (invincibilityTimer <= 0 && LayerMask.LayerToName(collision.gameObject.layer) == "Enemy")
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyHealth>().getEnemyDamage());
        }
    }

    public void updateRage(float endValue)
    {
        targetValue = endValue;
        startTime = Time.time;
        startValue = rageSlider.value;
    }

    public bool getDamageReduction()
    {
        return reduceDamage;
    }

    public void toggleDamageReduction()
    {
        if (reduceDamage){
            reduceDamage = false;
        }
        else
        {
            reduceDamage = true;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (reduceDamage)
        {
            health -= damageAmount / 2;
        }
        else
        {
            health -= damageAmount;
        }
        invincibilityTimer = invincibilityTime;
        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            GetComponent<PlayerMovement>().PlayerDeath();
        }
    }

    private IEnumerator FlashRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public float getHealth()
    {
        return health;
    }

    public float getRage()
    {
        return rageSlider.value;
    }

}