using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public Transform plrTransform; 
    public GameObject bombPrefab;
    public Transform bombsTransform;

    public Vector3 bombOffset = new Vector3(0, 1, 0);

    public float bombTrailSpacing = 1f;
    public int numberOfTrailBombs = 5;

    public Vector3 velocity;
    public float accelerationTime; 
    public float speed;
    public float maxSpeed; 

    private void Start()
    {

        accelerationTime = 1f; 
        speed = 5f; 

    }

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

        if (Input.GetKeyDown(KeyCode.V))
        {

            SpawnBombOnRandomCorner(3f);

        }

        if (Input.GetKeyDown(KeyCode.W))
        {

            WarpPlayer(enemyTransform, 1.4f);

        }

        if (Input.GetKey(KeyCode.S))
        {

            DetectAsteroids(10f, asteroidTransforms);

        }

        PlayerMovement();

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

    public void SpawnBombOnRandomCorner(float inDistance)
    {

        int randomNumber = Random.Range(0, 4);
        float halfInDistance = inDistance / 2; 

        Vector3 topLeft = new Vector3(-halfInDistance, halfInDistance, 0);
        Vector3 topRight = topLeft + new Vector3(inDistance, 0, 0);
        Vector3 bottomRight = topRight + new Vector3(0, -inDistance, 0);
        Vector3 bottomLeft = bottomRight + new Vector3(-inDistance, 0, 0); 

        if (randomNumber == 0)
        {
            Instantiate(bombPrefab, plrTransform.position + topLeft, Quaternion.identity);


        }

        if (randomNumber == 1)
        {
            Instantiate(bombPrefab, plrTransform.position + topRight, Quaternion.identity);


        }

        if (randomNumber == 2)
        {
            Instantiate(bombPrefab, plrTransform.position + bottomLeft, Quaternion.identity);


        }

        if (randomNumber == 3)
        {
            Instantiate(bombPrefab, plrTransform.position + bottomRight, Quaternion.identity);


        }

    }

    public void WarpPlayer(Transform target, float ratio)
    {

        if (ratio > 1)
        {

            ratio = 1f; 

        }

        plrTransform.position = Vector3.Lerp(plrTransform.position, target.position, ratio);

    }

    public void DetectAsteroids(float inMaxRange, List<Transform> inAsteroids)
    {

        List<Vector3> normals = new List<Vector3>();

        for (int i = 0; i < inAsteroids.Count; i++)
        {

            float magnitude = (plrTransform.position - inAsteroids[i].position).magnitude;
            Vector3 normal = (inAsteroids[i].position  - plrTransform.position).normalized;

            if (Mathf.Abs(magnitude) < inMaxRange)
            {

                normals.Add(normal);

            }

        }

        for (int i = 0; i < normals.Count; i++)
        {

            Debug.DrawLine(plrTransform.position, plrTransform.position + normals[i] * 2.5f);

        }


    }

    public void PlayerMovement()
    {

        float accelerationRate = maxSpeed / accelerationTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {

            velocity += Vector3.left * accelerationRate * Time.deltaTime; 

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {

            velocity += Vector3.right * accelerationRate * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {

            velocity += Vector3.up * accelerationRate * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {

            velocity += Vector3.down * accelerationRate * Time.deltaTime;

        }

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        plrTransform.position += velocity * Time.deltaTime; 

    }

}