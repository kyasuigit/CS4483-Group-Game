using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyDamage;
    public float health;
    public float maxHealth;
    public bool isBoss;
    [SerializeField] HealthBar healthBar;

    public void TakeDamage(Transform takenFrom, float damageAmount)
    {

        health -= damageAmount;
        StartCoroutine(FlashRed());
        if (gameObject.GetComponent<Hellhound>())
            gameObject.GetComponent<Hellhound>().Knockback(takenFrom);
        
        healthBar.updateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            if (gameObject.GetComponent<Hellhound>())
                gameObject.GetComponent<Hellhound>().Die();
            else if (gameObject.GetComponent<Ogre>())
                gameObject.GetComponent<Ogre>().Die();
            else if (gameObject.GetComponent<EyeDemon>())
            {
                gameObject.GetComponent<EyeDemon>().Die();
            }
            else if (gameObject.GetComponent<CrowScript>())
            {
                gameObject.GetComponent<CrowScript>().Die();
            }
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
