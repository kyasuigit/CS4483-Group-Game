using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hellhound : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject assassin;

    public bool flip;

    private bool dead = false;
    private Animator animator;
    private Rigidbody2D rb;

    private float deathTimer = 0f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip deathAudio;

    public float knockbackTimer;
    public float knockbackPower;

    public float speed;

    public float knockbackTotalTime;
    private bool knockFromLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (assassin.activeSelf)
        {
            playerTransform = assassin.transform;
        }
    }

    void Update()
    {
        
        if (Vector2.Distance(playerTransform.transform.position, transform.position) < 10f && !dead)
        {

            if (knockbackTimer <= 0)
            {

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerTransform.position.x, transform.position.y, transform.position.z), Time.deltaTime * speed);

                Vector3 localScale = transform.localScale;

                if (playerTransform.position.x > transform.position.x)
                {
                    localScale.x = -1 * Mathf.Abs(localScale.x) * (flip ? -1 : 1);
                }
                else
                {
                    localScale.x = Mathf.Abs(localScale.x) * (flip ? -1 : 1);
                }

                transform.localScale = localScale;

                animator.SetBool("chasing", true);
            }

            else
            {
                knockbackTimer -= Time.deltaTime;
                if (knockFromLeft)
                {
                    rb.velocity = new Vector2(-1 * knockbackPower, 0f);
                }
                else
                {
                    rb.velocity = new Vector2(knockbackPower, 0f);
                }
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

    public void Knockback(Transform takenFrom)  
    {
        knockbackTimer = knockbackTotalTime;
        if (takenFrom.position.x <= transform.position.x)
        {
            knockFromLeft = false;
        }
        else
        {
            knockFromLeft = true;
        }
    }
    public void Die()
    {
        audioSource.clip = deathAudio;
        audioSource.Play();
        animator.SetBool("killed", true);
        deathTimer = 0.3f;
        dead = true; 
    }
}
