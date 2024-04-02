using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2Script: MonoBehaviour
{
    private float horizontal;
    private float speed = 5.5f;
    private float attack1Timer = 0f;
    private float attack2Timer = 0f;
    private float shieldSummonTimer = 0.7f;
    private float deathTimer = 0f;
    private bool dead = false;
    
    private bool isFacingRight = true;

    public PlayerHealth playerStats;

    private float rageAmount;

    private float regen = 1f;
    private bool damageReduction = false;
    private float knockbackEnemies = 5f;
    private bool summons = false;

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

    [SerializeField] private LayerMask groundLayerMask;

    private Coroutine rageDrainCoroutine;
    private float rageDrainRate = 1f;
    public GameObject shield;
    public LayerMask enemyLayer;
    [SerializeField] private GameObject shieldSummon;

    private void Start()
    {
        StartCoroutine(RegenerateHealth());
        shieldSummon.GetComponent<MoveAwayFromPlayer>().setTarget(transform); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            if (playerStats.getRage() <= 0)
            {
                toggleDamageReduction();
            }
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
                animator.SetTrigger("Jump");
                animator.SetBool("Grounded", false);
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                GameObject.Find("Assassin").SetActive(true);
            }

            else if (isGrounded())
            {
                animator.SetBool("Grounded", isGrounded());
            }
            animator.SetFloat("AirSpeedY", rb.velocity.y);

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                animator.SetTrigger("Jump");
                animator.SetBool("Grounded", isGrounded());
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
            else if (Input.GetKeyDown(KeyCode.R))
            {
                toggleDamageReduction();
            }
            if (shieldSummonTimer >= 0)
            {
                shieldSummonTimer -= Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && playerStats.getRage() > 0 && shieldSummonTimer <= 0)
            {
                summonShields();
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

    // Method to start draining rage continuously

    private void toggleDamageReduction()
    {

        if (playerStats.getRage() > 0 && !playerStats.getDamageReduction())
        {
            shield.SetActive(true);
            playerStats.toggleDamageReduction();
            StartRageDrain();
        }
        else if (playerStats.getDamageReduction() == true)
        {
            shield.SetActive(false);
            playerStats.toggleDamageReduction();
            StopRageDrain();
        }
    }

    private void summonShields()
    {
        playerStats.updateRage(playerStats.getRage() - 3);
        Vector3 shield1Pos = new Vector3(transform.position.x - 2, transform.position.y + 1.3f, transform.position.z);
        Instantiate(shieldSummon, shield1Pos, Quaternion.identity);

        Vector3 shield2Pos = new Vector3(transform.position.x + 2, transform.position.y + 1.3f, transform.position.z);
        Instantiate(shieldSummon, shield2Pos, Quaternion.identity);
    }

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
                playerStats.toggleDamageReduction();
                StopRageDrain();
            }
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FixedUpdate()
    {

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

    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Wait for 5 seconds before regen

            if (playerStats.getHealth() < playerStats.maxHealth)
            {
                playerStats.changeHealth(playerStats.getHealth() + 1); // Increment health by 1
            }
        }
    }

    public void PlayerDeath()
    {
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Die");
        dead = true;
        deathTimer = 1.5f;
    }

    public bool facingRight()
    {
        return isFacingRight;
    }
}
