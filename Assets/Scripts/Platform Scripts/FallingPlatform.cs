using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Platform_Scripts
{
    public class FallingPlatform : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private float destroyDelay = 1.4f;
        [SerializeField] private float respawnDelay = 2.4f;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private bool _isDestroyed = false;
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
            rb.bodyType = RigidbodyType2D.Dynamic;
            Destroy(gameObject, destroyDelay);
            yield return new WaitForSeconds(destroyDelay);
            _isDestroyed = true;
        }

        private IEnumerator RestorePlatform()
        {
            
            yield return new WaitForSeconds(respawnDelay);
            rb.bodyType = RigidbodyType2D.Kinematic;
            Instantiate(gameObject, _currentPos, _currentRot);
            
            _isDestroyed = false;
        }
    
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
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
        
    }
}
