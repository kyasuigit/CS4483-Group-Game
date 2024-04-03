using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NextLevel : MonoBehaviour
{
    public string nextSceneName;
    public GameObject levelEndScreen; 
    private float loadTimer = -1f;

    private Color startingColor;
    private Color targetColor;

    private void Start()
    {
        startingColor = levelEndScreen.GetComponent<Image>().color;
        targetColor = new Color(startingColor.r, startingColor.g, startingColor.b, 1f);
    }
    private void Update()
    {
        if (loadTimer >= 0)
        {
            loadTimer += Time.deltaTime;
            float t = Mathf.Clamp01(loadTimer / 2);
            levelEndScreen.GetComponent<Image>().color = Color.Lerp(startingColor, targetColor, t);

            if (t >= 1f)
            {

                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            levelEndScreen.SetActive(true);
            loadTimer = 0f;
        }
    }
}
