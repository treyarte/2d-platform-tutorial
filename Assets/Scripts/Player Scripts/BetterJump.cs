using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BetterJump : MonoBehaviour
{
    private bool _isJumpPressed = false;
    [SerializeField]private float gravity;
    [SerializeField]private float initialJumpVelocity;
    [SerializeField]private float maxJumpHeight = 1.0f;
    [SerializeField]private float maxJumpTime = .5f;
    [SerializeField]private bool isJumping = false;

    private void Awake()
    {
        var timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        Debug.Log($"Am I jumping? {_isJumpPressed}");
    }
}
