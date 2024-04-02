using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHeartHoarder : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public float appearTime;
    public float startTime;
    public float attack1Time;
    public float attack1Speed;
    public Transform[] spawnPoints;
    public Transform pointA, pointB;
    private bool isFighting = false;
    private float appearTimer;
    private float startTimer;
    private float attack1Timer, attack2Timer, attack3Timer;

    private bool phase1 = false;
    private bool phase2 = false;
    private bool phase3 = false;

    private bool movingToA = false;

    [Header("Attacks")]
    public GameObject attack1;

    private float health;

   
 
    // Update is called once per frame
    void Update()
    {
        if (appearTimer > 0)
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

    void Attack2()
    {

    }

    void Attack3()
    {

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

    public void TriggerBoss()
    {
        appearTimer = appearTime;
    }
}
