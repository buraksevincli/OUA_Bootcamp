using System;
using GameFolders.Scripts.Concretes.PlayFab;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameFolders.Scripts.Concretes.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button leaderboardButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button exitButton;

        [SerializeField] private GameObject leaderboardPanel;

        private void OnEnable()
        {
            startButton.onClick?.AddListener(StartGame);
            exitButton.onClick?.AddListener(ExitGame);
            leaderboardButton.onClick?.AddListener(LeaderboardClick);
        }

        private void OnDisable()
        {
            startButton.onClick?.RemoveListener(StartGame);
            exitButton.onClick?.RemoveListener(ExitGame);
            leaderboardButton.onClick?.RemoveListener(LeaderboardClick);
        }

        private void LeaderboardClick()
        {
            NewPlayFabManager.Instance.GetLeaderboard();
            leaderboardPanel.SetActive(!leaderboardPanel.activeSelf);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        private void StartGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
