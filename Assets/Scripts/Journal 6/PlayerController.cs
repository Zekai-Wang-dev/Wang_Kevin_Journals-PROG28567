using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpForce;

    public FacingDirection currentFacing; 

    public Rigidbody2D rigidbody; 

    public enum FacingDirection
    {
        left, right
    }

    void Start()
    {
        
        rigidbody = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        Vector2 playerInput = new Vector2();

        playerInput.x = Input.GetAxis("Horizontal");

        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {

        Vector2 velocity = playerInput * speed;

        if (Input.GetKeyDown(KeyCode.Space) & IsGrounded())
        {

            velocity.y = jumpForce;

        }

        rigidbody.linearVelocity = new Vector2(velocity.x, rigidbody.linearVelocityY += velocity.y); 

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

        if (rigidbody.linearVelocityY > -0.01 && rigidbody.linearVelocityY < 0.01)
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
