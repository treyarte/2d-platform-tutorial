using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Manager_Scrips
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] 
        private List<Transform> Hearts;
        [SerializeField] 
        private Transform HeartContainer;
        [SerializeField]
        private Transform HeartFull;
        [SerializeField]
        private Transform HeartHalf;
        [SerializeField]
        private Transform HeartEmpty;
        [SerializeField]
        private HealthManager healthManager;
        private void OnEnable()
        {
            DamagePlayer.DoDamage += RemoveHeart;
            InitializeHearts();
        }


        private void OnDisable()
        {
            DamagePlayer.DoDamage -= RemoveHeart;
        }

        private void InitializeHearts()
        {
            var health = healthManager.Health;
            var maxHealth = healthManager.MaxHealth;
            var hearts = new Transform [(int)maxHealth];
            ClearChildren(HeartContainer);
            for (var i = 0; i < maxHealth; i++)
            {
                hearts[i] = (HeartFull);
                var t = Instantiate(HeartFull, HeartContainer.transform, true);
            }
        }
        
        private void ClearChildren( Transform parent)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }

        private void RemoveHeart(float damageAmount)
        {

            if (damageAmount == 0)
            {
                return;
            }
            
            if (damageAmount < 1)
            {
                var maxChild = 0;
                foreach (Transform child in HeartContainer.transform)
                {
                    var heartType = child.gameObject.GetComponentInChildren<HeartType>();

                    if (heartType.heartType == HeartTypeEnum.HeartFull ||
                        heartType.heartType == HeartTypeEnum.HeartHalf)
                    {
                        maxChild++;
                    }
                }
                
                if (maxChild <= 0)
                {
                    return;
                }
            
                Destroy(HeartContainer.GetChild(maxChild - 1).gameObject);
                var t = Instantiate(HeartHalf, HeartContainer.transform, true);
            }
            else
            {
                for (var i = 0; i < damageAmount; i++)
                {
                    var maxChild = 0;
                    foreach (Transform child in HeartContainer.transform)
                    {
                        var heartType = child.gameObject.GetComponentInChildren<HeartType>();

                        if (heartType.heartType == HeartTypeEnum.HeartFull ||
                            heartType.heartType == HeartTypeEnum.HeartHalf)
                        {
                            maxChild++;
                        }
                    }

                    if (maxChild <= 0)
                    {
                        return;
                    }
                    
                    Destroy(HeartContainer.GetChild(maxChild -1).gameObject);

                    var t = Instantiate(HeartEmpty, HeartContainer.transform, true);
                }
            }
        }
    }
}