using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerDeath : MonoBehaviour
    {
        public static event Action PlayerDied;

        private void OnDisable()
        {
            if (!this.gameObject.scene.isLoaded)
            {
                return;
            }
            PlayerDied?.Invoke();
        }
    }
}
