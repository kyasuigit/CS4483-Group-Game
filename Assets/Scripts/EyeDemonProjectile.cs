using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeDemonProjectile : MonoBehaviour
{

    public float aliveTime;
    public float dmg;
    public Vector3 rotationSpeed;

    private float aliveTimer;
    // Start is called before the first frame update
    void Start()
    {
        aliveTimer = aliveTime;
    }

    // Update is called once per frame
    void Update()
    {
        aliveTimer -= Time.deltaTime;

        if (aliveTimer < 0)
        {
            Destroy(gameObject);
        }
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(dmg);
            Destroy(gameObject);
        }

        else if (LayerMask.LayerToName(collision.gameObject.layer) == "Ground")
        {
            Destroy(gameObject);

        }

    }
}
