using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBall : MonoBehaviour
{
    public bool hasCollidedWithBrick = false;
    private Coroutine noCollisionCoroutine;
    public GameObject splitBallPowerupPrefab;

    public void StartBallMovement()
    {
        hasCollidedWithBrick = false;
        noCollisionCoroutine = StartCoroutine(CheckForNoCollision(15f));
    }

    private IEnumerator CheckForNoCollision(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (!hasCollidedWithBrick)
        {
            SpawnSplitBallPowerup();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (
            collision.gameObject.CompareTag("SquareBrick")
            || collision.gameObject.CompareTag("TriangleBrick")
            || collision.gameObject.CompareTag("Ground")
        )
        {
            hasCollidedWithBrick = true;

            if (noCollisionCoroutine != null)
            {
                StopCoroutine(noCollisionCoroutine);
            }
        }
    }

    private void SpawnSplitBallPowerup()
    {
        Vector3 spawnPosition = transform.position;
        Instantiate(splitBallPowerupPrefab, spawnPosition, Quaternion.identity);
    }
}
