using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBallPowerup : MonoBehaviour
{
    public GameManager gameManager;
    public BallController ballController;
    public bool isCollideSplitBallPowerup = false;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        ballController = FindObjectOfType<BallController>();
    }

    private void OnEnable()
    {
        isCollideSplitBallPowerup = false;
    }

    private void Update()
    {
        if (
            ballController.currentBallState == BallController.ballState.endShot
            && isCollideSplitBallPowerup
        )
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ball") || collider.gameObject.CompareTag("ExtraBall"))
        {
            isCollideSplitBallPowerup = true;
            SplitBalls(collider.gameObject);
        }
    }

    private void SplitBalls(GameObject ball)
    {
        var allBalls = gameManager.ballsInScene;

        foreach (var b in allBalls)
        {
            if (b.gameObject == ball)
            {
                Vector2 direction = GetRandomDirection();

                b.GetComponent<Rigidbody2D>().velocity = direction * ballController.constantSpeed;
            }
        }
    }

    private Vector2 GetRandomDirection()
    {
        Vector2[] directions = new Vector2[]
        {
            new Vector2(1, 1).normalized,
            new Vector2(1, 0).normalized,
            new Vector2(-1, 1).normalized,
        };

        int randomIndex = Random.Range(0, directions.Length);
        return directions[randomIndex];
    }
}
