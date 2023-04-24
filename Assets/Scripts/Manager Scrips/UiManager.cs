using System.Collections.Generic;
using UnityEngine;

namespace Manager_Scrips
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private List<Transform> Hearts;
        [SerializeField]
        private Transform HeartFull;
        [SerializeField]
        private Transform HeartHalf;
        [SerializeField]
        private Transform HeartEmpty;
        private void OnEnable()
        {
            DamagePlayer.DoDamage += RemoveHeart;
        }


        private void OnDisable()
        {
            DamagePlayer.DoDamage -= RemoveHeart;
        }

        private void RemoveHeart(float damageAmount)
        {
            if (damageAmount < 1)
            {
                Hearts.RemoveAt(Hearts.Count);
                Hearts.Add(HeartHalf);
            }
            else
            {
                for (var i = 0; i < damageAmount; i++)
                {
                    Hearts.RemoveAt(Hearts.Count);
                    Hearts.Add(HeartEmpty);
                }
            }
        }
    }
}