using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFlip : MonoBehaviour
{
    /// <summary>
    /// Player horizontal input
    /// </summary>
    private float _horizontal;

    private bool _isFacingRight;

    /// <summary>
    /// Player sprite
    /// </summary>
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _isFacingRight = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isFacingRight && _horizontal > 0f)
        {
            Flip();
            _spriteRenderer.flipX = false;
        }
        else if(_isFacingRight && _horizontal < 0f)
        {
            Flip();
            _spriteRenderer.flipX = true;
        }
    }

    public void GetHorizontal(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<float>();
    }
    
    public void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
