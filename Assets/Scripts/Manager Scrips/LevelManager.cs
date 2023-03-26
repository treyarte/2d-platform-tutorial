using System;
using Item_Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager_Scrips
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] public GameObject LevelCompleteUi; 
        
        private void OnEnable()
        {
            Goal.CompleteLevel += BeatLevel;
        }

        private void OnDisable()
        {
            Goal.CompleteLevel -= BeatLevel;
        }

        private void BeatLevel()
        {
            // Time.timeScale = 0f;
            LevelCompleteUi.SetActive(true);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }
    }
}