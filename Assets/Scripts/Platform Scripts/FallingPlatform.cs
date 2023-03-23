using System;
using System.Collections;
using Manager_Scrips;
using Player_Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Platform_Scripts
{
    public class FallingPlatform : MonoBehaviour
    {
        [SerializeField] private float destroyDelay = 1.4f;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField]private SpawnFallingPlatform _spawnFallingPlat = null;
        private Vector3 _currentPos;
        private Quaternion _currentRot;
        
        [SerializeField, Range(0, 1f)] 
        [Tooltip("The amount of delay we want before the plat form falls")]
        public float fallDelay;

        private void Awake()
        {
            _currentPos = gameObject.transform.position;
            _currentRot = gameObject.transform.rotation;

            
        }

        public void Start()
        {
            var fallingSpawnGameObj = GameObject.FindWithTag("FallingPlatformSpawner");
            if (fallingSpawnGameObj)
            {
                _spawnFallingPlat = fallingSpawnGameObj.GetComponent<SpawnFallingPlatform>();    
            }
        }

        private IEnumerator Fall()
        {
            yield return new WaitForSeconds(fallDelay);
            rb.velocity += Physics2D.gravity;
            Destroy(gameObject, destroyDelay);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (CheckIfPlayerLandedOnPlatform(col))
            {
                StartCoroutine(Fall());
            }
        }

        private void OnDestroy()
        {
            if (_spawnFallingPlat)
            {
                _spawnFallingPlat.RestoreFallingPlatform(_currentPos, _currentRot);
            }
        }

        /// <summary>
        /// Check the collider for the player object and
        /// get the playerGroundChecker script to see if the player is grounded.
        /// If the player is grounded and the collider went off at the same time
        /// that means the player is on the platform
        /// </summary>
        /// <param name="col"></param>
        private bool CheckIfPlayerLandedOnPlatform(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player") == false)
            {
                return false;
            }
            
            var playerBody = col.gameObject.GetComponent<Rigidbody2D>();

            if (playerBody == null)
            {
                return false;
            }

            return playerBody.velocity.y == 0;
        }
    }
}
