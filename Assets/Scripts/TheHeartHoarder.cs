using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHeartHoarder : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public GameObject Guardian;
    public float appearTime;
    public float startTime;
    public float attack1Time;
    public float attack1Speed;
    public AudioSource audioSource;
    public AudioClip deathSound;
    
    public Transform[] spawnPoints;
    public Transform pointA, pointB;
    public Transform phase2Point;

    private bool isFighting = false;
    private float appearTimer;
    private float startTimer;
    private float phase2Timer;
    private float attack1Timer, attack2Timer, attack3Timer;

    private bool phase1 = false;
    private bool phase2 = false;
    private bool phase3 = false;

    private bool movingToA = false;

    private float deathTimer;
    private bool dead = false;

    [Header("Attacks")]
    public GameObject attack1;
    public GameObject attack2;
    public GameObject attack3;

    // Update is called once per frame

    private void Start()
    {
        PlayerChoice.CharacterChoice = "Guardian";
        if (PlayerChoice.CharacterChoice == "Guardian")
        {
            player = Guardian;
        }
    }
    void Update()
    {
        if (dead)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer < 0f)
            {
                Destroy(gameObject);
            }
        }
        else if (appearTimer > 0)
        {
            Appear();
        }
        else if (startTimer > 0) {
            Idle();
        }
        else if (phase1)
        {
            Attack1();
        }
        else if (phase2)
        {
            Attack2();
        }
        else if (phase3)
        {
            Attack3();
        }
    }

    public void Appear()
    {
        if (appearTimer > 0)
        {
            appearTimer -= Time.deltaTime;
        }
        if (appearTimer <= 0)
        {
            startTimer = startTime;
            animator.SetTrigger("isIdling");
        }
    }

    public void Idle()
    {
        if (startTimer > 0)
        {
            startTimer -= Time.deltaTime;
        }
        if (startTimer <= 0)
        {
            phase1 = true;
            animator.SetBool("isAttacking", true);
        }
    }

    void Attack1()
    {
        Transform targetPoint = movingToA ? pointA : pointB;

        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, attack1Speed * Time.deltaTime);

        animator.SetBool("attack1", true);

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
    public void RespawnInMiddle()
    {
        transform.position = phase2Point.position;
    }

    void Attack2()
    {
        animator.SetBool("attack2", true);
    }

    void Attack3()
    {
        animator.SetBool("attack3", true);
    }

    public void SpawnInOnePoints()
    {
        transform.position = spawnPoints[Random.Range(0, 5)].position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            player.GetComponent<PlayerHealth>().TakeDamage(5);
        }
    }

    public void EnableAttack1Hitbox()
    {
        attack1.SetActive(true);
    }

    public void DisableAttack1Hitbox()
    {
        attack1.SetActive(false);
    }

    public void EnableAttack2Hitbox()
    {
        attack2.SetActive(true);
    }

    public void DisableAttack2Hitbox()
    {
        attack2.SetActive(false);

    }

    public void EnableAttack3Hitbox()
    {
        attack3.SetActive(true);
    }

    public void DisableAttack3Hitbox()
    {
        attack3.SetActive(false);

    }

    public void EnterPhase2()
    {
        animator.SetTrigger("isVanishing");
        phase1 = false;
        phase2 = true;
        audioSource.Play();
    }

    public void EnterPhase3()
    {
        animator.SetTrigger("isVanishing");
        phase2 = false;
        phase3 = true;
        audioSource.Play();

    }

    public void TriggerBoss()
    {
        appearTimer = appearTime;
        audioSource.Play();

    }
    public void Die()
    {
        audioSource.clip = deathSound;
        audioSource.Play();
        animator.SetBool("killed", true);
        deathTimer = 3.0f;
        dead = true;
    }
}
