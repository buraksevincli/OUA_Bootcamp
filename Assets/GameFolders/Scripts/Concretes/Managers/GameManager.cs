using System;
using GameFolders.Scripts.Abstracts.Utilities;
using GameFolders.Scripts.Concretes.Controllers;
using UnityEngine.SceneManagement;

namespace GameFolders.Scripts.Concretes.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public int HeavyGunAmmo { get; set; }
        public int PistolAmmo { get; set; }
        public int Score { get; set; }
        
        private void Update()
        {
            if (PlayerHealthController.Instance.IsDead)
            {
                DataManager.Instance.EventData.GameOver?.Invoke();
            }
        }
        
        public void LoadGameScene(string scene)
        {
            SceneManager.LoadSceneAsync(scene);
        }
    }
}
