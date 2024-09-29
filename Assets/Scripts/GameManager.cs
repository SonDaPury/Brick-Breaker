using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<Transform> spawnTargets;
    public List<GameObject> bricksInScene;
    public List<GameObject> ballsInScene;
    public GameObject triangleBrick;
    public GameObject squareBrick;
    public GameObject extraBallPowerup;
    private ObjectPool objectPool;
    public int numberOfBricksToStart;
    public int level;
    public int numberOfExtraBallsInRow = 0;

    private void Awake()
    {
        objectPool = FindAnyObjectByType<ObjectPool>();
    }

    private void Start()
    {
        level = 1;

        spawnTargets = GetRandomSpawnPoint(spawnPoints, 2);

        bool hasExtraBall = false;

        foreach (Transform spawnPoint in spawnTargets)
        {
            int brickToCreate = Random.Range(0, 2);

            if (!hasExtraBall)
            {
                bricksInScene.Add(
                    Instantiate(extraBallPowerup, spawnPoint.position, Quaternion.identity)
                );
                hasExtraBall = true;
            }
            else
            {
                if (brickToCreate == 0)
                {
                    bricksInScene.Add(
                        Instantiate(squareBrick, spawnPoint.position, Quaternion.identity)
                    );
                }
                else if (brickToCreate == 1)
                {
                    bricksInScene.Add(
                        Instantiate(triangleBrick, spawnPoint.position, Quaternion.identity)
                    );
                }
            }
        }
    }

    public void PlaceBricks()
    {
        level++;
        int targetsCount =
            level < 10 ? 3
            : level < 50 ? 4
            : level < 100 ? 5
            : 6;
        spawnTargets = GetRandomSpawnPoint(spawnPoints, targetsCount);

        bool hasExtraBall = false;
        bool hasRowBreaker = false;
        bool hasColumnBreaker = false;
        bool hasSplitBall = false;

        foreach (Transform spawnPoint in spawnTargets)
        {
            if (!hasRowBreaker && Random.Range(0, 101) < 10)
            {
                GameObject rowBreaker = objectPool.GetPooledObject("RowBreakerPowerup");
                bricksInScene.Add(rowBreaker);
                if (rowBreaker != null)
                {
                    rowBreaker.transform.position = spawnPoint.position;
                    rowBreaker.transform.rotation = Quaternion.identity;
                    rowBreaker.SetActive(true);
                    hasRowBreaker = true;
                }
            }
            else if (!hasColumnBreaker && Random.Range(0, 101) < 10)
            {
                GameObject columnBreaker = objectPool.GetPooledObject("ColumnBreakerPowerup");
                bricksInScene.Add(columnBreaker);
                if (columnBreaker != null)
                {
                    columnBreaker.transform.position = spawnPoint.position;
                    columnBreaker.transform.rotation = Quaternion.identity;
                    columnBreaker.SetActive(true);
                    hasColumnBreaker = true;
                }
            }
            else if (!hasSplitBall && Random.Range(0, 101) < 100)
            {
                GameObject splitBall = objectPool.GetPooledObject("SplitBallPowerup");
                bricksInScene.Add(splitBall);
                if (splitBall != null)
                {
                    splitBall.transform.position = spawnPoint.position;
                    splitBall.transform.rotation = Quaternion.identity;
                    splitBall.SetActive(true);
                    hasSplitBall = true;
                }
            }
            else if (!hasExtraBall)
            {
                GameObject ball = objectPool.GetPooledObject("ExtraBallPowerup");
                bricksInScene.Add(ball);
                if (ball != null)
                {
                    ball.transform.position = spawnPoint.position;
                    ball.transform.rotation = Quaternion.identity;
                    ball.SetActive(true);
                    hasExtraBall = true;
                }
            }
            else
            {
                int brickToCreate = Random.Range(0, 2);
                if (brickToCreate == 0)
                {
                    GameObject brick = objectPool.GetPooledObject("SquareBrick");
                    bricksInScene.Add(brick);
                    if (brick != null)
                    {
                        brick.transform.position = spawnPoint.position;
                        brick.transform.rotation = Quaternion.identity;
                        brick.SetActive(true);
                    }
                }
                else if (brickToCreate == 1)
                {
                    GameObject brick = objectPool.GetPooledObject("TriangleBrick");
                    bricksInScene.Add(brick);
                    if (brick != null)
                    {
                        brick.transform.position = spawnPoint.position;
                        brick.transform.rotation = Quaternion.identity;
                        brick.SetActive(true);
                    }
                }
            }
        }
        numberOfExtraBallsInRow = 0;
    }

    public List<Transform> GetRandomSpawnPoint(List<Transform> spawnPoints, int count)
    {
        List<Transform> randomSpawnPoints = new();
        count = Mathf.Min(count, spawnPoints.Count);
        while (randomSpawnPoints.Count < count)
        {
            int randomIndex = Random.Range(0, spawnPoints.Count);
            Transform randomSpawnPoint = spawnPoints[randomIndex];
            if (!randomSpawnPoints.Contains(randomSpawnPoint))
            {
                randomSpawnPoints.Add(randomSpawnPoint);
            }
        }
        return randomSpawnPoints;
    }
}
