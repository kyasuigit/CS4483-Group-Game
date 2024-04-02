using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public TMP_Text textBox;
    public GameObject guardianPlayer;
    public GameObject assassinPlayer;
    private GameObject currentPlayer;

    void Start()
    {
        if (assassinPlayer.activeSelf)
        {
            currentPlayer = assassinPlayer;
        }
        else
        {
            currentPlayer = guardianPlayer;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (currentPlayer != null)
        {
            textBox.text = currentPlayer.GetComponent<PlayerHealth>().getHealth().ToString();
        }
    }
}
