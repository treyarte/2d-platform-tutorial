using UnityEngine;
using UnityEngine.InputSystem;

namespace Player_Scripts
{
    public class PlayerJump : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]private Rigidbody2D rb;
        [SerializeField]private Vector2 _velocity;
        private PlayerGroundChecker _groundChecker;

        [Header("Jumping Attributes")] 
        [SerializeField, Range(2f, 5.5f)] [Tooltip("Max jump height")]
        public float jumpHeight = 8.1f;
    
        [SerializeField, Range(0.2f, 1.25f)] [Tooltip("The amount of time it takes the player to reach the peak of the jump")]
        public float timeToJumpPeak;
    
        [SerializeField, Range(0f, 5f)] [Tooltip("Gravity when accelerating up in the air")]
        public float gravityUpForce = 1f;
    
        [SerializeField, Range(1f, 10f)] 
        [Tooltip("Gravity applied when falling down from jump")] 
        public float gravityDownForce = 6.17f;
    
        [SerializeField, Range(0, 1)] 
        [Tooltip("The total amount of times a player can jump in air")] 
        public int totalAirJumps;

        [Header("Options")] 
        [SerializeField] [Tooltip("Allow the player to hold the jump button to change jump height")]
        public bool variableJumpHeight = true;

        [SerializeField, Range(1f, 10f)] [Tooltip("Gravity force when the players let go of the jump btn")]
        public float jumpCutOff;

        [SerializeField] [Tooltip("How fast the player can fall")]
        public float fallSpeedLimit;

        [SerializeField, Range(0f, 0.3f)]
        [Tooltip("How long the player have to be able to jump off a platform the just walked off on")]
        public float coyoteTime = 0.15f;

        [SerializeField, Range(0f, 0.3f)] [Tooltip("How far from the ground a jump is cached")]
        public float jumpBuffer = 0.15f;

        [Header("calculations")] 
        public float jumpSpeed;
        public float gravityMultiplier;
        private float _defaultGravityScale;

        [Header("Current State")] 
        public bool canJumpAgain = false;
        public bool isGrounded;
        private bool _desiredJump;
        private float _jumpBufferCounter;
        private float _coyoteTimeCounter = 0;
        private bool _pressingJump;
        private bool _isJumping;

        private bool _hasJumped;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            _groundChecker = GetComponent<PlayerGroundChecker>();
            _defaultGravityScale = 1f;
        }



        // Update is called once per frame
        void Update()
        {
            SetGravityOnPlayer();

            isGrounded = _groundChecker.GetIsGrounded();

            if (jumpBuffer > 0)
            {
                if (_desiredJump)
                {
                    _jumpBufferCounter += Time.deltaTime;
                    if (_jumpBufferCounter > jumpBuffer)
                    {
                        _desiredJump = false;
                        _jumpBufferCounter = 0;
                    }
                
                }            
            }
        
            //If we are not on the ground and not jump then we have stepped off an ledge
            if (!_isJumping && !isGrounded)
            {
                _coyoteTimeCounter += Time.deltaTime;
            }
            else
            {
                //If we touched the ground or jumped reset the timer
                _coyoteTimeCounter = 0;
            }
        }

        private void FixedUpdate()
        {
            _velocity = rb.velocity;
        
            if (_desiredJump)
            {
                PerformJump();
                rb.velocity = _velocity;
            
                //Skip gravity calculations this frame, so currentlyJumping doesn't turn off
                //This makes sure you can't do the coyote time double jump bug
                return;
            }

            CalculateGravity();
        }

        private void SetGravityOnPlayer()
        {
            Vector2 newGravity = new Vector2(0, (-2 * jumpHeight) / (timeToJumpPeak * timeToJumpPeak));
            rb.gravityScale = (newGravity.y / Physics2D.gravity.y) * gravityMultiplier;
        }

        /// <summary>
        /// Changes the character's gravity based on y direction
        /// </summary>
        private void CalculateGravity()
        {
            //If we are going up
            if (rb.velocity.y > 0.01f)
            {
                if (isGrounded)
                {
                    //If we are standing on something like a moving platform
                    gravityMultiplier = _defaultGravityScale;
                }
                else
                {
                    if (variableJumpHeight)
                    {
                        //Applying upward direction if player is rising and holding jump
                        if (_pressingJump && _isJumping)
                        {
                            gravityMultiplier = gravityUpForce;
                        }
                        //Player let go
                        else
                        {
                            gravityMultiplier = jumpCutOff;
                        }
                    }
                    //Normal jump
                    else
                    {
                        gravityMultiplier = gravityUpForce;
                    }
                }
            }
        
            //If going down
            else if (rb.velocity.y < -0.01f)
            {
                if (isGrounded)
                {
                    //If we are standing on something like a moving platform
                    gravityMultiplier = _defaultGravityScale;
                }
                else
                {
                    gravityMultiplier = gravityDownForce;
                }
            }

            //if not jumping or falling set everything back to default
            else
            {
                if (isGrounded)
                {
                    _isJumping = false;
                }

                gravityMultiplier = _defaultGravityScale;
            }

            //Set the character's Rigidbody's velocity idk what this means
            //But clamp the Y variable within the bounds of the speed limit, for the terminal velocity assist option
            rb.velocity = new Vector3(_velocity.x, Mathf.Clamp(_velocity.y, -fallSpeedLimit, 100));
        }

        private void PerformJump()
        {
            //Create the jump, provided we are on the ground, in coyote time, or have a double jump available
            if (isGrounded || (_coyoteTimeCounter > 0.03f && _coyoteTimeCounter < coyoteTime) || canJumpAgain)
            {
                _desiredJump = false;
                _jumpBufferCounter = 0;
                _coyoteTimeCounter = 0;
            
                //allow only one double jump
                canJumpAgain = (totalAirJumps == 1 && canJumpAgain == false);
            
                //Determine jump power
                jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * rb.gravityScale * jumpHeight);
            
                //If the player is moving up or down when she jumps (such as when doing a double jump), change the jumpSpeed;
                //This will ensure the jump is the exact same strength, no matter your velocity.
                if (_velocity.y > 0f)
                {
                    jumpSpeed = Mathf.Max(jumpSpeed - _velocity.y, 0f);
                } else if (_velocity.y < 0f)
                {
                    jumpSpeed += Mathf.Abs(rb.velocity.y);
                }
            
                //Apply the new jumpSpeed to the velocity. It will be sent to the Rigidbody in FixedUpdate;
                _velocity.y += jumpSpeed;
                _isJumping = true;
            }

            if (jumpBuffer == 0)
            {
                //If we don't have a jump buffer, then turn off desiredJump immediately after hitting jumping
                _desiredJump = false;
            }
        }
        public void OnJump(InputAction.CallbackContext context) {
            //This function is called when one of the jump buttons (like space or the A button) is pressed.
        
            //When we press the jump button, tell the script that we desire a jump.
            //Also, use the started and canceled contexts to know if we're currently holding the button
            if (context.started) {
                _desiredJump = true;
                _pressingJump = true;
            }

            if (context.canceled) {
                _pressingJump = false;
            }
        }
    }
}
