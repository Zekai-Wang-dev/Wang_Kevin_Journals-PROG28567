using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using random = UnityEngine.Random; 

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;

    public Transform asteroidTransform; 

    public Vector3 randomDirection; 
    public Vector3 randomPoint; 

    public Vector3 velocity; 
    public float distance; 

    // Start is called before the first frame update
    void Start()
    {

        velocity = Vector3.zero; 
        randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
        randomPoint = randomDirection * maxFloatDistance; 
        distance = (randomPoint - asteroidTransform.position).magnitude; 

    }

    // Update is called once per frame
    void Update()
    {

        AsteroidMovement();

    }

    public void AsteroidMovement(){

        float currentDistance = (randomPoint - asteroidTransform.position).magnitude; 

        velocity = randomDirection * moveSpeed; 

        print(currentDistance);

        if (currentDistance > distance - arrivalDistance) {

            asteroidTransform.position += velocity * Time.deltaTime; 

        }
        else {

            randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
            randomPoint = randomDirection * maxFloatDistance; 
            distance = (randomPoint - asteroidTransform.position).magnitude; 

        }

        

    }

}
