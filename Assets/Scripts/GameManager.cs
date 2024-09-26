using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public GameObject triangleBrick;
    public GameObject squareBrick;
    public int numberOfBricksToStart;
    public int level;

    private void Start()
    {
        level = 1;

        foreach (Transform spawnPoint in spawnPoints)
        {
            int brickToCreate = Random.Range(0, 3);

            if (brickToCreate == 0)
            {
                Instantiate(squareBrick, spawnPoint.position, Quaternion.identity);
            }
            else if (brickToCreate == 1)
            {
                Instantiate(triangleBrick, spawnPoint.position, Quaternion.identity);
            }
        }
    }
}
