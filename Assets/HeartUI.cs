using System.Collections;
using System.Collections.Generic;
using Enums;
using Manager_Scrips;
using UnityEngine;

public class HeartUI : MonoBehaviour
{
    private Transform _heartContainer;
    
    [SerializeField]
    private Transform heartFull;
    [SerializeField]
    private Transform heartHalf;
    [SerializeField]
    private Transform heartEmpty;
    [SerializeField]
    private HealthManager healthManager;
        private void OnEnable()
        {
            _heartContainer = this.GetComponent<Transform>();
            DamagePlayer.DoDamage += RemoveHeart;
            InitializeHearts();
        }


        private void OnDisable()
        {
            DamagePlayer.DoDamage -= RemoveHeart;
        }
        
        /// <summary>
        /// Remove children from a transform
        /// </summary>
        /// <remarks>
        /// TODO move this to some type of helper class
        /// </remarks>
        /// <param name="parent"></param>
        private void ClearChildren( Transform parent)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Initializes a set of hearts based on the starting/current amount
        /// of health in the healthManager class
        /// </summary>
        private void InitializeHearts()
        {
            ClearChildren(_heartContainer);
            
            var health = healthManager.Health;

            //Creating new full hearts and attaching them as children to this component 
            for (var i = 0; i < health; i++)
            {
                Instantiate(heartFull, _heartContainer.transform, true);
            }
        }

        /// <summary>
        /// Get the total amount of full and half hearts
        /// </summary>
        /// <returns></returns>
        private int GetFullAndHalfHeartsCount()
        {
            int heartCount = 0;

            foreach (Transform child in _heartContainer.transform)
            {
                var heartType = child.gameObject.GetComponent<HeartType>();
                
                if (heartType.heartType is HeartTypeEnum.HeartFull or HeartTypeEnum.HeartHalf)
                {
                    heartCount++;
                }
            }

            return heartCount;
        }

        /// <summary>
        /// Destroys a heart at the passed in index and
        /// and create a new heart
        /// </summary>
        /// <param name="heartToDestroyIndex"></param>
        /// <param name="heartToAdd"></param>
        private void DestroyAndAddHeart(int heartToDestroyIndex, Transform heartToAdd)
        {
            Destroy(_heartContainer.GetChild(heartToDestroyIndex).gameObject);
            Instantiate(heartToAdd, _heartContainer.transform, true);
        }
 
        /// <summary>
        /// Remove a heart from the heart container and replace it
        /// with a half or empty heart
        /// </summary>
        /// <remarks>
        /// TODO update for greater than 1 and fractions
        /// </remarks>
        /// <param name="damageAmount"></param>
        private void RemoveHeart(float damageAmount)
        {

            if (damageAmount == 0)
            {
                return;
            }

            var heartCount = GetFullAndHalfHeartsCount();

            if (heartCount < 1)
            {
                return;
            }
            
            if (damageAmount < 1)
            {
                DestroyAndAddHeart(heartCount - 1, heartHalf);
            } else if (damageAmount >= 1)
            {
                DestroyAndAddHeart(heartCount - 1, heartEmpty);
            }
            
        }
}
