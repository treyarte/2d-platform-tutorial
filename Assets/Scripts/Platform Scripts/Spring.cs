using UnityEngine;

namespace Platform_Scripts
{
    public class Spring : MonoBehaviour
    {
        [Range(20, 70)] [SerializeField] private float springBounce = 50f;
        [SerializeField] private Animator springAnimator;
        private static readonly int PlayerCollideTrigger = Animator.StringToHash("playerCollideTrigger");

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                var player = col.gameObject;

                var playerRigidBody = player.GetComponent<Rigidbody2D>();

                if (playerRigidBody)
                {
                    Vector2 velocity = playerRigidBody.velocity;

                    velocity = new Vector2(velocity.x, velocity.y + springBounce);

                    playerRigidBody.velocity = velocity;
                    
                    springAnimator.SetTrigger(PlayerCollideTrigger);
                    
                }
            }
        }
    }
}
