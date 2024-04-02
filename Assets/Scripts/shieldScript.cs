using UnityEngine;

public class MoveAwayFromPlayer : MonoBehaviour
{
    public Transform playerTransform;  // The object to move away from
    public float moveSpeed = 5f;    // Speed at which the object moves
    public float maxDistance = 10f;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    public void setTarget(Transform targetTransform)
    {
        playerTransform = targetTransform;
    }

    private void Update()
    {
        // Calculate the direction from this object to the player
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        // Calculate the distance between this object and the player
        float distanceToPlayer = directionToPlayer.x;

        // Calculate the target position to move away from the player
        Vector3 targetPosition = transform.position;

        // Move to the left of the player if the object is to the right
        if (distanceToPlayer > 0)
        {
            targetPosition.x -= Mathf.Abs(distanceToPlayer) + 1f; // Adding 1 unit extra for safety
        }
        // Move to the right of the player if the object is to the left
        else
        {
            targetPosition.x += Mathf.Abs(distanceToPlayer) + 1f; // Adding 1 unit extra for safety
        }

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the distance from the initial position exceeds the max distance
        if (Vector3.Distance(transform.position, initialPosition) > maxDistance)
        {
            gameObject.SetActive(false); // Set the object to inactive
            transform.position = initialPosition; // Reset position to the initial position
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Enemy")
        {
            // Damages enemies, maybe add knockback? 
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(gameObject.transform, 2);
        }
    }

}
