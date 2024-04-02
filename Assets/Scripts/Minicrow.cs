 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minicrow : MonoBehaviour
{
    private GameObject player;

    public float crowSpeed;
    private bool facingLeft = true;

    private float disappearTime = 4.3f;
    private float timer;
    private bool dead = false;

    private float deathTimer = 0.3f;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = disappearTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 3f) { 
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, crowSpeed * Time.deltaTime);
        }
        if (player.transform.position.x > transform.position.x && facingLeft)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            facingLeft = false;
        }
        else if (player.transform.position.x < transform.position.x && !facingLeft)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            facingLeft = true;
        }

        timer -= Time.deltaTime;

        if (timer < 0 || dead)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer < 0f)
            {
                Destroy(gameObject);
            }
        }
    }
    public void Die()
    {
        animator.SetBool("killed", true);
        dead = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player") {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(3);
            Die();
        }
        else if (LayerMask.LayerToName(collision.gameObject.layer) == "Sword" )
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            Die();
        }
        else if (LayerMask.LayerToName(collision.gameObject.layer) == "Sword")
        {
            Die();
        }
    }
}
