using UnityEngine;

public class shieldScript : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 5f;
    public float moveDistance = 5f;
    public GameObject shield;

    private Vector3 initialOffset; // Initial offset from the player

    private void Start()
    {
        // Calculate the initial offset from the player
        initialOffset = transform.position - playerTransform.position;
    }

    private void Update()
    {
        // Calculate the target position based on the player's position and the initial offset
        Vector3 targetPosition = playerTransform.position + initialOffset;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the shield has moved far enough away from the player
        if (Vector3.Distance(transform.position, playerTransform.position) >= moveDistance)
        {
            // Reset the shield's position relative to the player
            shield.SetActive(false);
            transform.position = playerTransform.position + initialOffset;
            shield.SetActive(true);
        }
    }
}
