using System;
using System.Collections.Generic;
using Enums;
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
        [SerializeField] private MovingPlatformEnum type; 

        private int i;

        public void Awake()
        {
            if (type == MovingPlatformEnum.UpAndDown)
            {
                transform.position =
                    new Vector3(ListOfPoints[0].position.x, transform.position.y, transform.position.z);
            }
        }

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

            var pointPosition = ListOfPoints[i].position;

            transform.position = Vector2.MoveTowards(transform.position, pointPosition,
                Speed * Time.deltaTime);
        }

        /// <summary>
        /// When we a player collide with the moving platform
        /// we set the platform (what is rendered) as the player parent.
        /// </summary>
        /// <param name="col"></param>
        private void OnCollisionEnter2D(Collision2D col)
        {
            //Check if what collide has the playertag
             if (col.gameObject.CompareTag("Player"))
            {
                if (CheckIfPlayerIsOnPlatform(col.gameObject))
                {
                    var playerBody = col.gameObject.GetComponent<Rigidbody2D>();
                    if (playerBody)
                    {
                        col.transform.SetParent(transform);
                        // To stop the jitteriness we have tp set the player's rb interpolation to none
                        col.rigidbody.interpolation = RigidbodyInterpolation2D.None;
                    }
                }
            }
        }

        /// <summary>
        /// When the player leaves the platform set the player back
        /// to how it was before
        /// </summary>
        /// <param name="col"></param>
        private void OnCollisionExit2D(Collision2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;
            
            col.transform.SetParent(null);
            col.rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        /// <summary>
        /// Check if the player is standing on the platform
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private bool CheckIfPlayerIsOnPlatform(GameObject player)
        {

            //This is for low to the ground platforms
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
            
            return playerBody.velocity.y <= 0;

        }
    }
}