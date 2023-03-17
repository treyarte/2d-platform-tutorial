using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    [Header("Colliders")] 
    [SerializeField][Tooltip("The length of the raycast that will check if the player is grounded")]
    public float groundLength = 0.62f;
    // [SerializeField][Tooltip("")] public Vector3 colliderOffSet;
    
    [Header("Layer Mask")]
    [Tooltip("Layer the player is able to jump on")]
    [SerializeField]public LayerMask groundLayer;

    private bool _isGrounded;
    
    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundLength, groundLayer);
    }

    private void OnDrawGizmos()
    {
        if (_isGrounded)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
        // Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
    }

    public bool GetIsGrounded()
    {
        return _isGrounded;
    }
}
