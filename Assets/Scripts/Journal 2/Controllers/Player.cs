using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public Transform plrTransform; 
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    public Vector3 bombOffset = new Vector3(0, 1, 0);

    public float bombTrailSpacing = 1f;
    public int numberOfTrailBombs = 5; 

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {

            SpawnBombAtOffset(bombOffset);

        }

        if (Input.GetKeyDown(KeyCode.T))
        {

            SpawnTrailBomb(bombTrailSpacing, numberOfTrailBombs);

        }

    }

    private void SpawnBombAtOffset(Vector3 offset)
    {

        Instantiate(bombPrefab, plrTransform.position + offset, Quaternion.identity);

    }

    private void SpawnTrailBomb(float bombSpacing, float numberOfBombs)
    {

        for (int i = 0; i < numberOfBombs; i++)
        {

            SpawnBombAtOffset(new Vector3(0, -bombTrailSpacing * i - bombTrailSpacing, 0));

        }

    }

}
