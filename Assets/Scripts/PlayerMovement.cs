using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float attack1Timer = 0f;
    private float attack2Timer = 0f;
    private float deathTimer = 0f;
    private bool dead = false;
    
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingTime = 0.2f;
    private float dashingCD = 1f;
    public PlayerHealth playerStats;

    private bool canBolt = true;
    private bool isBolting;
    private float boltTime = 0.55f;
    private float boltCD = 1f;
    private float rageAmount;

    private bool boltUnlocked = false;
    private bool boostUnlocked = false;
    private bool dashUnlocked = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private float jumpingPower = 20f;

    [SerializeField] private float attack1Time;
    [SerializeField] private GameObject attack1Collider;
    [SerializeField] private AudioSource attack1Audio;

    [SerializeField] private float attack2Time;
    [SerializeField] private GameObject attack2Collider;
    [SerializeField] private AudioSource attack2Audio;

    [SerializeField] private GameObject trailRenderer;
    [SerializeField] private float dashingPower = 10f;

    public GameObject boltPrefab;
    [SerializeField] private int boltDmg;
    [SerializeField] private LayerMask groundLayerMask;

    private bool rageSpeedBoost = false;
    private Coroutine rageDrainCoroutine;
    private float rageDrainRate = 1f;
    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (isDashing)
            {
                return;
            }
            horizontal = Input.GetAxisRaw("Horizontal");

            if (horizontal != 0)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            if (Input.GetButtonDown("Jump") && isGrounded())
            {
                animator.SetBool("isJumping", true);
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
            else if (isGrounded())
            {
                animator.SetBool("isJumping", false);
            }
            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                animator.SetBool("isJumping", false);
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            if (Input.GetMouseButtonDown(0) && attack2Timer <= 0)
            {
                animator.SetTrigger("attack1");
                attack1Collider.SetActive(true);
                if (attack1Timer <= 0)
                {
                    attack1Audio.Play();
                    attack1Timer = attack1Time;
                }

            }
            else if (Input.GetMouseButtonDown(1) && attack1Timer <= 0)
            {
                animator.SetTrigger("attack2");
                attack2Collider.SetActive(true);
                if (attack2Timer <= 0)
                {
                    attack2Audio.Play();
                    attack2Timer = attack2Time;

                }
            }

            if (attack1Timer >= 0)
            {
                attack1Timer -= Time.deltaTime;
            }
            else if (attack1Timer < 0 && attack1Collider.activeSelf)
            {
                attack1Collider.SetActive(false);
            }

            

            if (attack2Timer >= 0)
            {
                attack2Timer -= Time.deltaTime;
            }
            else if (attack2Timer < 0 && attack2Collider.activeSelf)
            {
                attack2Collider.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
            }

            if (Input.GetKeyDown(KeyCode.E) && canBolt)
            {
                StartCoroutine(SpawnBolt());
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                UnlockMovespeed();
            }
            Flip();
        }
        else
        {
            if (deathTimer > 0)
            {
                deathTimer -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
                SceneManager.LoadScene(0);
            }
        }

    }

    public void UnlockMovespeed()
    {
        if (playerStats.getRage() > 0 && !rageSpeedBoost) {
            rageSpeedBoost = true;
            speed += 6f;
            jumpingPower += 6f;
            StartRageDrain();
            trailRenderer.SetActive(true);
        }
        else
        {
            trailRenderer.SetActive(false);
            StopRageDrain();
            rageSpeedBoost = false;
            speed = 8f;
            jumpingPower = 20f;
        }
    }

    // Method to start draining rage continuously
    private void StartRageDrain()
    {
        if (rageDrainCoroutine == null)
        {
            rageDrainCoroutine = StartCoroutine(RageDrainCoroutine());
        }
    }

    // Method to stop draining rage
    private void StopRageDrain()
    {
        if (rageDrainCoroutine != null)
        {
            StopCoroutine(rageDrainCoroutine);
            rageDrainCoroutine = null;
        }
    }

    // Coroutine to continuously drain rage
    private IEnumerator RageDrainCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (playerStats.getRage() > 0)
            {
                playerStats.updateRage(playerStats.getRage() - 1);
            }
            else
            {
                StopRageDrain();
                trailRenderer.SetActive(false);
                rageSpeedBoost = false;
                jumpingPower = 20f;
                speed = 8f;
            }
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x = localScale.x * -1;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        if (playerStats.getRage() > 0 && dashUnlocked)
        {
            playerStats.updateRage(playerStats.getRage() - 1);
            canDash = false;
            isDashing = true;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
            //trailRenderer.emitting = true;
            yield return new WaitForSeconds(dashingTime);
            //trailRenderer.emitting = false;
            rb.gravityScale = originalGravity;
            isDashing = false;
            yield return new WaitForSeconds(dashingCD);
            canDash = true;
        }
    }

    private IEnumerator SpawnBolt()
    {

        if (playerStats.getRage() > 0 && boltUnlocked)
        {
            playerStats.updateRage(playerStats.getRage() - 2);
            canBolt = false;
            isBolting = true;
            GameObject bolt;
        
            Vector3 direction = transform.forward;
            Vector3 spawnPosition = transform.position + (isFacingRight ? transform.right * 5 : -transform.right * 5);

            bolt = Instantiate(boltPrefab, spawnPosition, Quaternion.identity);
            bolt.transform.forward = direction;
            yield return new WaitForSeconds(boltTime);
            Destroy(bolt);
       
            isBolting = false;
            yield return new WaitForSeconds(boltCD);
            canBolt = true;
        
        }

    }

    public void PlayerDeath()
    {

        rb.bodyType = RigidbodyType2D.Static;
        animator.SetBool("isDead", true);
        dead = true;
        deathTimer = 1.5f;
    }

    public void unlockMoveSpeed()
    {
        boostUnlocked = true;
    }

    public void unlockDash()
    {
        dashUnlocked = true;
    }

     public void unlockBolt()
    {
        boltUnlocked = true;
    }

    public bool facingRight()
    {
        return isFacingRight;
    }
}
