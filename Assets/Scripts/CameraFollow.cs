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

    private bool followGuardian = false;

    [SerializeField] private Transform target;
    [SerializeField] private GameObject assassin;
    [SerializeField] private GameObject guardian;


    private Camera mainCamera;

    public void changeTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Start()
    {
        mainCamera = Camera.main;

        if (PlayerChoice.CharacterChoice == "Guardian")
        {
            guardian.SetActive(true);
            followGuardian = true;
        }
        else
        {
            assassin.SetActive(true);
        }

    }
    void Update()
    {
        if (!isBoss) {
            if (!followGuardian)
            {
                offset = assassin.GetComponent<PlayerMovement>().facingRight() ? new Vector3(1.5f, 2.5f, -5f) : new Vector3(-1.5f, 2.5f, -5f);
                changeTarget(assassin.transform);
            }
            else
            {
                offset = guardian.GetComponent<Player2Script>().facingRight() ? new Vector3(1.5f, 0.5f, -5f) : new Vector3(-1.5f, 0.5f, -5f);
                changeTarget(guardian.transform);
            }
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
