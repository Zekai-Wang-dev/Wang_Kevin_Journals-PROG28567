using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

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

    public enum FacingDirection
    {
        left, right
    }

    void Start()
    {
        
        rigidbody = GetComponent<Rigidbody2D>();

    }

    public void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        Vector2 playerInput = new Vector2();

        playerInput.x = Input.GetAxis("Horizontal");

        MovementUpdate(playerInput);

    }
    private float time;

    private bool dashing;
    private bool dashDebounce; 

    private Vector2 velocity = Vector2.zero;

    private void MovementUpdate(Vector2 playerInput)
    {

        velocity = playerInput * speed;
        gravity = -2 * apexHeight / Mathf.Pow(apexTime, 2);

        velocity.y = gravity * Time.deltaTime;

        if (IsClimbing() && playerInput != Vector2.zero)
        {

            rigidbody.position = new Vector2(rigidbody.position.x, rigidbody.position.y + climbSpeed * Time.deltaTime);

        }

        if (IsGrounded())
        {

            time = 0f; 

        }


        if (time < coyoteTime)
        {

            time += Time.deltaTime;

        }


        if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || time < coyoteTime))
        {

            time = coyoteTime;
            velocity.y += 2 * apexHeight / apexTime;

        }

        if (Input.GetKeyDown(KeyCode.Q) && !dashing && !dashDebounce)
        {

            if (playerInput.x < 0)
            {

                dashDistance = new Vector2(-dashSpeed, 0);

            }

            else if (playerInput.x > 0)
            {

                dashDistance = new Vector2(dashSpeed, 0);

            }

            else
            {

                dashDistance = Vector2.zero;

            }

            if (Input.GetKey(KeyCode.Space))
            {

                dashDistance += new Vector2(0, dashSpeed);

            }

            StartCoroutine(dashStart(playerInput));

        }

        if (!dashing && !exploded) 
        {

            rigidbody.linearVelocity = new Vector2(velocity.x, Mathf.Clamp(rigidbody.linearVelocity.y + velocity.y, -terminalVelocity, Mathf.Infinity));

        }

    }

    private IEnumerator dashStart(Vector2 playerInput)
    {

        float t = 0f;

        dashing = true;
        dashDebounce = true; 

        Vector2 currentPosition = rigidbody.position;

        rigidbody.linearVelocity = Vector2.zero; 

        while (t < dashDuration)
        {

            t += Time.deltaTime;
            rigidbody.MovePosition(Vector2.Lerp(currentPosition, currentPosition + dashDistance, t));

            yield return 0; 

        }

        dashing = false;

        t = 0f;

        dashCDText.enabled = true; 

        while (t < dashCD)
        {

            dashCDText.text = "Dash CD: " + (dashCD - t).ToString("0.0");
            t += Time.deltaTime;

            yield return 0; 

        }

        dashDebounce = false;
        dashCDText.enabled = false;

    }

    public bool IsWalking()
    {

        if (rigidbody.linearVelocityX > 0 || rigidbody.linearVelocityX < 0)
        {
            return true; 
        }

        return false;
    }

    public bool IsGrounded()
    {

        List<RaycastHit2D> hit = new List<RaycastHit2D>();

        if (rigidbody.Cast(Vector2.down, hit, 0.01f) > 0)
        {

            return true;

        }        

        return false;

    }

    public bool IsClimbing()
    {

        List<RaycastHit2D> hit = new List<RaycastHit2D>();

        if (rigidbody.Cast(Vector2.left, hit, 0.01f) > 0 || rigidbody.Cast(Vector2.right, hit, 0.01f) > 0)
        {

            return true;

        }

        return false;

    }

    public FacingDirection GetFacingDirection()
    {

        if (rigidbody.linearVelocityX > 0)
        {

            currentFacing = FacingDirection.right; 

            return FacingDirection.right;

        }
        else if (rigidbody.linearVelocityX < 0)
        {

            currentFacing = FacingDirection.left;

            return FacingDirection.left;

        }
        else return currentFacing; 

    }
}
