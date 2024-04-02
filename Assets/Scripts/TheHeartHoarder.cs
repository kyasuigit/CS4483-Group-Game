using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHeartHoarder : MonoBehaviour
{
    public Animator animator;
    public float startTime;
    public Transform[] spawnPoints;

    private bool isFighting = false;
    private float startTimer;
    // Start is called before the first frame update
    void Start()
    {
        startTimer = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer > 0)
        {
            startTimer -= Time.deltaTime;
        }
        else if (!isFighting)
        {
            animator.SetBool("isFighting", true);
            isFighting = true;
        }
    }

    void Attack1()
    {

    }

    void Attack2()
    {

    }

    void Attack3()
    {

    }
}
