using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossEntryPoint : MonoBehaviour
{
    public Camera cameraPan;
    public GameObject leftWall;

    public Tilemap tilemap;
    public float duration = 1.3f;

    private bool wallIsAppearing = false;

    private float timer = 0f;
    private Color startingColor;
    private Color targetColor;

    public AudioSource bgMusicAudio;
    public AudioClip bgMusic;

    private void Start()
    { 
        startingColor = tilemap.color;
        targetColor = new Color(startingColor.r, startingColor.g, startingColor.b, 1f);
    }

    private void Update()
    {
        if (wallIsAppearing) { 
            timer += Time.deltaTime;

            float t = Mathf.Clamp01(timer / duration);
            tilemap.color = Color.Lerp(startingColor, targetColor, t);

            if (t >= 1f)
            {
                
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            if (!wallIsAppearing) {
                bgMusicAudio.clip = bgMusic;
                bgMusicAudio.Play();
                cameraPan.GetComponent<CameraFollow>().BossTime();
                leftWall.SetActive(true);
                wallIsAppearing = true;
            }
        }
    }
}
