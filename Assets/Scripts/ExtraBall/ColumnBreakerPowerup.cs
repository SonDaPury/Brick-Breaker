using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnBreakerPowerup : MonoBehaviour
{
    public GameManager gameManager;
    public BrickHealthManager brickHealthManager;
    public BallController ballController;
    public GameObject laserPrefab;
    public bool isCollideColumnBreakerPowerup = false;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        brickHealthManager = FindAnyObjectByType<BrickHealthManager>();
        ballController = FindAnyObjectByType<BallController>();
    }

    private void OnEnable()
    {
        isCollideColumnBreakerPowerup = false;
    }

    private void Update()
    {
        if (
            ballController.currentBallState == BallController.ballState.endShot
            && isCollideColumnBreakerPowerup
        )
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("ExtraBall"))
        {
            isCollideColumnBreakerPowerup = true;
            AudioManager.Instance.PlaySoundEffect(3);
            BreakColumn(transform.position.x);
            ActivateLaser(transform.position);
        }
    }

    public void BreakColumn(float rowX)
    {
        List<GameObject> bricksToDestroy = new List<GameObject>();
        foreach (GameObject brick in gameManager.bricksInScene)
        {
            if (Mathf.Abs(brick.transform.position.x - rowX) < 0.1f)
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
        var laserInstance = Instantiate(laserPrefab, startPosition, Quaternion.Euler(0, 0, 90));

        yield return new WaitForSeconds(0.1f);

        Destroy(laserInstance);
    }
}
