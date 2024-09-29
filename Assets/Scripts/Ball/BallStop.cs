using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStop : MonoBehaviour
{
    public Rigidbody2D rbBall;
    public BallController ballController;
    public int collisionCount;
    private GameManager gameManager;
    public ExtraBallManager extraBallManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        extraBallManager = FindAnyObjectByType<ExtraBallManager>();
    }

    private void Start()
    {
        collisionCount = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            extraBallManager.numberOfBallsText.transform.position = extraBallManager
                .bottomOfBall
                .position;

            collisionCount++;
            rbBall.velocity = Vector2.zero;

            if (collisionCount > 1)
            {
                ballController.currentBallState = BallController.ballState.wait;
            }
        }
        if (collision.gameObject.CompareTag("ExtraBall"))
        {
            gameManager.ballsInScene.Remove(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
}
