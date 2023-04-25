using System;
using System.Collections;
using Cinemachine;
using Enums;
using Extensions;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerManager : MonoBehaviour
    {

        /// <summary>
        /// A player prefab
        /// </summary>
        [SerializeField] private GameObject Player;

        [SerializeField] private CinemachineVirtualCamera Vcam;

        [SerializeField] private PlayerState playerState = PlayerState.Normal;

        [SerializeField] private float playerHurtTime = 2.5f;
        [SerializeField] private float playerInvincibleTime = 2.5f;
        
        /// <summary>
        /// The last position the player have saved at. The start of a level, a checkpoint, etc
        /// </summary>
        [SerializeField] public Vector3 PlayerLastSavedPos;

        private void Awake()
        {
            PlayerLastSavedPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        }

        private void OnEnable()
        {
            PlayerDeath.PlayerDied += RespawnPlayer;
            DamagePlayer.DoDamage += OnPlayerDamage;
        }

        private void OnDisable()
        {
            PlayerDeath.PlayerDied -= RespawnPlayer;
            DamagePlayer.DoDamage -= OnPlayerDamage;
        }

        /// <summary>
        /// Instantiate a new player obj and respawn them at
        /// their last saved position
        /// </summary>
        private void RespawnPlayer()
        {
            GameObject playerPrefab = Instantiate(Player) as GameObject;
            
            playerPrefab.name = "Player";
            playerPrefab.transform.position = PlayerLastSavedPos;
            Vcam.Follow = playerPrefab.transform;
        }

        /// <summary>
        /// When the player is damaged the player state is updated
        /// to hurt then to invincible then to normal
        /// </summary>
        private void OnPlayerDamage(float damageAmount)
        {
            playerState = PlayerState.Hurt;
            StartCoroutine(TogglePlayerInvincibilityState());
            StartCoroutine(TogglePlayerInvincibilityState());

        }

        /// <summary>
        /// Updates the player state 
        /// </summary>
        /// <param name="state"></param>
        private IEnumerator TogglePlayerInvincibilityState()
        {
            yield return new WaitForSeconds(playerHurtTime);
            playerState = PlayerState.Invincible;
            yield return new WaitForSeconds(playerInvincibleTime);
            playerState = PlayerState.Normal;
        }
    }
}
