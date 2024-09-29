using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {
        scoreText.text = "Score: " + gameManager.level;
    }
}
