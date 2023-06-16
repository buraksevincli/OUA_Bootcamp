using System.Collections.Generic;
using GameFolders.Scripts.Concretes.Managers;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.PlayFab
{
    public class NewPlayFabManager : MonoBehaviour
    {
        [Header("UI")] 
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private TMP_InputField emailInput;
        [SerializeField] private TMP_InputField passwordInput;
        [SerializeField] private TMP_InputField nameInput;

        [SerializeField] private GameObject namePanel;
        [SerializeField] private GameObject loginPanel;

        private void OnEnable()
        {
            DataManager.Instance.EventData.GameOver += HandleGameOver;
        }

        private void OnDisable()
        {
            DataManager.Instance.EventData.GameOver -= HandleGameOver;
        }
        
        private void HandleGameOver()
        {
            SendLeaderboard(GameManager.Instance.Score);
        }
        
        public void SendLeaderboard(int score)
        {
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate
                    {
                        StatisticName = "Bootcamp Leaderboard",
                        Value = score
                    }
                }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnLeaderboardError);
        }

        private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
        {
            Debug.Log("Successfull leaderboard sent");
        }

        private void OnLeaderboardError(PlayFabError obj)
        {
            Debug.Log("Error leaderboard sent/get!");
        }
        
        public void GetLeaderboard()
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = "Bootcamp Leaderboard",
                StartPosition = 0,
                MaxResultsCount = 10
            };
            PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnLeaderboardError);
        }

        private void OnLeaderboardGet(GetLeaderboardResult result)
        {
            foreach (var item in result.Leaderboard)
            {
                Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);
            }
        }
        
        public void RegisterButton()
        {
            if (passwordInput.text.Length < 6)
            {
                messageText.text = "Password too short!";
                return;
            }
            
            var request = new RegisterPlayFabUserRequest
            {
                Email = emailInput.text,
                Password = passwordInput.text,
                RequireBothUsernameAndEmail = false
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterError);
        }

        private void OnRegisterError(PlayFabError obj)
        {
            messageText.text = "E-mail is being used!";
        }

        private void OnRegisterSuccess(RegisterPlayFabUserResult obj)
        {
            // Başarılı giriş sonrası isim girme paneli açılacak.
            loginPanel.SetActive(false);
            namePanel.SetActive(true);
            messageText.text = "";
        }

        public void SubmitNameButton()
        {
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = nameInput.text,
            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnSubmitNameError);
        }

        private void OnSubmitNameError(PlayFabError obj)
        {
            messageText.text = "This name is being used!";
        }

        private void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult obj)
        {
            Debug.Log("Display name Updated!");
            GameManager.Instance.LoadGameScene("Game");
        }
        
        public void LoginButton()
        {
            var request = new LoginWithEmailAddressRequest
            {
                Email = emailInput.text,
                Password = passwordInput.text
            };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginError);
        }

        private void OnLoginError(PlayFabError obj)
        {
            messageText.text = "E-mail or password is incorrect!";
        }

        private void OnLoginSuccess(LoginResult result)
        {
            messageText.text = "Logged in!";
            GameManager.Instance.LoadGameScene("Game");
        }
    }
}
