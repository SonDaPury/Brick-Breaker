using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverManager : MonoBehaviour
{
    private BallController ballController;
    public GameObject endGamePanel;

    private void Awake()
    {
        ballController = FindAnyObjectByType<BallController>();
    }

    private void Start()
    {
        endGamePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            collision.gameObject.CompareTag("SquareBrick")
            || collision.gameObject.CompareTag("TriangleBrick")
        )
        {
            ballController.currentBallState = BallController.ballState.endGame;
            endGamePanel.SetActive(true);
        }
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
