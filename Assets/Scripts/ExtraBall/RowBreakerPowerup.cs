using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowBreakerPowerup : MonoBehaviour
{
    public GameManager gameManager;
    public BrickHealthManager brickHealthManager;
    public BallController ballController;
    public GameObject laserPrefab;
    public bool isCollideRowBreakerPowerup = false;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        brickHealthManager = FindAnyObjectByType<BrickHealthManager>();
        ballController = FindAnyObjectByType<BallController>();
    }

    private void OnEnable()
    {
        isCollideRowBreakerPowerup = false;
    }

    private void Update()
    {
        if (
            ballController.currentBallState == BallController.ballState.endShot
            && isCollideRowBreakerPowerup
        )
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("ExtraBall"))
        {
            isCollideRowBreakerPowerup = true;
            AudioManager.Instance.PlaySoundEffect(3);
            BreakRow(transform.position.y);
            ActivateLaser(transform.position);
        }
    }

    public void BreakRow(float rowY)
    {
        List<GameObject> bricksToDestroy = new List<GameObject>();
        foreach (GameObject brick in gameManager.bricksInScene)
        {
            if (Mathf.Abs(brick.transform.position.y - rowY) < 0.1f)
            {
                if (brick.CompareTag("SquareBrick") || brick.CompareTag("TriangleBrick"))
                {
                    bricksToDestroy.Add(brick);
                }
            }
        }

        foreach (GameObject brick in bricksToDestroy)
        {
            brick.GetComponent<BrickHealthManager>().TakeDamage(1);
        }
    }

    public void ActivateLaser(Vector3 startPosition)
    {
        StartCoroutine(FireLaser(startPosition));
    }

    private IEnumerator FireLaser(Vector3 startPosition)
    {
        var laserInstance = Instantiate(laserPrefab, startPosition, Quaternion.identity);

        yield return new WaitForSeconds(0.1f);

        Destroy(laserInstance);
    }
}
