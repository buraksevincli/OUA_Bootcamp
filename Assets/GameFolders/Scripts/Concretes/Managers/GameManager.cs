using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Abstracts.Utilities;
using GameFolders.Scripts.Concretes.PlayFab;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFolders.Scripts.Concretes.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public int HeavyGunAmmo { get; set; }
        public int RifleAmmo { get; set; }
        public int MiniGunAmmo { get; set; }
        public int Score { get; set; }

        public void SendDataToLeaderboard()
        {
            DataManager.Instance.EventData.GameOver?.Invoke();
        }

        public void LoadGameScene(string scene)
        {
            SceneManager.LoadSceneAsync(scene);
        }
    }
}
