using System.Collections;
using UnityEngine;

namespace Platform_Scripts
{
    public class FallingPlatform : MonoBehaviour
    {
        private Collider2D _collider2D;
        private float _destroyDelay = 1.4f;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField, Range(0, 1f)]
        [Tooltip("The amount of delay we want before the plat form falls")]
        public float fallDelay;

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        private IEnumerator Fall()
        {
            yield return new WaitForSeconds(fallDelay);
            rb.bodyType = RigidbodyType2D.Dynamic;
            Destroy(gameObject, _destroyDelay);
        }
    
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Fall());
            }
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
