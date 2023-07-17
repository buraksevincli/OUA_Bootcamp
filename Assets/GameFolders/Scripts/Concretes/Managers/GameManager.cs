using System;
using GameFolders.Scripts.Abstracts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace GameFolders.Scripts.Concretes.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private TMP_Text score;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private TMP_Text lastScoreText;
        
        public int HeavyGunAmmo { get; set; }
        public int PistolAmmo { get; set; }
        public int Score { get; set; }

        private void Start()
        {
            Time.timeScale = 1;
        }

        private void OnEnable()
        {
            DataManager.Instance.EventData.OnZombieDead += OnZombieDeadHandler;
            DataManager.Instance.EventData.GameOver += GameOverHandler;
        }

        private void OnDisable()
        {
            DataManager.Instance.EventData.OnZombieDead -= OnZombieDeadHandler;
            DataManager.Instance.EventData.GameOver -= GameOverHandler;
        }

        private void GameOverHandler()
        {
            gameOverPanel.SetActive(true);
            lastScoreText.text = Score.ToString();
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void OnZombieDeadHandler()
        {
            score.text = Score.ToString();
        }

        public void LoadGameScene(string scene)
        {
            SceneManager.LoadSceneAsync(scene);
        }

        public void LoadMenuScene()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
