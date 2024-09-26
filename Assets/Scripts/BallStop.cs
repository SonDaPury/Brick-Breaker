using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStop : MonoBehaviour
{
    public Rigidbody2D rbBall;
    public BallController ballController;
    public int collisionCount;

    private void Start()
    {
        collisionCount = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            collisionCount++;
            rbBall.velocity = Vector2.zero;

            if (collisionCount > 1)
            {
                ballController.currentBallState = BallController.ballState.wait;
            }
        }
    }
}
