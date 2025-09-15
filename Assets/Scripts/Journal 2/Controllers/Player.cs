using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public Transform plrTransform; 
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    public Vector3 bombOffset = new Vector3(0, 1, 0);
    
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {

            SpawnBombAtOffset(bombOffset);

        }

    }

    private void SpawnBombAtOffset(Vector3 offset)
    {

        Instantiate(bombPrefab, plrTransform.position + offset, Quaternion.identity);

    }

}
