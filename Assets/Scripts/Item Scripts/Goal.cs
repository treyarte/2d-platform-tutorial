using System;
using UnityEngine;

namespace Item_Scripts
{
    public class Goal : MonoBehaviour
    {
        public static event Action CompleteLevel;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                Debug.Log("I Ran");
                CompleteLevel?.Invoke();        
            }
        }
    }
}