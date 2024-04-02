using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sword : MonoBehaviour
{

    public int damage;
    public PlayerHealth playerStats;
    [SerializeField] private bool isGuardian = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Enemy")
        {
            if (!isGuardian)
            {
                playerStats.updateRage(playerStats.getRage() + 1);
            }
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(gameObject.transform, damage);
        }
    }
}
