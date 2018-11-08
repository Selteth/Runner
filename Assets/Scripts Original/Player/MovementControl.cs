using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    [Header("Horizontal movement")]
    public float moveForce = 365f;          // Amount of force added to move the player left and right
    public float maxSpeed = 5f;				// The fastest the player can travel in the x axis

    /* Components */
    private new Rigidbody2D rigidbody;      // Used new to disable warning about hiding base member

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        
        MoveHorizontal(horizontalAxis);
        
    }

    // Handles horizontal movement
    private void MoveHorizontal(float horizontalAxis)
    {
        /* Accelerate the speed */
        if (horizontalAxis * rigidbody.velocity.x < maxSpeed)
            rigidbody.AddForce(Vector2.right * moveForce * horizontalAxis);

        /* Limit player maximum speed */
        if (Mathf.Abs(rigidbody.velocity.x) > maxSpeed)
            rigidbody.velocity = new Vector2(Mathf.Sign(rigidbody.velocity.x) * maxSpeed, rigidbody.velocity.y);
    }

}
