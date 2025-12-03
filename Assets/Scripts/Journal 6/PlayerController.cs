using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //Initialize variables
    public float climbSpeed; 

    public float speed;
    public float dashSpeed;
    Vector2 dashDistance = Vector2.zero;

    public float dashDuration;
    public float dashCD;

    public TMP_Text dashCDText; 

    public float apexHeight;
    public float apexTime;

    public float gravity; 

    public float terminalVelocity;

    public float coyoteTime; 

    public FacingDirection currentFacing; 

    public Rigidbody2D rigidbody;
    public bool exploded; 

    //An enum for left and right
    public enum FacingDirection
    {
        left, right
    }

    //Attach the rigidbody to a variable
    void Start()
    {
        
        rigidbody = GetComponent<Rigidbody2D>();

    }

    //Update loop to get player input
    public void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        Vector2 playerInput = new Vector2();

        playerInput.x = Input.GetAxis("Horizontal");

        MovementUpdate(playerInput);

    }
    
    //Initialize more variables to do with movement 
    private float time;

    private bool dashing;
    private bool dashDebounce; 

    private Vector2 velocity = Vector2.zero;

    //Method to update the movement of the character
    private void MovementUpdate(Vector2 playerInput)
    {

        //Assign value to the variables.
        velocity = playerInput * speed;
        gravity = -2 * apexHeight / Mathf.Pow(apexTime, 2);

        velocity.y = gravity * Time.deltaTime;

        //Makes the character climb the wall if the character is climbing and the player is moving
        if (IsClimbing() && playerInput != Vector2.zero)
        {

            rigidbody.position = new Vector2(rigidbody.position.x, rigidbody.position.y + climbSpeed * Time.deltaTime);

        }

        //Reset the timer for coyoteTime once the player touches the ground
        if (IsGrounded())
        {

            time = 0f; 

        }

        //Add to the timer while under coyoteTime
        if (time < coyoteTime)
        {

            time += Time.deltaTime;

        }

        //If coyoteTime has not been reached, allow player to jump 
        if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || time < coyoteTime))
        {

            time = coyoteTime;
            velocity.y += 2 * apexHeight / apexTime;

        }

        //Makes player dash by pressing Q
        if (Input.GetKeyDown(KeyCode.Q) && !dashing && !dashDebounce)
        {

            //Dash left
            if (playerInput.x < 0)
            {

                dashDistance = new Vector2(-dashSpeed, 0);

            }

            //Dash right
            else if (playerInput.x > 0)
            {

                dashDistance = new Vector2(dashSpeed, 0);

            }

            //Don't dash if no input detected
            else
            {

                dashDistance = Vector2.zero;

            }

            //Dash up
            if (Input.GetKey(KeyCode.Space))
            {

                dashDistance += new Vector2(0, dashSpeed);

            }


            //Start lerping character to dash destination
            StartCoroutine(dashStart(playerInput));

        }

        //Add to linearvelocity while not dashing or exploding 
        if (!dashing && !exploded) 
        {

            rigidbody.linearVelocity = new Vector2(velocity.x, Mathf.Clamp(rigidbody.linearVelocity.y + velocity.y, -terminalVelocity, Mathf.Infinity));

        }

    }

    //Coroutine to lerp the player to dash destination
    private IEnumerator dashStart(Vector2 playerInput)
    {

        //Assign value to some variables
        float t = 0f;

        dashing = true;
        dashDebounce = true; 

        Vector2 currentPosition = rigidbody.position;

        rigidbody.linearVelocity = Vector2.zero; 

        //Starts lerping to destination
        while (t < dashDuration)
        {

            t += Time.deltaTime;
            rigidbody.MovePosition(Vector2.Lerp(currentPosition, currentPosition + dashDistance, t));

            yield return 0; 

        }

        dashing = false;

        t = 0f;

        dashCDText.enabled = true; 

        //Starts cooldown timer 
        while (t < dashCD)
        {

            dashCDText.text = "Dash CD: " + (dashCD - t).ToString("0.0");
            t += Time.deltaTime;

            yield return 0; 

        }

        //Cooldown resets
        dashDebounce = false;
        dashCDText.enabled = false;

    }

    //Checks if the character is moving left/right
    public bool IsWalking()
    {

        if (rigidbody.linearVelocityX > 0 || rigidbody.linearVelocityX < 0)
        {
            return true; 
        }

        return false;
    }

    //Checks if the character touched the ground
    public bool IsGrounded()
    {

        List<RaycastHit2D> hit = new List<RaycastHit2D>();

        //Cast a ray down from the character
        if (rigidbody.Cast(Vector2.down, hit, 0.01f) > 0)
        {

            return true;

        }        

        return false;

    }

    //Checks if the character is climbing a wall. 
    public bool IsClimbing()
    {

        List<RaycastHit2D> hit = new List<RaycastHit2D>();

        //Cast a ray left and right to check for walls
        if (rigidbody.Cast(Vector2.left, hit, 0.01f) > 0 || rigidbody.Cast(Vector2.right, hit, 0.01f) > 0)
        {

            return true;

        }

        return false;

    }

    //Checks for the character's facing direction based on its movements
    public FacingDirection GetFacingDirection()
    {

        //Face right
        if (rigidbody.linearVelocityX > 0)
        {

            currentFacing = FacingDirection.right; 

            return FacingDirection.right;

        }

        //Face left
        else if (rigidbody.linearVelocityX < 0)
        {

            currentFacing = FacingDirection.left;

            return FacingDirection.left;

        }
        else return currentFacing; 

    }
}
