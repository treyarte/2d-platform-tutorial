using System;
using Cinemachine;
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
        }

        private void OnDisable()
        {
            PlayerDeath.PlayerDied -= RespawnPlayer;
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
        
    }
}
