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
    }

    public void EnemyMovement()
    {

        //The movement for the enemy would be the same as the player, except its direction is based on the plrTransform's position. 

    }

}
