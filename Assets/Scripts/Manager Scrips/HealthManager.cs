using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Manager_Scrips
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] public float MaxHealth = 3f;
        [SerializeField] public float Health = 3f;

        private void OnEnable()
        {
            DamagePlayer.DoDamage += SubtractHealth;
        }


        private void OnDisable()
        {
            DamagePlayer.DoDamage -= SubtractHealth;
        }

        private void SubtractHealth(float damageAmount)
        {
            if (Health > 0)
            {
                Health -= damageAmount;
            }

            if (Health <= 0)
            {
                //Kill Player
                Debug.Log("Kill player");
            }
        }
    }
    
}