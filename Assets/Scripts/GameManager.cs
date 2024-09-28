using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> spawnPoints;
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

        foreach (Transform spawnPoint in spawnPoints)
        {
            int brickToCreate = Random.Range(0, 3);

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
            else if (brickToCreate == 2 && numberOfExtraBallsInRow == 0)
            {
                bricksInScene.Add(
                    Instantiate(extraBallPowerup, spawnPoint.position, Quaternion.identity)
                );

                numberOfExtraBallsInRow++;
            }
        }

        numberOfExtraBallsInRow = 0;
    }

    public void PlaceBricks()
    {
        level++;
        foreach (Transform spawnPoint in spawnPoints)
        {
            int brickToCreate = Random.Range(0, 3);

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
}
