using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Endgame : MonoBehaviour
{
    public GameObject levelEndScreen;
    public Image image;
    private float loadTimer = -1f;

    private Color startingColor;
    private Color targetColor;

    private void Start()
    {
        startingColor = image.color;
        targetColor = new Color(startingColor.r, startingColor.g, startingColor.b, 1f);
    }
    private void Update()
    {
        if (loadTimer >= 0)
        {
            loadTimer += Time.deltaTime;
            float t = Mathf.Clamp01(loadTimer / 2);
            image.color = Color.Lerp(startingColor, targetColor, t);

            if (t >= 1f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void StartEndGame()
    {
        levelEndScreen.SetActive(true);
        loadTimer = 0;
    }
}
