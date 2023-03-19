using UnityEngine;

namespace Player_Scripts
{
    public class PlayerGroundChecker : MonoBehaviour
    {
        [Header("Colliders")]
        [SerializeField]
        [Tooltip("The length of the raycast that will check if the player is grounded")]
        public float groundLength = 0.62f;

        /// <summary>
        /// Without the offset the colliders will be in the center of the
        /// character which will mess up jumping when part of the character is on an edge
        /// </summary>
        [SerializeField] [Tooltip("The offset pushes the colliders to the edge of the character")]
        public Vector3 colliderOffSet;

        [Header("Layer Mask")] [Tooltip("Layer the player is able to jump on")] [SerializeField]
        public LayerMask groundLayer;

        private bool _isGrounded;

        // Update is called once per frame
        void Update()
        {
            var position = transform.position;
            _isGrounded = Physics2D.Raycast(position + colliderOffSet, Vector2.down, groundLength, groundLayer) ||
                          Physics2D.Raycast(position - colliderOffSet, Vector2.down, groundLength, groundLayer);
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

            var position = transform.position;

            Gizmos.DrawLine(position + colliderOffSet, position + colliderOffSet + Vector3.down * groundLength);
            Gizmos.DrawLine(position - colliderOffSet, position - colliderOffSet + Vector3.down * groundLength);
        }

        public bool GetIsGrounded()
        {
            return _isGrounded;
        }
    }
}
