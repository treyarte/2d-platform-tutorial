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
            HealPlayer.Heal += AddHealth;
        }


        private void OnDisable()
        {
            DamagePlayer.DoDamage -= SubtractHealth;
            HealPlayer.Heal -= AddHealth;
        }

        private void AddHealth(float healAmount)
        {
            var newHealth = healAmount + Health;

            if (newHealth > MaxHealth)
            {
                Health = MaxHealth;
            }
            else
            {
                Health = newHealth;
            }
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