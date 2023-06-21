using System;
using GameFolders.Scripts.Concretes.Managers;
using GameFolders.Scripts.Concretes.ObjectPooling;
using UnityEngine;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class TargetController : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private GameObject explodeEffect;

        private EnemyController _enemyController;
        private bool _isDead;
        public bool IsDead => _isDead;

        private float _currentHealth;

        private void OnEnable()
        {
            _currentHealth = health;
            _isDead = false;
        }

        private void Awake()
        {
            _enemyController = GetComponent<EnemyController>();
        }

        public void TakeDamage(float amount)
        {
            _currentHealth -= amount;

            if (_currentHealth <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            
            Instantiate(explodeEffect, transform.position, transform.rotation);

            GameManager.Instance.Score++;

            ObjectPooler.Instance.SetPool(_enemyController);
        }
    }
}
