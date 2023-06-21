using GameFolders.Scripts.Abstracts.Utilities;
using UnityEngine.SceneManagement;

namespace GameFolders.Scripts.Concretes.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public int HeavyGunAmmo { get; set; }
        public int PistolAmmo { get; set; }
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
