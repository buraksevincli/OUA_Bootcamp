using System;
using System.Collections;
using GameFolders.Scripts.Abstracts.Utilities;
using GameFolders.Scripts.Concretes.Managers;
using UnityEngine;
using TMPro;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class PlayerHealthController : MonoSingleton<PlayerHealthController>
    {
        [SerializeField] private TMP_Text playerHealthText;
        [SerializeField] private int firstAid;

        private int _playerHealth;
        public int PlayerHealth => _playerHealth;

        private bool _isDead;

        private float _enemyFireRate = 1f;
        private float _nextTimeToAttack;

        private void Start()
        {
            _playerHealth = 100;
        }

        private void Update()
        {
            playerHealthText.text = _playerHealth.ToString();
        }

        public void TakeDamagePlayer(int amount)
        {
            if (_playerHealth > 0 && Time.time >= _nextTimeToAttack)
            {
                _playerHealth -= amount;
                _nextTimeToAttack = Time.time + 1f / _enemyFireRate;
            }

            if (_playerHealth <= 0)
            {
                _isDead = true;
                
                if (_isDead)
                {
                    DataManager.Instance.EventData.GameOver?.Invoke();
                    _isDead = false;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("FirstAid"))
            {
                _playerHealth += firstAid;
                other.gameObject.SetActive(false);
            }
        }
    }
}
