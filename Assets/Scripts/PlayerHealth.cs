using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float health;
    public float invincibilityTime;
    private float invincibilityTimer;

    private void Start()
    {
        health = maxHealth;
    }
    public void Update()
    {
        if (invincibilityTimer >= 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (invincibilityTimer < 0)
        {
            if (LayerMask.LayerToName(collision.gameObject.layer) == "Enemy")
            {
                TakeDamage(collision.gameObject.GetComponent<EnemyHealth>().getEnemyDamage());
            }
        }
    }

    
    public void TakeDamage(float damageAmount)
    {

        health -= damageAmount;
        invincibilityTimer = invincibilityTime;
        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            gameObject.GetComponent<PlayerMovement>().PlayerDeath();
        }
    }

    public IEnumerator FlashRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public float getHealth()
    {
        return health;
    }
}