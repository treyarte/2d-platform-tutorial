using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    [Range(1, 10)] 
    public float jumpVelocity;    
    public Transform groundCheck;
    public LayerMask groundLayer;
    
    private Rigidbody2D rb;
    
    private bool _hasJumped;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (_hasJumped && IsGrounded())
        {
            rb.velocity =  Vector2.up * jumpVelocity;
        }
        
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !_hasJumped)
        {
            // rb.velocity = Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        _hasJumped = context.performed;
    }
}
