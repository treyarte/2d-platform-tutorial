using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Platform_Scripts
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private float Speed;
        [SerializeField] private int StartingPoint;
        [SerializeField] private List<Transform> ListOfPoints;
        [SerializeField] private float cutOffDistance = 0.95f;

        private int i;

        public void Start()
        {
            transform.position = ListOfPoints[StartingPoint].position;
        }

        public void Update()
        {
            if ((Vector2.Distance(transform.position, ListOfPoints[i].position)) < cutOffDistance)
            {
                i++;

                if (i == ListOfPoints.Count)
                {
                    i = 0;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, ListOfPoints[i].position,
                Speed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                if (CheckIfPlayerIsOnPlatform(col.gameObject))
                {
                    var playerBody = col.gameObject.GetComponent<Rigidbody2D>();
                    if (playerBody)
                    {
                        col.transform.SetParent(transform);
                        // playerBody.velocity =
                        //     Vector2.MoveTowards(col.gameObject.transform.position, transform.position, Speed);
                    }
                }
            }
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.transform.SetParent(null);
            }
        }

        private bool CheckIfPlayerIsOnPlatform(GameObject player)
        {

            // if (transform.position.y > player.transform.position.y )
            // {
            //     return false;
            // }
            //
            var playerBody = player.GetComponent<Rigidbody2D>();
            
            if (!playerBody)
            {
                return false;
            }
            
            return playerBody.velocity.y == 0;

        }
    }
}