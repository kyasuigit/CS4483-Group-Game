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


    public AudioSource audio;
    private bool facingLeft = true;

    private float attackTimer = 0f;
    public GameObject birdPrefab;
    public float birdSpeed;

    private GameObject crow1;
    private GameObject crow2;
    private GameObject crow3;
    private GameObject crow4;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAudio()
    {
        audio.Play();
    }

    void Update()
    {

        if (Vector2.Distance(playerTransform.transform.position, transform.position) < 9f && !dead)
        {
            if (attackTimer <= 0)
            {
                attackTimer = attackTime;
                SpawnCrows();
            }
            else
            {
                attackTimer -= Time.deltaTime;
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

        crow1 = Instantiate(birdPrefab, transform.position + new Vector3(0, 3, 0), Quaternion.identity) ;
        crow2 = Instantiate(birdPrefab, transform.position + new Vector3(0, -3, 0), Quaternion.identity);
        crow3= Instantiate(birdPrefab, transform.position + new Vector3(3, 0, 0), Quaternion.identity);
        crow4 = Instantiate(birdPrefab, transform.position + new Vector3(-3, 0, 0), Quaternion.identity);

    }

    public void Die()
    {

        animator.SetBool("killed", true);
        deathTimer = 0.3f;
        dead = true;
    }
}
