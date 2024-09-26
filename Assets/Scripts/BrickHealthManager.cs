using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BrickHealthManager : MonoBehaviour
{
    public int brickHealth;
    public GameManager gameManager;

    [SerializeField]
    private TextMeshProUGUI brickHealthText;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Start()
    {
        brickHealth = gameManager.level;
    }

    private void Update()
    {
        brickHealthText.text = " " + brickHealth.ToString();

        if (brickHealth <= 0) { }
    }

    private void TakeDamage(int damageToTake)
    {
        brickHealth -= damageToTake;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage(1);
        }
    }
}
