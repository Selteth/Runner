using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    [Header("Horizontal movement")]
    // Amount of force added to move the player left and right.
    public float moveForce = 100f;
    // The fastest the player can travel in the x axis.
    public float maxSpeed = 5f;
    // Horizontal movement axis
    private float horizontalAxis;

    [Header("Vertical movement")]
    // Amount of force added to make the player jump
    public float jumpForce = 500f;
    // Position on the player sprite that defines if he is on the ground
    ///private Transform groundCheck;
    
    // Instead of creating special GameObject on those sides, we need, we should better use a vector math. 
    // It is becouse our player object will rotate in the jump.
    private float downVectorLength = 0f;
    public float downVectorCoef = 0.55f;
    // Whether or not the player is on the ground
    private bool isGrounded = false;
    // Whether the player should jump
    private bool shouldJump = false;

    [Header("Rotate speed")]
    //
    public float angleRotatingImpulse = 10f;
    // The vector around which the player rotates
    //private Vector3 rotateCenter = new Vector3(0, 0, 1);
    private bool isRotating = false;
    
    // Used new to disable warning about hiding base member
    private new Rigidbody2D rigidbody;

    

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        // groundCheck = transform.Find("GroundCheck");
        downVectorLength = GetComponent<BoxCollider2D>().size.x * gameObject.transform.localScale.x * downVectorCoef;
    }
    
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        shouldJump = Input.GetButton("Jump");
        isGrounded = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y-downVectorLength), 1 << LayerMask.NameToLayer("Ground"));
        if (isGrounded)
            isRotating = false;
    }

    void FixedUpdate()
    {
        MoveHorizontal();
        MoveVertical();
        Rotate();
    }

    // Handles horizontal movement
    private void MoveHorizontal()
    {
        if (horizontalAxis * rigidbody.velocity.x < maxSpeed)
            rigidbody.AddForce(Vector2.right * horizontalAxis * moveForce);

        if (Mathf.Abs(rigidbody.velocity.x) > maxSpeed)
            rigidbody.velocity = new Vector2(Mathf.Sign(rigidbody.velocity.x) * maxSpeed, rigidbody.velocity.y);
    }

    // Handles vertical movement (jumping)
    private void MoveVertical()
    {
        if (shouldJump && isGrounded)
        {
            rigidbody.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
            
        }
    }
    private void Rotate()
    {
        if (!isGrounded && !isRotating)
        {
            rigidbody.AddForceAtPosition(
                new Vector2(transform.position.x+1, transform.position.y), 
                Vector2.left*angleRotatingImpulse*(horizontalAxis>0?(horizontalAxis<0?0:1):-1)
                );
            isRotating = true;
        }
    }
}
