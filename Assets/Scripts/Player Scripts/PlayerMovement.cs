using UnityEngine;
using UnityEngine.InputSystem;

namespace Player_Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public Rigidbody2D rb;
        private PlayerGroundChecker _playerGroundChecker;

        [Header("Movement Stats")] [SerializeField, Range(0f, 20f)] 
        [Tooltip("Maximum movement speed")] 
        public float maxSpeed = 8f;
    
        [SerializeField, Range(0f, 100f)]
        [Tooltip("How fast to stop when changing direction")] 
        public float maxTurnSpeed = 80f;

        [SerializeField, Range(0f, 100f)]
        [Tooltip("How fast to reach max speed when in mid-air")] 
        public float maxAirAcceleration;
    
        [SerializeField, Range(0f, 100f)]
        [Tooltip("How fast to stop in mid-air when no direction is used")] 
        public float maxAirDeceleration;
    
        [SerializeField, Range(0f, 100f)]
        [Tooltip("How fast to stop when changing direction when in mid-air")] 
        public float maxAirTurnSpeed = 80f;
    

        [Header("Calculations")] 
        public float directionX;
        private Vector2 _desiredVelocity;
        public Vector2 velocity;
        private float _maxSpeedChange;

        private bool _isGrounded;
   
    
    
        private void Awake()
        {
            _playerGroundChecker = GetComponent<PlayerGroundChecker>();
            rb = GetComponent<Rigidbody2D>();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            directionX = context.ReadValue<float>();
        }

        // Update is called once per frame
        void Update()
        {
            _desiredVelocity = new Vector2(directionX, 0f) * Mathf.Max(maxSpeed, 0f);
        }

        private void FixedUpdate()
        {
            _isGrounded = _playerGroundChecker.GetIsGrounded();
            velocity = rb.velocity;
        
            velocity.x = _desiredVelocity.x;
            rb.velocity = velocity;
        }
    
    }
}
