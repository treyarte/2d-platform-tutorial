using UnityEngine;

namespace Player_Scripts
{
    public class PlayerFallDeath : MonoBehaviour
    {
        private GameObject _player;

        /// <summary>
        /// The height when falling when the player is considered dead
        /// </summary>
        [SerializeField] public float fallHeight = -7.8f;

        /// <summary>
        /// The height in the air where the player has gone to far up
        /// and is considered dead.
        /// </summary>
        // [SerializeField] public float skyHeight;

        // Update is called once per frame
        void Update()
        {
            if (this.transform.localPosition.y <= fallHeight)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
