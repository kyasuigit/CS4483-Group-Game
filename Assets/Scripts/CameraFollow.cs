using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform bossPosition;
    public float zoomSize;
    public float transitionSpeed;

    private Vector3 offset = new Vector3(3f, 0f, -10f);
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.25f;
    private bool isBoss = false;

    [SerializeField] private Transform target;
    [SerializeField] private GameObject player;


    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        if (!isBoss) { 
            offset = player.GetComponent<PlayerMovement>().facingRight() ? new Vector3(1.5f, 0f, -10f) : new Vector3(-1.5f, 0f, -10f);
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            if (bossPosition)
            {
                Vector3 desiredPosition = bossPosition.position + new Vector3(0, 5, 0);
                desiredPosition.z = mainCamera.transform.position.z;
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, desiredPosition, Time.deltaTime * transitionSpeed);

                mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, zoomSize, Time.deltaTime * transitionSpeed);
            }
        }
    }

    public void BossTime()
    {
        isBoss = true;
    }
}
