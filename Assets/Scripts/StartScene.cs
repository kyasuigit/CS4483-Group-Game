using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public GameObject levelEndScreen;
    private float loadTimer = 0f;

    private Color startingColor;
    private Color targetColor;

    private void Start()
    {
        startingColor = levelEndScreen.GetComponent<Image>().color;
        targetColor = new Color(startingColor.r, startingColor.g, startingColor.b, 0f);
    }
    private void Update()
    {
        
        loadTimer += Time.deltaTime;
        float t = Mathf.Clamp01(loadTimer / 2);
        levelEndScreen.GetComponent<Image>().color = Color.Lerp(startingColor, targetColor, t);

        if (t >= 1f)
        {
            levelEndScreen.SetActive(false);
            Destroy(gameObject);
        }
        
    }
}
