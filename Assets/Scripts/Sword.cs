using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sword : MonoBehaviour
{

    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(gameObject.transform, damage);
        }
    }
}
