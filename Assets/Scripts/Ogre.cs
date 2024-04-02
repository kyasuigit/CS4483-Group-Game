using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogre : MonoBehaviour
{
    public Transform playerTransform;

    public bool flip;

    private bool dead = false;
    private Animator animator;

    private float deathTimer = 0f;

    public float speed;
    public Transform pointA;
    public Transform pointB;
    public float attackTime;
    public AudioSource audio;
    public GameObject assassin;

    private float attackTimer = 0;
    private bool movingToA = true;
    private bool attacking = false;
    public GameObject attackHitBox;


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
    public void EnableHitBox()
    {
        attackHitBox.SetActive(true);
    }

    public void DisableHitBox()
    {
        attackHitBox.SetActive(false);
    }

    void Update()
    {

        if (Vector2.Distance(playerTransform.transform.position, transform.position) < 4f && !dead)
        {
            if ((playerTransform.transform.position.x > transform.position.x && movingToA && !attacking) || (playerTransform.transform.position.x < transform.position.x && !movingToA && !attacking))
            {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
                attacking = true;
            }
            if (attackTimer <= 0)
            {
                attackTimer = attackTime;

                animator.SetTrigger("attacking");
            }
            attackTimer -= Time.deltaTime;
        }
        else if (dead)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer < 0f)
            {
                Destroy(gameObject);
            }
        }
        else if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        else if (dead)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer < 0f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            attackHitBox.SetActive(false);

            if (attacking)
            {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
                attacking = false;
            }
            Transform targetPoint = movingToA ? pointA : pointB;

            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

            animator.SetTrigger("walking");

            if (Vector2.Distance(transform.position, targetPoint.position) < 2f && movingToA)
            {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
                movingToA = false;
            }
            else if (Vector2.Distance(transform.position, targetPoint.position) < 2f && !movingToA)
            {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
                movingToA = true;
            }
        }
    }

    public void Die()
    {       
        animator.SetBool("killed", true);
        deathTimer = 0.3f;
        dead = true;
    }
}
