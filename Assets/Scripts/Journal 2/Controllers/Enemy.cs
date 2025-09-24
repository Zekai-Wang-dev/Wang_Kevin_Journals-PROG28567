using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public Transform plrTransform;
    public Transform enemyTransform;

    public Vector3 velocity;
    public float accelerationTime;
    public float decelerationTime;
    public float speed;
    public float maxSpeed;

    public bool accel;

    private void Start()
    {

        accelerationTime = 1f;
        decelerationTime = 1f;
        maxSpeed = 10f; 
        speed = 5f;
        accel = false;

    }

    private void Update()
    {

        EnemyMovement();
        enemyTransform.position += velocity * Time.deltaTime;

    }

    public void EnemyMovement()
    {

        //The movement for the enemy would be the same as the player, except its direction is based on the plrTransform's position. 

        float accelerationRate = maxSpeed / accelerationTime;
        float decelerationRate = maxSpeed / decelerationTime;

        if (plrTransform.position.x < enemyTransform.position.x)
        {
            velocity += Vector3.left * accelerationRate * Time.deltaTime;

        }
        if (plrTransform.position.x > enemyTransform.position.x)
        {
            velocity += Vector3.right * accelerationRate * Time.deltaTime;

        }
        if (plrTransform.position.y > enemyTransform.position.y)
        {
            velocity += Vector3.up * accelerationRate * Time.deltaTime;

        }
        if (plrTransform.position.y < enemyTransform.position.y)
        {
            velocity += Vector3.down * accelerationRate * Time.deltaTime;

        }

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

    }

}
