using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float speed = 8f;

    [Header("Movement Stats")] [SerializeField, Range(0f, 20f)] [Tooltip("Maximum movement speed")] public float maxSpeed = 8f;
    [SerializeField, Range(0f, 100f)][Tooltip("How fast to stop when changing direction")] public float maxTurnSpeed = 80f;
    [SerializeField, Range(0f, 100f)][Tooltip("How fast to reach max speed when in mid-air")] public float maxAirAcceleration;
    [SerializeField, Range(0f, 100f)][Tooltip("How fast to stop in mid-air when no direction is used")] public float maxAirDeceleration;
    [SerializeField, Range(0f, 100f)][Tooltip("How fast to stop when changing direction when in mid-air")] public float maxAirTurnSpeed = 80f;
    
    [Range(1, 10)] 
    public float jumpVelocity;

    [Header("Calculations")] 
    public float directionX;
    private Vector2 desiredVelocity;
    public Vector2 velocity;
    private float maxSpeedChange;
    

    private float horizontal;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        directionX = context.ReadValue<float>();
    }

    // Update is called once per frame
    void Update()
    {
        desiredVelocity = new Vector2(directionX, 0f) * Mathf.Max(maxSpeed, 0f);
    }

    private void FixedUpdate()
    {


        velocity.x = desiredVelocity.x;
        rb.velocity = velocity;
    }

    // public void Jump(InputAction.CallbackContext context)
    // {
    //     if (context.performed && IsGrounded())
    //     {
    //         rb.velocity = Vector2.up * jumpVelocity;
    //     }
    //
    //     if (context.canceled && rb.velocity.y > 0f)
    //     {
    //         rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    //     }
    // }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
