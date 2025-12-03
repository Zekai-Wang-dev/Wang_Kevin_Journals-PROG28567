using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    //Initialize variables
    public bool exploded;
    public float explodeRadius;

    public float explodePower; 
    public PlayerController playerController;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Checks collision to activate tnt 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!exploded && collision.collider.name == "Player")
        {

            explode();

        }

    }

    //Method to explode the tnt and fling players
    public void explode()
    {

        //Initialize variables
        List<RaycastHit2D> hit = new List<RaycastHit2D>();
        GameObject player = null;
        exploded = true;
        GetComponent<SpriteRenderer>().color = Color.red; 

        //Casts a ray to the right of the tnt and checks for the player 
        if (GetComponent<Rigidbody2D>().Cast(Vector2.right, hit, explodeRadius) > 0)
        {
            for (int i = 0; i < hit.Count; i++)
            {

                if (hit[i].collider.name == "Player")
                {
                    player = GameObject.Find(hit[i].collider.name);

                }
            }

        }

        //Casts a ray to the left of the tnt and checks for the player 
        if (GetComponent<Rigidbody2D>().Cast(Vector2.left, hit, explodeRadius) > 0)
        {
            for (int i = 0; i < hit.Count; i++)
            {

                if (hit[i].collider.name == "Player")
                {
                    player = GameObject.Find(hit[i].collider.name);

                }

            }

        }

        //If player is found fling the player. 
        if (player != null)
        {

            Rigidbody2D plrRigidbody = player.GetComponent<Rigidbody2D>();
            Vector2 direction = (player.transform.position - transform.position).normalized;

            plrRigidbody.linearVelocity += direction * explodePower; 
            playerController.exploded = true;

        }

        //Start the coroutine to destroy the tnt
        StartCoroutine(selfDestruct());

    }

    //Destroys the tnt after a set time has passed. 
    public IEnumerator selfDestruct()
    {

        float t = 0; 

        while (t < 0.5)
        {

            t += Time.deltaTime;
            yield return 0; 

        }
        
        playerController.exploded = false;
        Destroy(gameObject);

    }

}
