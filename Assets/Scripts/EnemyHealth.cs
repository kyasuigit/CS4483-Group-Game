using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyDamage;
    public float health;
    public float maxHealth;
    [SerializeField] HealthBar healthBar;

    public void TakeDamage(Transform takenFrom, float damageAmount)
    {

        health -= damageAmount;
        StartCoroutine(FlashRed());
        gameObject.GetComponent<Hellhound>().Knockback(takenFrom);
        healthBar.updateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            gameObject.GetComponent<Hellhound>().Die();
        }
    }


    public IEnumerator FlashRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

   

    public float getEnemyDamage()
    {
        return enemyDamage;
    }

}
