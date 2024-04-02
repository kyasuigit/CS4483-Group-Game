using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("WOW");
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Enemy")
        {
            Debug.Log("TEST");
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(gameObject.transform, damage);
        }
    }
}
