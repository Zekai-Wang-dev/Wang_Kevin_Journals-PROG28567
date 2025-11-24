using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed;

    public float apexHeight;
    public float apexTime;

    public float gravity; 

    public float terminalVelocity;

    public float coyoteTime; 

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

    private void MovementUpdate(Vector2 playerInput)
    {

        Vector2 velocity = playerInput * speed;
        gravity = -2 * apexHeight / Mathf.Pow(apexTime, 2);

        velocity.y = gravity * Time.deltaTime;

        if (!IsGrounded())
        {
            
            time += Time.deltaTime; 
        }
        else
        {
            time = 0f;

        }

        if (Input.GetKeyDown(KeyCode.Space) & (IsGrounded() || time < coyoteTime))
        {

            time = coyoteTime;
            velocity.y += 2 * apexHeight / apexTime; 

        }
   
        rigidbody.linearVelocity = new Vector2(velocity.x, Mathf.Clamp(rigidbody.linearVelocity.y + velocity.y, -terminalVelocity, Mathf.Infinity)); 
    
        if (rigidbody.linearVelocity.y < 0)
        {

            rigidbody.linearVelocity = new Vector2(velocity.x, Mathf.Clamp(rigidbody.linearVelocity.y + velocity.y, -terminalVelocity, -0.2f)); 

        }

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

        if (rigidbody.linearVelocityY > -0.01f && rigidbody.linearVelocityY < 0.01f)
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
