using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowScript : MonoBehaviour
{
    public Transform playerTransform;

    public bool flip;

    private bool dead = false;
    private Animator animator;

    private float deathTimer = 0f;

    public float speed;
    public float attackTime;


    public AudioSource audioSource;
    private bool facingLeft = true;

    private float attackTimer = 0f;
    public GameObject birdPrefab;
    public float birdSpeed;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAudio()
    {
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {

        if (Vector2.Distance(playerTransform.transform.position, transform.position) < 9f && !dead)
        {
            if (attackTimer <= 0)
            {
                animator.SetBool("isSummoning", true);
                attackTimer = attackTime;
                SpawnCrows();
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
            if (attackTimer < 3f)
            {
                animator.SetBool("isSummoning", false);
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

    public void SpawnCrows()
    {

        Instantiate(birdPrefab, transform.position + new Vector3(0, 3, 0), Quaternion.identity) ;
        Instantiate(birdPrefab, transform.position + new Vector3(0, -3, 0), Quaternion.identity);
        Instantiate(birdPrefab, transform.position + new Vector3(3, 0, 0), Quaternion.identity);
        Instantiate(birdPrefab, transform.position + new Vector3(-3, 0, 0), Quaternion.identity);

    }

    public void Die()
    {

        animator.SetBool("killed", true);
        deathTimer = 0.3f;
        dead = true;
    }
}
