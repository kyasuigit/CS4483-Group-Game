using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeDemon : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject assassin;

    public bool flip;

    private bool dead = false;
    private Animator animator;

    private float deathTimer = 0f;

    public float speed;
    public float attackTime;


    public AudioSource audio;
    private bool facingLeft = true;

    private float attackTimer = 0f;
    public GameObject fireballPrefab;
    public float fireballSpeed;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (PlayerChoice.CharacterChoice == "Assassin")
        {
            playerTransform = assassin.transform;
        }
    }

    public void PlayAudio()
    {
        audio.Play();
    }

    void Update()
    {

        if (Vector2.Distance(playerTransform.transform.position, transform.position) < 12f && !dead)
        {
            if (Vector2.Distance(playerTransform.transform.position, transform.position) > 7f) { 
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            }
            else
            {
                if  (attackTimer <= 0)
                {
                    attackTimer = attackTime;
                    ShootFireball();
                }
                else
                {
                    attackTimer -= Time.deltaTime;
                }
            }


            if (playerTransform.position.x > transform.position.x && facingLeft)
            {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
                facingLeft = false;
            }
            else if (playerTransform.position.x < transform.position.x && !facingLeft)
            {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
                facingLeft = true;
            }

        }
        else if (dead)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer < 0f)
            {
                Destroy(gameObject);
            }
        }
       
    }

    public void ShootFireball()
    {

        GameObject fireBall = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        fireBall.GetComponent<Rigidbody2D>().velocity = direction * fireballSpeed;
    }

    public void Die()
    {

        animator.SetBool("killed", true);
        deathTimer = 0.25f;
        dead = true;
    }
}
