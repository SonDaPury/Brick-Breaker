using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExtraBallManager : MonoBehaviour
{
    private BallController ballController;
    private GameManager gameManager;
    public float ballWaitTime;
    private float ballWaitTimeSeconds;
    public int numberOfExtraBalls;
    public int numberOfBallsToFire;
    public ObjectPool objectPool;
    public TextMeshProUGUI numberOfBallsText;

    private void Start()
    {
        ballController = FindAnyObjectByType<BallController>();
        gameManager = FindAnyObjectByType<GameManager>();
        ballWaitTimeSeconds = ballWaitTime;
        numberOfExtraBalls = 0;
        numberOfBallsToFire = 0;
        numberOfBallsText.text = "x" + 1;
    }

    private void Update()
    {
        numberOfBallsText.text = "x" + (numberOfExtraBalls + 1);

        if (ballController.currentBallState == BallController.ballState.fire)
        {
            if (numberOfBallsToFire > 0)
            {
                ballWaitTimeSeconds -= Time.deltaTime;

                if (ballWaitTimeSeconds <= 0)
                {
                    var ball = objectPool.GetPooledObject("ExtraBall");

                    if (ball != null)
                    {
                        ball.transform.position = ballController.ballLaunchPosition;
                        ball.SetActive(true);
                        gameManager.ballsInScene.Add(ball);
                        ball.GetComponent<Rigidbody2D>().velocity =
                            12 * ballController.tempVelocity;
                        ballWaitTimeSeconds = ballWaitTime;
                        numberOfBallsToFire--;
                    }
                    ballWaitTimeSeconds = ballWaitTime;
                }
            }
        }
        if (ballController.currentBallState == BallController.ballState.endShot)
        {
            numberOfBallsToFire = numberOfExtraBalls;
        }
    }
}
