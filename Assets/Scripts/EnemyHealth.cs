using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyDamage;
    public float health;
    public float maxHealth;
    public bool isBoss;
    private bool phase2Triggered = false, phase3Triggered = false;
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
            else if (gameObject.GetComponent<Minicrow>())
            {
                gameObject.GetComponent<Minicrow>().Die();
            }
            else if (gameObject.GetComponent<TheHeartHoarder>())
            {
                gameObject.GetComponent<TheHeartHoarder>().Die();
            }
        }

        if (isBoss)
        {
            if (health < (66) && !phase2Triggered)
            {
                GetComponent<TheHeartHoarder>().EnterPhase2();
                phase2Triggered = true;
            }
            else if (health < (33) && !phase3Triggered)
            {
                GetComponent<TheHeartHoarder>().EnterPhase3();
                phase3Triggered = true;
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
