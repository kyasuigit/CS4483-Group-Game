using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(3f, 0f, -10f);
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.25f;

    [SerializeField] private Transform target;
    [SerializeField] private GameObject player;
    void Update()
    {
        offset = player.GetComponent<PlayerMovement>().facingRight() ? new Vector3(1.5f, 0f, -10f) : new Vector3(-1.5f, 0f, -10f);
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
